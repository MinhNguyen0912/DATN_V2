using AutoMapper;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Models;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModel.VoucherVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class VoucherController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        private readonly DATNDbContext _dbContext;
        public VoucherController(IUnitOfWork unitOfWork, IMapper mapper, ClientService clientService, HttpClient httpClient, DATNDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientService = clientService;
            _httpClient = httpClient;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index(int BatchId, string userName = null, Core.Enum.VoucherStatus? status = null)
        {
            var allUsers = await _clientService.Get<List<AppUser>>("https://localhost:7095/api/User/GetAllUser");
            var users = allUsers.Where(x=>x.Description == "Customer").ToList();
            var request = new SearchVoucherRequest
            {
                BatchId = BatchId,
                UserName = userName,
                Status = status
            };

            List<VoucherVM> vouchers;
            if (!string.IsNullOrEmpty(userName) || status.HasValue)
            {
                vouchers = await _clientService.Post<List<VoucherVM>>("https://localhost:7095/api/Voucher/SearchVoucher_Viet", request);
            }
            else
            {
                vouchers = await _clientService.GetList<VoucherVM>($"https://localhost:7095/api/Voucher/GetVoucherByBatchId_Viet/{BatchId}");
            }

            if (vouchers == null)
            {
                vouchers = new List<VoucherVM>();
            }
            ViewBag.Users = users;
            ViewBag.BatchId = BatchId;
            return View(vouchers);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var voucher = await _clientService.Get<VoucherVM>($"https://localhost:7095/api/Voucher/Get/{id}");

                if (voucher == null)
                {
                    throw new Exception("Không tìm thấy");
                }
                return View(voucher);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> AssignVoucher(AssignVoucherRequest request, int batchId, string userName = null, Core.Enum.VoucherStatus? status = null)
        {
            try
            {
                var result = await _clientService.Post<AssignVoucherRequest>("https://localhost:7095/api/Voucher/AssignVoucher_Viet", request);
                if (result != null)
                {
                    ToastHelper.ShowSuccess(TempData, "Phân phối voucher thành công!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return RedirectToAction("Index", new { BatchId = batchId, userName, status });
        }
        [HttpPost]
        public async Task<IActionResult> RevokeVoucher(int id, int batchId, string userName = null, Core.Enum.VoucherStatus? status = null)
        {
            try
            {
                var result = await _clientService.Post<int>($"https://localhost:7095/api/Voucher/RevokeVoucher_Viet/{id}", null);
                if (result > 0)
                {
                    ToastHelper.ShowSuccess(TempData, "Thu hồi voucher thành công!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return RedirectToAction("Index", new { BatchId = batchId, userName, status });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteVoucher(int id, int batchId, string userName = null, Core.Enum.VoucherStatus? status = null)
        {
            try
            {
                var result = await _clientService.Delete<VoucherVM>($"https://localhost:7095/api/Voucher/Delete/{id}");
                if (result != null)
                {
                    ToastHelper.ShowSuccess(TempData, "Xóa voucher thành công!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return RedirectToAction("Index", new { BatchId = batchId, userName, status });
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(VoucherVM voucher)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(voucher); // Trả về lại view với model và hiển thị lỗi
        //        }

        //        var result = await _clientService.Post<VoucherVM>("https://localhost:7095/api/Voucher/Create", voucher);
        //        if (result != null)
        //        {
        //            ToastHelper.ShowSuccess(TempData, "Thêm thành công!");


        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        // Xử lý lỗi và hiển thị thông báo lỗi nếu cần
        //        TempData["Error"] = ex.Message;

        //    }
        //    return RedirectToAction("Index");



        //}
        //public async Task<IActionResult> Update(int id)
        //{
        //    try
        //    {

        //        var lstVoucher = await _clientService.Get<VoucherVM>($"https://localhost:7095/api/Voucher/Get/{id}");
        //        return View(lstVoucher);
        //    }
        //    catch (Exception ex)
        //    {
        //        ToastHelper.ShowError(TempData, ex.Message);
        //        return RedirectToAction("Index");
        //    }

        //}
        //[HttpPost]
        //public async Task<IActionResult> Update1(VoucherVM voucher)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return RedirectToAction("Update", voucher); // Trả về lại view với model và hiển thị lỗi

        //        }
        //        var result = await _clientService.Put<VoucherVM>($"https://localhost:7095/api/Voucher/Update/{voucher.Id}", voucher);
        //        if (result != null)
        //        {
        //            ToastHelper.ShowSuccess(TempData, "Sửa thành công!");

        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        // Xử lý lỗi và hiển thị thông báo lỗi nếu cần
        //        TempData["Error"] = ex.Message;

        //    }
        //    return RedirectToAction("Index");
        //}
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var voucher = await _clientService.Delete<VoucherVM>($"https://localhost:7095/api/Voucher/Delete/{id}");
        //        if (voucher != null)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        return BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        ToastHelper.ShowError(TempData, ex.Message);
        //        return RedirectToAction("Index");
        //    }

        //}
    }
}
