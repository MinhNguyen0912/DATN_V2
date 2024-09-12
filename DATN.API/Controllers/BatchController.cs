using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.BatchVM;
using DATN.Core.ViewModel.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BatchController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBatchRequest request)
        {
            if (request == null)
            {
                return BadRequest("Batch data is null"); // 400 Bad Request
            }
            var batch = new Batch
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                Type = request.Type,
                DiscountType = request.DiscountType,
                DiscountAmount = request.DiscountAmount,
                MinOrderAmount = request.MinOrderAmount,
                MaxDiscountAmount = request.MaxDiscountAmount,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ExpirationDate = request.ExpirationDate
            };
            _unitOfWork.BatchRepository.Create(batch);
            _unitOfWork.SaveChanges();
            return Ok(batch);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllPaging([FromBody] BatchPaging request)
        {
            var batches = _unitOfWork.BatchRepository.batchPaging(request);
            return Ok(batches);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] BatchVM request)
        {
            var batch = _unitOfWork.BatchRepository.GetByIdCustom(request.Id);
            if (batch == null)
            {
                return NotFound(); // 404 Not Found
            }
            _mapper.Map(request, batch);
            _unitOfWork.BatchRepository.Update(batch);
            _unitOfWork.SaveChanges();
            return Ok(batch);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var batch = _unitOfWork.BatchRepository.GetByIdCustom(id);
            if (batch == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(batch);
        }
    }
}
