using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.Product_EAV;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductEAVController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductEAVController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<List<Product_EAV>> GetProductByPromotion(int promotionId)
        {
            var products = _unitOfWork.productPromotionRepository.GetAllCustom().Where(p => p.PromotionId == promotionId).Select(p => p.Product).ToList();


            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM_EAV>>(products);
                return Ok(productVms);
            }

            return NoContent(); // Trả về 204 nếu không tìm thấy sản phẩm nào
        }
        [HttpGet]
        public ActionResult<int> GetProductRateCount(int productId)
        {
            var data = _unitOfWork.InvoiceDetailRepository.GetAll().Where(p => p.Variant.ProductId == productId).Count(p => p.Comment != null);
            return Ok(data);

        }
        [HttpGet]
        public ActionResult<double> GetProductRating(int productId)
        {
            var data = _unitOfWork.InvoiceDetailRepository.GetAll().Where(p => p.Variant.ProductId == productId).ToList();
            if (data != null && data.Count > 0)
            {
                return Ok(Math.Round((decimal)data.Where(p => p.Comment != null).Average(p => p.Comment.Rating), 1));
            }
            return Ok(5);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = _unitOfWork.ProductEAVRepository.GetByIdCustom(id);
            var brand = await _unitOfWork.brandRepository.GetById(products.BrandId);

            products.Brand.Name = brand.Name;
            if (products != null)
            {
                var productsVM = _mapper.Map<ProductVM_EAV>(products);
                return Ok(productsVM);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult GetAll_Viet()
        {
            var data = _unitOfWork.ProductEAVRepository.GetAll();
            var result = _mapper.Map<List<Product_EAV>>(data);
            return Ok(result);
        }
    }
}
