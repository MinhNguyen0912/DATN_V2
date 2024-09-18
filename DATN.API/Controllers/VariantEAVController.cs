using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.ViewModel.ProdutEAVVM;
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
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int productId, [FromBody] List<CreateVariantsVM> request)
        {
            foreach (var variant in request)
            {
                Variant newVariant = new Variant
                {
                    ProductId = productId,
                    VariantName = variant.Name,
                    Quantity = variant.Quantity,
                    PuscharPrice = variant.PuscharPrice,
                    SalePrice = variant.SalelPrice,
                    AfterDiscountPrice = variant.AfterDiscountPrice,
                    IsDefault = variant.IsDefault,
                    MaximumQuantityPerOrder = variant.MaximumQuantityPerOrder,
                    Weight = variant.Weight,
                };

                var createdVariant = _unitOfWork.VariantRepository.Create(newVariant);
                _unitOfWork.SaveChanges();
                if (createdVariant == null)
                {
                    throw new Exception("Failed to create variant.");
                }

                var variantId = newVariant.VariantId;

                // Tách attributeValueIds và tạo VariantAttributes
                var attributeValueIds = variant.attributeValueIds.Split('/').Select(int.Parse).ToList();
                foreach (var attributeValueId in attributeValueIds)
                {
                    VariantAttribute variantAttribute = new VariantAttribute
                    {
                        VariantId = variantId,
                        AttributeValueId = attributeValueId
                    };
                    var createdVariantAttribute = _unitOfWork.VariantAttributeRepository.Create(variantAttribute);
                    _unitOfWork.SaveChanges();
                    if (createdVariantAttribute == null)
                    {
                        throw new Exception("Failed to create variant attribute.");
                    }
                }

                // Tạo Specifications cho từng Variant
                if (variant.Specifications != null)
                {
                    foreach (var spec in variant.Specifications)
                    {
                        Specification specification = new Specification
                        {
                            VariantId = variantId,
                            Key = spec.Name,
                            Value = spec.Value
                        };
                        var createdSpecification = _unitOfWork.SpecificationRepository.Create(specification);
                        _unitOfWork.SaveChanges();
                        if (createdSpecification == null)
                        {
                            throw new Exception("Failed to create specification.");
                        }
                    }
                }
            }
            return Ok();
        }

    }
}


