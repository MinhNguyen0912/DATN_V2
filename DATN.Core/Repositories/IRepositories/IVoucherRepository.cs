using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModel.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        VoucherPaging GetVoucherPaging(VoucherPaging request);
       public Task CreateVoucherConditionAsync(AppUser user);
        public Task CreateVoucherAutoRgisterAsync(Guid UserId);
        public Task CreateVoucherActivationTimeAsync(DateTime ActivationTime);
    }
}
