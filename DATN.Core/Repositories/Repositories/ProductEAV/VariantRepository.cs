using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Repositories.IRepositories.ProductEAV;

namespace DATN.Core.Repositories.Repositories.ProductEAV
{
    public class VariantRepository : BaseRepository<Variant>, IVariantRepository
    {
        private readonly IMapper _mapper;
        public VariantRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
