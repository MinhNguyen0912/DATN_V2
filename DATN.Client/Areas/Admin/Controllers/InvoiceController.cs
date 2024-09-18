using DATN.Client.Helper;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.GHNVM;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ShippingOrderVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using CreateGHNOrderAdmin = DATN.Core.ViewModel.GHNVM.CreateGHNOrderAdmin;
using OrderItemsGHNAdmin = DATN.Core.ViewModel.GHNVM.OrderItemsGHNAdmin;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class InvoiceController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DATNDbContext _context;
        public InvoiceController(ClientService clientService, IUnitOfWork unitOfWork, DATNDbContext context)
        {
            _clientService = clientService;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<IActionResult> Index(InvoicePaging request)
        {
            InvoicePaging invoicePaging = new InvoicePaging();


            invoicePaging = await _clientService.Post<InvoicePaging>("https://localhost:7095/api/Invoice/GetInvoicePaging", request);

            if (invoicePaging == null)
            {
                return NotFound();
            }

            return View(invoicePaging);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _clientService.Get<InvoiceVM>($"https://localhost:7095/api/Invoice/GetById/get-by-id?id={id}");
            return View(result);

        }
        public async Task<IActionResult> ChangeStatus(int id, int status)
        {
            var order = await _context.Invoices.FindAsync(id);
            if (order == null)
            {
                // Nếu đơn hàng không tồn tại, trả về lỗi 404
                return NotFound();
            }
            if (status == 1)
            {
                order.Status = DATN.Core.Enum.InvoiceStatus.Success;
            }
            else { order.Status = DATN.Core.Enum.InvoiceStatus.Cancel; }
            // Cập nhật trạng thái của đơn hàng

            _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private List<List<Variant>> CalculateShipments(List<Variant> variants, double maxWeightPerShipment) //max = 10000
        {
            // Sắp xếp các sản phẩm theo thứ tự giảm dần trọng lượng
            variants.Sort((a, b) => b.Weight.CompareTo(a.Weight));

            // Danh sách các lần ship, mỗi lần ship là một danh sách chứa các Variant
            List<List<Variant>> shipments = new List<List<Variant>>();

            foreach (var variant in variants)
            {
                bool added = false;

                // Tìm xem có chuyến ship nào có thể thêm được sản phẩm này mà không vượt quá maxWeightPerShipment
                foreach (var shipment in shipments)
                {
                    double currentShipmentWeight = shipment.Sum(v => v.Weight);
                    if (currentShipmentWeight + (variant.Weight <= maxWeightPerShipment ? variant.Weight : maxWeightPerShipment) <= maxWeightPerShipment)
                    {
                        shipment.Add(variant);
                        added = true;
                        break;
                    }
                }

                // Nếu không thể thêm sản phẩm vào chuyến ship nào thì tạo một chuyến ship mới
                if (!added)
                {
                    shipments.Add(new List<Variant> { variant });
                }
            }

            // Trả về danh sách các đơn hàng
            return shipments;
        }
        public async Task<IActionResult> CreateShippingOrder(int invoiceId)
        {
            var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(invoiceId);
            var cart = _unitOfWork.InvoiceDetailRepository.FincByInvoiceIdCustom(invoiceId).Select(p => p.Variant).ToList();

            var cartCaculate = CalculateShipments(cart, 10000);

            foreach (var item in cartCaculate)
            {
                var items = new List<OrderItemsGHNAdmin>();
                foreach (var item1 in item)
                {
                    var itemz = new OrderItemsGHNAdmin()
                    {
                        name = $"{item1.Product.ProductName}-{item1.VariantName}",
                        quantity = item1.Quantity,
                        weight = item1.Weight
                    };
                    items.Add(itemz);
                }
                var bodyGHN = new CreateGHNOrderAdmin()
                {
                    to_name = invoice.Note.Split('-')[0],
                    to_phone = invoice.Note.Split('-')[1],
                    to_address = invoice.Note.Split('-')[2],
                    to_ward_code = invoice.Note.Split('-')[3],
                    to_district_id = invoice.Note.Split('-')[4],
                    cod_amount = 0,
                    weight = item.Sum(p => p.Weight),
                    Items = items

                };
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Token", "af9239df-2402-11ef-8e53-0a00184fe694");
                    client.DefaultRequestHeaders.Add("shop_Id", "192491");

                    // Convert bodyGHN to JSON (if it's an object)
                    var jsonBody = JsonConvert.SerializeObject(bodyGHN);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Gửi yêu cầu POST
                    var response = await client.PostAsync("https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CreateGHNResponse>(responseContent);

                    var shippingOrder = new ShippingOrder()
                    {
                        OrderCode = data.Data.order_code,
                        Price = double.Parse(data.Data.total_fee),
                        UserId = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user").UserId,
                        InvoiceId = invoiceId
                    };
                    await _unitOfWork.ShippingOrderRepository.Create(shippingOrder);
                }
            }
            invoice.PaymentInfo.PaymentStatus = Core.Enum.PaymentStatus.Success;
            invoice.Status = Core.Enum.InvoiceStatus.Delivery;
            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.SaveChanges();

            
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> CancelInvoice(int invoiceId)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetById(invoiceId);
            invoice.Status = Core.Enum.InvoiceStatus.Cancel;
            //if (invoice.VoucherUser != null)
            //{
            //    invoice.VoucherUser.IsDeleted = false;
            //}
            var decent = await _clientService.Get($"https://localhost:7095/api/Invoice/ChangeStatus2?invoiceId={invoiceId}");
            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.SaveChanges();
            var content = CancelnvoiceContent.GenerateContentMail(invoice.Note.Split("-")[6], invoice);
            var sendemail = await _clientService.Post("https://localhost:7095/api/SendMail/SendMail", content);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(int InvoiceId, string RecipientName, string PhoneNumber, string Address,string matinh, string madiaphuong)
        {
            // Tìm đối tượng người dùng theo id
            var user = await _context.Invoices.FindAsync(InvoiceId);
            if (user == null)
            {
                return NotFound();
            }

            // Ghép các giá trị lại thành chuỗi Note
            user.Note = $"{RecipientName}-{PhoneNumber}-{Address}-{matinh}-{madiaphuong}";

            // Cập nhật thông tin người dùng
            _context.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Quay lại trang danh sách hoặc trang khác sau khi cập nhật thành công
        }
    }
}