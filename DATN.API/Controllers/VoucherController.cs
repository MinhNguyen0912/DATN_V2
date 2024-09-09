﻿using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Service;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModel.VoucherVM;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VoucherService _voucherService;

        public VoucherController(IUnitOfWork unitOfWork, IMapper mapper, VoucherService voucherService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _voucherService = voucherService;
        }
        [HttpPost]
        public IActionResult GetVoucherPaging([FromBody] VoucherPaging request)
        {
            VoucherPaging voucherPaging = _unitOfWork.VoucherRepository.GetVoucherPaging(request);
            return Ok(voucherPaging);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vouchers = _unitOfWork.VoucherRepository.GetAll();

            if (vouchers != null)
            {
                var vouchersVM = _mapper.Map<List<VoucherVM>>(vouchers);
                return Ok(vouchersVM);
            }
            return NoContent(); // Trả về 204 nếu không tìm thấy newfeed
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id);
            if (voucher == null)
            {
                return NotFound(); // 404 Not Found
            }
            var result = _mapper.Map<Voucher>(voucher);
            return Ok(result); // 200 OK
        }
        [HttpPost]
        public async Task<IActionResult> Create(VoucherVM voucherVM)
        {
            if (voucherVM == null)
            {
                return BadRequest("Product data is null"); // 400 Bad Request
            }

            var voucher = _mapper.Map<Voucher>(voucherVM);
            await _unitOfWork.VoucherRepository.Create(voucher);
            _unitOfWork.SaveChanges();

            return Ok(voucher); // 201 Created
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id);
            if (voucher == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.VoucherRepository.Delete(voucher);
            _unitOfWork.SaveChanges();

            return Ok(voucher);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VoucherVM voucherVM)

        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id);
            if (voucher == null)
            {
                return NotFound(); // 404 Not Found
            }

            _mapper.Map(voucherVM, voucher);
            _unitOfWork.VoucherRepository.Update(voucher);
            _unitOfWork.SaveChanges();

            return Ok(voucher);
        }

        //Dùng api từ đây trở đi
        [HttpPost]
        public async Task<IActionResult> CreateVoucher_Viet([FromBody] CreateVoucherRequest request)
        {
            if (request == null)
            {
                return BadRequest("Voucher data is null"); // 400 Bad Request
            }
            var batch =await _unitOfWork.BatchRepository.GetById(request.BatchId);
            foreach (var item in request.VoucherCodes)
            {
                var voucher = new Voucher
                {
                    BatchId = request.BatchId,
                    Code = item,
                    Status = Core.Enum.VoucherStatus.Unpushlished
                };
                if (batch.EndDate != null)
                {
                    voucher.ExpiryDate = batch.EndDate;
                }
                await _unitOfWork.VoucherRepository.Create(voucher);
            }
            int result = _unitOfWork.SaveChanges();
            return Ok(result); // 201 Created
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoucher_Phuc([FromBody] CreateVoucherRequest request)
        {
            if (request == null)
            {
                return BadRequest("Voucher data is null"); // 400 Bad Request
            }

            var batch = await _unitOfWork.BatchRepository.GetById(request.BatchId);
            if (batch == null)
            {
                return NotFound("Batch not found");
            }
                try
                {
                    foreach (var item in request.VoucherCodes)
                    {
                        var voucher = new Voucher
                        {
                            BatchId = request.BatchId,
                            Code = item,
                            Status = Core.Enum.VoucherStatus.Unpushlished,
                            ExpiryDate = batch.EndDate ?? null,
                            ActivationTime = request.ActivationTime // assuming you pass the activation time in the request
                        };
                        await _unitOfWork.VoucherRepository.Create(voucher);
                    }

                    int result = _unitOfWork.SaveChanges();


                // Schedule voucher activation using Hangfire
                var activationDelay = request.ActivationTime - DateTime.Now;
                if (activationDelay > TimeSpan.Zero)
                {
                    BackgroundJob.Schedule(() => _voucherService.GenerateVoucherActivationTimeAsync(request.ActivationTime), activationDelay);
                }

                return Ok(result); // 201 Created
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "Internal Server Error");
                }
            
        }


        // Method to activate vouchers based on the BatchId and ActivationTime
        //public async Task ActivateVouchers(Guid batchId, DateTime activationTime)
        //{
        //    var vouchers = await _unitOfWork.VoucherRepository.GetVouchersByBatchAndTime(batchId, activationTime);
        //    foreach (var voucher in vouchers)
        //    {
        //        voucher.Status = Core.Enum.VoucherStatus.NotUsed;
        //        _unitOfWork.VoucherRepository.Update(voucher);
        //    }
        //    await _unitOfWork.SaveChangesAsync();
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVoucherByBatchId_Viet(int id)
        {
            var vouchers = _unitOfWork.VoucherRepository.GetAllVouchers().Where(p => p.BatchId == id).ToList();
            if (vouchers != null && vouchers.Any())
            {
                var vouchersVM = _mapper.Map<List<VoucherVM>>(vouchers);
                return Ok(vouchersVM);
            }
            return NoContent(); // Trả về 204 nếu không tìm thấy newfeed
        }
    }
}
