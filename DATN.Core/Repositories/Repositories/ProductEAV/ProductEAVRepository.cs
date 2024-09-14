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

        public async Task<Product_EAV> GetByIdAsync(int id)
        {
            var prod=_context.Product_EAVs.Where(p=>p.ProductId==id).Include(p => p.Variants).FirstOrDefault();
            return prod;

        }

        public async Task<bool> UpdateQuantiyVariant(int ProductId, int VariantId, int Quantity)
        {
            try
            {
                var prodVariant = _context.Variants
                    .FirstOrDefault(c => c.ProductId == ProductId && c.VariantId == VariantId);
                if (prodVariant.Quantity - Quantity >= 0)
                {
                    prodVariant.Quantity -= Quantity;
                    _context.Variants.Update(prodVariant);
                    await  _context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public async Task<List<Product_EAV>> GetByName(string name)
        {
            var queryabler = _context.Product_EAVs.AsQueryable();
            var filteredProduct = await queryabler.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).Include(p => p.Variants).ThenInclude(p => p.VariantAttributes).ThenInclude(p=>p.AttributeValue).ThenInclude(p=>p.Attribute).Include(p=>p.Brand).Include(p=>p.Images).ToListAsync();
            return filteredProduct;

        }

        public Product_EAV GetByIdWithPromotion(int id)
        {
            return _context.Product_EAVs.Where(p => p.ProductId == id).Include(p => p.PromotionProducts).ThenInclude(p=>p.Promotion).FirstOrDefault();

        }
    }
}
