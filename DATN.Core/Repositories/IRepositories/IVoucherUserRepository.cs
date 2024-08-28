using DATN.Core.Infrastructures;
using DATN.Core.Model;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IVoucherUserRepository : IBaseRepository<VoucherUser>
    {
        List<VoucherUser> GetVoucherByUser(Guid Id);
        public VoucherUser GetByIdCustom(int Id);

    }
}
