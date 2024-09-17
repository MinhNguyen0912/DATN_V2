using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Repositories.IRepositories.ProductEAV;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.Product_EAV;
using Microsoft.AspNetCore.Mvc;
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
        public List<Product_EAV> GetAll()
        {
            return Context.Product_EAVs.Include(p => p.Images).Include(b => b.Brand).Include(o => o.Origin).Include(p => p.CategoryProducts).ThenInclude(p => p.Category).Include(p => p.Variants).ToList();
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
            var filteredProduct = await queryabler.Where(p => p.ProductName.ToLower().Contains(name.ToLower())).Include(p => p.Variants.Where(c=>c.Quantity>0)).ThenInclude(p => p.VariantAttributes).Include(p=>p.Images).ToListAsync();
            return filteredProduct;

        }
        public Product_EAV GetByIdWithPromotion(int id)
        {
            return _context.Product_EAVs.Where(p => p.ProductId == id).Include(p => p.PromotionProducts).ThenInclude(p=>p.Promotion).FirstOrDefault();
        }

        public List<Product_EAV> GetAllCustom()
        {
            return Context.Product_EAVs.Include(p => p.Images).Include(p => p.CategoryProducts).ThenInclude(p=>p.Category).Include(p => p.Brand).Include(p => p.Variants).ToList();
        }
        public ProductPaging ProductPaging(ProductPaging request)
        {
            var query = Context.Product_EAVs.Include(p => p.Images).Include(b => b.Brand).Include(o => o.Origin).Include(p => p.CategoryProducts).ThenInclude(p => p.Category).Include(p => p.Variants).ToList().AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.ProductName.ToLower().Contains(searchTerm));
            }


            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<ProductVM_EAV>>(list);

            return request;
        }
    }

		//public ProductPaging ProductPaging([FromBody]ProductPaging request)
		//{
		//	var query = Context.Product_EAVs.Include(p => p.Images).Include(b => b.Brand).Include(o => o.Origin).Include(p => p.CategoryProducts).ThenInclude(p => p.Category).Include(p => p.Variants).ToList().AsQueryable();

		//	if (!string.IsNullOrEmpty(request.SearchTerm))
		//	{
		//		string searchTerm = request.SearchTerm.Trim().ToLower();
		//		query = query.Where(x => x.ProductName.ToLower().Contains(searchTerm));
		//	}

		//	request.TotalRecord = query.Count();
		//	request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
		//	var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
		//	request.Items = _mapper.Map<List<ProductVM_EAV>>(list);

		//	return request;
		//}
	}

