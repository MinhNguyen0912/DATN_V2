using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.AttributeEAVVM;
using DATN.Core.ViewModel.AttributeVM.Viet_Attribute_VM;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttributeEAVController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;

        public AttributeEAVController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public IActionResult GetAttributePaging([FromBody] AttributesPaging request)
        {
            AttributesPaging Paging = _unitOfWork.AttributeEAVRepository.GetAttributePaging(request);
            return Ok(Paging);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attributes = _unitOfWork.AttributeEAVRepository.GetAllAttributeValue();

            if (attributes != null)
            {
                var attributeVMs = _mapper.Map<List<AttributeVM_EAV>>(attributes);
                return Ok(attributeVMs);
            }
            return BadRequest(new { Message = "Get dữ liệu không thành công" });
        }

        [HttpGet]
        [Route("api/attributes/{id}/values")]
        public async Task<IActionResult> GetAttributeValuesById(int id)
        {
            // Fetch the Attribute along with its AttributeValues using AttributeId
            var attribute = await _context.Attribute_EAVs
                                          .Include(a => a.AttributeValues)  // Eager loading of AttributeValues
                                          .FirstOrDefaultAsync(a => a.AttributeId == id);

            // If attribute is found
            if (attribute != null)
            {
                // Create a simplified response object containing the attribute's values
                var result = attribute.AttributeValues.Select(av => new
                {
                    ValueId = av.AttributeValueId,
                    ValueText = av.ValueText
                }).ToList();

                // Return 200 OK with the result as JSON
                return Ok(result);
            }

            // If no attribute is found for the given id, return a 404 Not Found status
            return NotFound(new { Message = "Attribute not found" });
        }
        [HttpGet("{id}")]
        public IActionResult GetAttributesWithValuesByProductId(int id)
        {
            // Lấy tất cả các giá trị thuộc tính liên quan đến sản phẩm
            var relatedAttributeValueIds = _context.Variants
                .Where(v => v.ProductId == id)
                .SelectMany(v => v.VariantAttributes)
                .Select(va => va.AttributeValueId)
                .Distinct()
                .ToList();

            // Lấy tất cả các thuộc tính có các giá trị thuộc tính liên quan
            var attributes = _context.Attribute_EAVs
                .Include(a => a.AttributeValues) // Bao gồm giá trị thuộc tính liên quan
                .Where(a => a.AttributeValues.Any(av => relatedAttributeValueIds.Contains(av.AttributeValueId)))
                .Select(a => new AttributeDTO
                {
                    AttributeId = a.AttributeId,
                    AttributeName = a.AttributeName,
                    AttributeValues = a.AttributeValues.Select(av => new AttributeValueDTO
                    {
                        AttributeValueId = av.AttributeValueId,
                        ValueText = av.ValueText
                    }).ToList()
                })
                .ToList();

            return Ok(attributes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAttributeEAVVM attributeVM)
        {
            if (attributeVM == null || string.IsNullOrWhiteSpace(attributeVM.Name))
            {
                return BadRequest("Attribute name is null or empty"); // 400 Bad Request
            }

            Attribute_EAV attribute_EAV = new Attribute_EAV() { AttributeName = attributeVM.Name };

            await _unitOfWork.AttributeEAVRepository.Create(attribute_EAV);
            _unitOfWork.SaveChanges();

            return Ok(new { success = true, message = "Attribute created successfully", attribute = attribute_EAV });
        }

    }
}
