﻿using DATN.Client.Services;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.BatchVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.Product_EAV;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class BatchController : Controller
    {

        private readonly ClientService _clientService;
        private readonly ILogger<BatchController> _logger;
        public BatchController(ClientService _client, ILogger<BatchController> logger)
        {
            _clientService = _client;
            _logger = logger;
        }
        public async Task<IActionResult> Index(BatchPaging request)
        {
            BatchPaging batchPaging = new BatchPaging();
            batchPaging = await _clientService.Post<BatchPaging>("https://localhost:7095/api/Batch/GetAllPaging", request);
            if (batchPaging == null)
            {
                return NotFound();
            }
            return View(batchPaging);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBatchRequest request)
        {
            var result = await _clientService.Post<Batch>("https://localhost:7095/api/Batch/Create", request);
            if (result != null)
            {
                return RedirectToAction("Index", "Voucher", new {BatchId = result.Id});
            }
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _clientService.Get<Batch>($"https://localhost:7095/api/Batch/get/{id}");
            if (result == null)
            {
                return NotFound();
            }
            //var assignVoucherExist = result.Vouchers.Where(x=>x.Status == Core.Enum.VoucherStatus.NotUsed || x.Status == Core.Enum.VoucherStatus.Used).Any();
            //ViewBag.AssignVoucherExist = assignVoucherExist;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBatchRequest request)
        {
            var result = await _clientService.Post<Batch>("https://localhost:7095/api/Batch/Edit", request);
            if (result != null)
            {
                return RedirectToAction("Index");
            }
            return View(request);
        }
    }
}
