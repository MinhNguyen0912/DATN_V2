using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Repositories.IRepositories.ProductEAV;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories.ProductEAV
{
    public class VariantRepository : BaseRepository<Variant>, IVariantRepository
    {
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;
        public VariantRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        public Variant GetByIdCustom(int variantId)
        {
            return _context.Variants.Where(p => p.VariantId == variantId).Include(p => p.Specifications).Include(p=>p.Product).ThenInclude(p=>p.Images).FirstOrDefault();
        }
    }
}
