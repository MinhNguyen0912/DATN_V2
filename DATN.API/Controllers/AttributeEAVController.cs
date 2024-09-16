using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.AttributeVM.Viet_Attribute_VM;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.Product_EAV;
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
        public IActionResult GetAttributeValueByProductId(int id)
        {
            // Lấy dữ liệu từ cơ sở dữ liệu và ánh xạ vào các lớp DTO
            var attributes = _context.AttributeValue_EAVs
        .Include(av => av.Attribute) // Bao gồm thuộc tính liên quan
        .Where(av => _context.Variants
            .Where(v => v.ProductId == id)
            .SelectMany(v => v.VariantAttributes)
            .Select(va => va.AttributeValueId)
            .Contains(av.AttributeValueId))
        .GroupBy(av => av.Attribute)
        .Select(g => new AttributeDTO
        {
            AttributeId = g.Key.AttributeId,
            AttributeName = g.Key.AttributeName,
            AttributeValues = g.Select(av => new AttributeValueDTO
            {
                AttributeValueId = av.AttributeValueId,
                ValueText = av.ValueText
            }).ToList()
        })
        .ToList();

            return Ok(attributes);
        }




    }
}
