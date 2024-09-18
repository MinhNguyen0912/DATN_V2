using AutoMapper;
using DATN.API.Data.Filter;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModel.ProdutEAVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                if (data.Where(p => p.Comment != null).Count()>0)
                {
                    return Ok(Math.Round((decimal)data.Where(p => p.Comment != null).Average(p => p.Comment.Rating), 1));
                }              
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
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var products = await _unitOfWork.ProductEAVRepository.GetByName(name);
            
            if (products.Count>0)
            {
                var lstproductsVM = _mapper.Map<List<ProductVM_EAV>>(products);
                return Ok(lstproductsVM);
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
        [HttpGet]
        public async Task<ActionResult<List<ProductVM_EAV>>> GetProductByCategory(int categoryId)
        {
            // Lấy danh mục với categoryId truyền vào
            var category = await _unitOfWork.CategoryRepository.GetAll().AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy danh mục
            }

            List<int> categoryIds = new List<int>();

            // Nếu danh mục là cấp 1, lấy thêm các danh mục con cấp 2
            if (category.Level == 1)
            {
                var subCategories = await _unitOfWork.CategoryRepository.GetAll().AsQueryable()
                    .Where(c => c.ParentCategoryId == categoryId && c.Level == 2)
                    .ToListAsync();

                categoryIds.AddRange(subCategories.Select(c => c.Id));
            }

            // Thêm danh mục truyền vào vào danh sách Id
            categoryIds.Add(categoryId);

            // Lấy danh sách sản phẩm theo danh mục cấp 1 và cấp 2
            var products1 = _unitOfWork.ProductEAVRepository.GetAllCustom();
            var products = products1
                .Where(p => p.CategoryProducts.Any(cp => categoryIds.Contains(cp.CategoryId)))
                .ToList();
            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM_EAV>>(products);
                return Ok(productVms);
            }

            return NoContent();
        }
        private IEnumerable<Product_EAV> FilterProduct(ProductFilter filter)
        {
            var query = _unitOfWork.ProductEAVRepository.GetAllCustom().Where(x => x.Status != ProductStatus.stop && x.CategoryProducts.Any(p => p.Category.ParentCategoryId == filter.Cate));
            //if (filter.BrandId != null && filter.BrandId.Any() && filter.CateId != null && filter.CateId.Any())
            //{
            //	query = query.Where(x => filter.BrandId.Contains(x.BrandId.Value)&& x.CategoryProducts.Any(p=>filter.CateId.Contains(p.CategoryId)));
            //}else if (filter.BrandId != null && filter.BrandId.Any())
            //         {
            //             query = query.Where(x => filter.BrandId.Contains(x.BrandId.Value));
            //         }else if(filter.CateId != null && filter.CateId.Any())
            //         {
            //             query = query.Where(x => x.CategoryProducts.Any(p => filter.CateId.Contains(p.CategoryId)));
            //         }else if (filter.MinPrice != null && filter.MaxPrice != null)
            //         {
            //             var productVM = _mapper.Map<List<ProductVM>>(query);
            //             var result = productVM.Where(p=>p.DefaultPrice>=filter.MinPrice&& p.DefaultPrice<=filter.MaxPrice).ToList();
            //             query = _mapper.Map<List<Product>>(result);
            //         }
            if (filter.BrandId != null && filter.BrandId.Any())
            {
                query = query.Where(x => filter.BrandId.Contains(x.BrandId.Value));
            }
            if (filter.CateId != null && filter.CateId.Any())
            {
                query = query.Where(x => x.CategoryProducts.Any(p => filter.CateId.Contains(p.CategoryId)));
            }
            if (filter.MinPrice != null && filter.MaxPrice != null)
            {
                var productVM = _mapper.Map<List<ProductVM_EAV>>(query);
                var result = productVM.Where(p => p.Variants.FirstOrDefault(p => p.IsDefault == true).AfterDiscountPrice >= filter.MinPrice && p.Variants.FirstOrDefault(p => p.IsDefault == true).AfterDiscountPrice <= filter.MaxPrice).ToList();
                query = _mapper.Map<List<Product_EAV>>(result);
            }
            return query;
        }
        [HttpPost]
        public ActionResult GetProductByFilter([FromBody] ProductFilter productFilter)
        {
            var products = FilterProduct(productFilter).ToList();
            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM_EAV>>(products);
                return new JsonResult(productVms);
            }
            return new JsonResult(new object[] { });
        }
        [HttpGet]
        public ActionResult<ProductPaging> GetProductBySearch(string? search, int page = 1, int pageSize = 10)
        {
            var query = _unitOfWork.ProductEAVRepository.GetAll()
                .Where(p => p.ProductName.ToLower().Contains(search.ToLower())); // Phân trang không phân biệt chữ hoa chữ thường

            var totalRecords = query.Count(); // Tổng số bản ghi
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize); // Tính tổng số trang

            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList(); // Lấy danh sách sản phẩm cho trang hiện tại

            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM_EAV>>(products);

                var productPaging = new ProductPaging
                {
                    Items = productVms,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalRecord = totalRecords
                };

                return Ok(productPaging);
            }

            return NoContent(); // Trả về 204 nếu không tìm thấy sản phẩm nào
        }

        [HttpPost]
        public IActionResult GetProductPaging([FromBody] ProductPaging request)
        {
            ProductPaging partnerPaging = _unitOfWork.ProductEAVRepository.ProductPaging(request);
            return Ok(partnerPaging);
        }
        
    }
}

