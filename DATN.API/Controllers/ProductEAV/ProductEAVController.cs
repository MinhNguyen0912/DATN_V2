using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers.ProductEAV
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
    }
}
