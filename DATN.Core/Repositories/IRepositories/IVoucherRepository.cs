using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModel.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        List<Voucher> GetAllVouchers();
        VoucherPaging GetVoucherPaging(VoucherPaging request);
        public Task<string> CreateVoucherAutoRegisterAsync(Guid userId);
        public Task<string> GenarateVoucherConditionAsync(Guid userId);
        public Task CreateVoucherActivationTimeAsync(DateTime activationTime);
        public Task<bool> IsCheckedTotalPuschasePriceAsync(Guid userId);
        public Task<bool> IsCheckedNumberBoughttAsync(Guid userId);
    }
}
