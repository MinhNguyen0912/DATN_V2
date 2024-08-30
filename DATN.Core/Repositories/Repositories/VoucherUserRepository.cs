using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{
    public class VoucherUserRepository : BaseRepository<VoucherUser>, IVoucherUserRepository
    {
        private readonly IMapper _mapper;
        public VoucherUserRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<VoucherUser> GetVoucherByUser(Guid Id)
        {
            return Context.VoucherUsers.Where(p => p.AppUserId == Id && p.IsDeleted == false).Include(p => p.Voucher).ToList();
        }
        public VoucherUser GetByIdCustom(int Id)
        {
            return Context.VoucherUsers.Where(p => p.Id == Id).Include(p => p.Voucher).FirstOrDefault();
        }
    }
}
