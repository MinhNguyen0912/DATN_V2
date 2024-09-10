using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModel.VoucherVM;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        private readonly IMapper _mapper;
        public VoucherRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<Voucher> GetAllVouchers()
        {
            return Context.Vouchers.Include(b=>b.Batch).Include(u=>u.User).ToList();
        }

        public Voucher GetByIdCustom(int id)
        {
            return Context.Vouchers.Include(b => b.Batch).Include(u => u.User).FirstOrDefault(x => x.Id == id);
        }

        public VoucherPaging GetVoucherPaging(VoucherPaging request)
        {
            var query = Context.Vouchers.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<VoucherVM>>(list);

            return request;
        }

        public List<Voucher> SearchVoucher(SearchVoucherRequest request)
        {
            var query = Context.Vouchers.Include(v=>v.User).Where(x=>x.BatchId == request.BatchId).ToList();
            if(request.Status != null)
            {
                query = query.Where(x => x.Status == request.Status).ToList();
            }
            if (request.UserName != null)
            {
                query = query.Where(x => x.User.UserName == request.UserName).ToList();
            }
            return query;
        }
    }


}
