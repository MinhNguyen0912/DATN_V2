using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Repositories.IRepositories.ProductEAV;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories.ProductEAV
{
    public class ProductEAVRepository : BaseRepository<Product_EAV>, IProductEAVRepository
    {
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;
        public ProductEAVRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        public Product_EAV GetByIdCustom(int id)
        {
            return _context.Product_EAVs.Where(p=>p.ProductId==id).Include(p => p.Variants).ThenInclude(p => p.VariantAttributes).ThenInclude(p=>p.AttributeValue).ThenInclude(p=>p.Attribute).Include(p=>p.Brand).Include(p=>p.Origin).Include(p=>p.Images).FirstOrDefault();
        }
        public Product_EAV GetByIdWithPromotion(int id)
        {
            return _context.Product_EAVs.Where(p => p.ProductId == id).Include(p => p.PromotionProducts).ThenInclude(p=>p.Promotion).FirstOrDefault();
        }
    }
}
