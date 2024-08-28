using AutoMapper;
using DATN.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers.ProductEAV
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductAttributeEAVController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductAttributeEAVController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
