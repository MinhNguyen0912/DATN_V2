using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Repositories.IRepositories.ProductEAV;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories.ProductEAV
{
    public class AttributeEAVRepository : BaseRepository<Attribute_EAV>, IAttributeEAVRepository
    {
        private readonly IMapper _mapper;
        public AttributeEAVRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<Attribute_EAV> GetAllAttributeValue()
        {
            return Context.Attribute_EAVs.Include(b => b.AttributeValues).ToList();
        }
    }
}
