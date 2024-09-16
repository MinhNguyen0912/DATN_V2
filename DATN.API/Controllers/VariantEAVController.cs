using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VariantEAVController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VariantEAVController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int variantId)
        {
            var variant = _unitOfWork.VariantRepository.GetByIdCustom(variantId);
            return Ok(variant);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductId_Viet(int id)
        {
            var lstVariants = _unitOfWork.VariantRepository.GetByIdProduct(id);
            var result = lstVariants.Select(v => new
            {
                VariantId = v.VariantId,
                AttributeValueIds = v.VariantAttributes.Select(va => va.AttributeValue.AttributeValueId).ToList()
            }).ToList();
            return Ok(result);
        }

    }
}
