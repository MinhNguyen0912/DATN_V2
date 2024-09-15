using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.VoucherVM;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        List<Voucher> GetAllVouchers();
        VoucherPaging GetVoucherPaging(VoucherPaging request);
        public Task<string> GenerateVoucherAutoRegisterAsync(Guid userId);
        public Task<string> GenerateVoucherConditionAsync(Guid userId);
        //public Task GenerateVoucherActivationTimeAsync(DateTime activationTime);
        public Task<bool> IsCheckedTotalPuschasePriceAsync(Guid userId);
        public Task<bool> IsCheckedNumberBoughttAsync(Guid userId);
        List<Voucher> SearchVoucher(SearchVoucherRequest request);
        Voucher GetByIdCustom (int id);
        public Task<Voucher> AddVoucherForUserOffline(CreateVoucherOfflineVM createVoucherOfflineVM);

    }
}
