using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.GHNVM;
using DATN.Core.ViewModel.InvoiceDetailVM;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.PendingCartVM;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.VNPayVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace DATN.Client.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly ClientService _clientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;


        public CartController(ClientService clientService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _clientService = clientService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
            if (user == null)
            {
                return Redirect("~/Identity/Account/Login");
            }
            var pendingCart = await _clientService.Post<PendingCartVM>("https://localhost:7095/api/PendingCart/GetByUserId",user.UserId);
            var voucher = await _clientService.GetList<VoucherVM>($"https://localhost:7095/api/Voucher/GetVoucherByUserId/{user.UserId}");
            try
            {
                //ViewData["voucher"] = voucher;
                ViewData["user"] = user;
                ViewData["pendingCart"] = pendingCart;
                ViewData["voucher"] = voucher;
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View();
        }
        public async Task<IActionResult> RemoveVariant(int variantId, int pendingCartId)
        {
            var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
            await _clientService.Get($"https://localhost:7095/api/PendingCart/RemoveVariant?variantId={variantId}&pendingCartId={pendingCartId}");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ListProduct()
        {
            //var lst = await _clientService.GetList<ProductVM>("https://localhost:7095/api/Product/GetAll");
            //return View(lst);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PaymentProcess(PaymentRequest request)
        {
            var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
            request.UserId = user.UserId;
            var invoice = await _clientService.Post<Invoice>("https://localhost:7095/api/Invoice/Create", request);
            if (request.PaymentMethod == Core.Enum.PaymentMethod.Cash)
            {
                await _clientService.Post($"https://localhost:7095/api/PendingCart/UpdatePendingCart?pendingCartId={request.pendingCartId}", new List<PendingCartVariant>());
            }
            else if (request.PaymentMethod == Core.Enum.PaymentMethod.TheNoiDia)
            {
                var totalMoney = invoice.InvoiceDetails.Sum(p => p.NewPrice * p.Quantity);
                return RedirectToAction("Pay", new { typePayment = 1, money = totalMoney + request.ShippingFee, invoiceId = invoice.InvoiceId, pendingCartId = request.pendingCartId });
            }
            else if (request.PaymentMethod == Core.Enum.PaymentMethod.TheQuocTe)
            {
                var totalMoney = invoice.InvoiceDetails.Sum(p => p.NewPrice * p.Quantity);
                return RedirectToAction("Pay", new { typePayment = 2, money = totalMoney + request.ShippingFee, invoiceId = invoice.InvoiceId, pendingCartId = request.pendingCartId });
            }
            return RedirectToAction("Index");
        }
        public IActionResult Pay(int typePayment, decimal money, int invoiceId, int pendingCartId)
        {
            try
            {
                //Get Config Info
                string vnp_Returnurl = _configuration["AppSettings:vnp_Returnurl"]; //URL nhan ket qua tra ve 
                string vnp_Url = _configuration["AppSettings:vnp_Url"]; //URL thanh toan cua VNPAY 
                string vnp_TmnCode = _configuration["AppSettings:vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
                string vnp_HashSecret = _configuration["AppSettings:vnp_HashSecret"]; //Secret Key

                //Get payment input
                OrderInfo order = new OrderInfo();
                order.OrderId = DateTime.Now.Ticks; // Giả lập mã giao dịch hệ thống merchant gửi sang VNPAY
                order.Amount = Convert.ToInt64(money); // Giả lập số tiền thanh toán hệ thống merchant gửi sang VNPAY 100,000 VND
                order.Status = "0"; //0: Trạng thái thanh toán "chờ thanh toán" hoặc "Pending" khởi tạo giao dịch chưa có IPN
                order.CreatedDate = DateTime.Now;
                //Save order to db

                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

                if (typePayment == 1)
                {
                    vnpay.AddRequestData("vnp_BankCode", "VNBANK");
                }
                else if (typePayment == 2)
                {
                    vnpay.AddRequestData("vnp_BankCode", "INTCARD");
                }
                var ultils = new Utils(_httpContextAccessor);
                vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", ultils.GetIpAddress());

                vnpay.AddRequestData("vnp_Locale", "vn");

                vnpay.AddRequestData("vnp_OrderInfo", "Hóa đơn quyên góp" + order.OrderId);
                vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                //Add Params of 2.1.0 Version
                //Billing
                HttpContext.Session.SetInt32("invoiceId", invoiceId);
                HttpContext.Session.SetInt32("pendingCartId", pendingCartId);
                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                return Redirect(paymentUrl);
            }
            catch (Exception ex)
            {

                ToastHelper.ShowError(TempData, ex.Message);

                return RedirectToAction("Error");
            }

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
        public async Task<IActionResult> vnpay_return()
        {

            if (HttpContext.Request.Query.Count > 0)
            {
                int? invoiceId = HttpContext.Session.GetInt32("invoiceId");
                int? pendingCartId = HttpContext.Session.GetInt32("pendingCartId");
                string vnp_HashSecret = _configuration["AppSettings:vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = HttpContext.Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (var s in vnpayData.Keys)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = HttpContext.Request.Query["vnp_SecureHash"];
                string TerminalID = HttpContext.Request.Query["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(HttpContext.Request.Query["vnp_Amount"]) / 100;
                string bankCode = HttpContext.Request.Query["vnp_BankCode"];



                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        ViewBag.displayMsg = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";

                        var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(invoiceId.Value);
                        var cart = _unitOfWork.InvoiceDetailRepository.FincByInvoiceIdCustom(invoiceId.Value).Select(p => p.Variant).ToList();

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
                                    InvoiceId = invoiceId.Value
                                };
                                await _unitOfWork.ShippingOrderRepository.Create(shippingOrder);
                            }
                        }
                        await _clientService.Post($"https://localhost:7095/api/PendingCart/UpdatePendingCart?pendingCartId={pendingCartId}", new List<PendingCartVariant>());
                        invoice.PaymentInfo.PaymentStatus = Core.Enum.PaymentStatus.Success;
                        invoice.Status = Core.Enum.InvoiceStatus.Delivery;
                        _unitOfWork.InvoiceRepository.Update(invoice);
                        _unitOfWork.SaveChanges();
                        return Redirect("/Home/Index");

                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.displayMsg = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                        //await _clientService.Get($"{ApiPaths.Invoice}/ChangeStatus?invoiceId={invoiceId}&status={5}");

                        var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(invoiceId.Value);
                        invoice.PaymentInfo.PaymentStatus = Core.Enum.PaymentStatus.Fail;
                        invoice.Status = Core.Enum.InvoiceStatus.Fail;
                        _unitOfWork.InvoiceRepository.Update(invoice);
                        _unitOfWork.SaveChanges();
                    }
                    ViewBag.displayTmnCode = "Mã Website (Terminal ID):" + TerminalID;
                    ViewBag.displayTxnRef = "Mã giao dịch thanh toán:" + orderId.ToString();
                    ViewBag.displayVnpayTranNo = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    ViewBag.displayAmount = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    ViewBag.displayBankCode = "Ngân hàng thanh toán:" + bankCode;
                }
                else
                {
                    ViewBag.displayMsg = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }
            return Redirect("/Cart/Index");
        }
    }
}
