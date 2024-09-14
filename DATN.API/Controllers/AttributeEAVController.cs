using AutoMapper;
using DATN.Core.Infrastructures;
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

        public AttributeEAVController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
    }
}
