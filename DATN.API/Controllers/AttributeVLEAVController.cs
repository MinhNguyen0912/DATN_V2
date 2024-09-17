using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.AttributeValueEAVVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttributeVLEAVController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;
        public AttributeVLEAVController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Create(List<CreateAttributeValueEAVVM> attributeVLVM)
        {
            if (attributeVLVM == null || !attributeVLVM.Any())
            {
                return BadRequest("Dữ liệu không hợp lệ"); // 400 Bad Request
            }

            foreach (var value in attributeVLVM)
            {
                // Kiểm tra giá trị đã tồn tại chưa
                var existingValue = await _context.AttributeValue_EAVs
                                                  .FirstOrDefaultAsync(v => v.AttributeId == value.AttributeId
                                                                         && v.ValueText == value.ValueText);
                if (existingValue != null)
                {
                    return BadRequest($"Giá trị '{value.ValueText}' đã tồn tại cho thuộc tính này");
                }

                AttributeValue_EAV attribute_EAV = new AttributeValue_EAV()
                {
                    AttributeId = value.AttributeId,
                    ValueText = value.ValueText,
                };

                await _context.AttributeValue_EAVs.AddAsync(attribute_EAV);
            }

            await _unitOfWork.SaveAsync();

            return Ok(attributeVLVM); // 201 Created
        }

    }
}
