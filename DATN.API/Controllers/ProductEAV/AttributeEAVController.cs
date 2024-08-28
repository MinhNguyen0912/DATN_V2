﻿using AutoMapper;
using DATN.Core.Infrastructures;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers.ProductEAV
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
    }
}
