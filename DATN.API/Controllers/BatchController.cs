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
                ExpirationDate = request.ExpirationDate,
                VoucherCates = new List<VoucherCate>(),
                VoucherProducts = new List<VoucherProduct>()
            };
            if (request.ApplyToAll == true)
            {
                var cates = _unitOfWork.CategoryRepository.GetAll();
                var products = _unitOfWork.ProductEAVRepository.GetAll();
                foreach (var item in products)
                {
                    var product = new VoucherProduct
                    {
                        ProductId = item.ProductId
                    };
                    batch.VoucherProducts.Add(product);
                }
                foreach (var item in cates)
                {
                    var cate = new VoucherCate
                    {
                        CategoryId = item.Id
                    };
                    batch.VoucherCates.Add(cate);
                }
            }
            else
            {
                if (request.CateItem != null)
                {
                    foreach (var item in request.CateItem)
                    {
                        var cate = new VoucherCate
                        {
                            CategoryId = item
                        };
                        batch.VoucherCates.Add(cate);
                    }
                }
                if (request.ProdItem != null)
                {
                    foreach (var item in request.ProdItem)
                    {
                        var product = new VoucherProduct
                        {
                            ProductId = item
                        };
                        batch.VoucherProducts.Add(product);
                    }
                }
            }
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
