﻿using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Repositories.IRepositories.ProductEAV;

namespace DATN.Core.Repositories.Repositories.ProductEAV
{
    public class AttributeValueEAVRepository : BaseRepository<AttributeValue_EAV>, IAttributeValueEAVRepository
    {
        private readonly IMapper _mapper;
        public AttributeValueEAVRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
