using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product_EAV;
using DATN.Core.Repositories.IRepositories.ProductEAV;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.Product_EAV;
using DATN.Core.ViewModels.Paging;
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
        public AttributesPaging GetAttributePaging(AttributesPaging request)
        {
            var query = Context.Attribute_EAVs.Include(b => b.AttributeValues).ToList();

            //if (!string.IsNullOrEmpty(request.SearchTerm))
            //{
            //    string searchTerm = request.SearchTerm.Trim().ToLower();
            //    query = query.Where(x => x.AttributeName.Contains(searchTerm));
            //}

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<AttributeVM_EAV>>(list);

            return request;
        }
    }
}
