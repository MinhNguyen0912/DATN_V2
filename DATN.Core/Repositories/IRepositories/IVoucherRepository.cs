using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.VoucherVM;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        List<Voucher> GetAllVouchers();
        VoucherPaging GetVoucherPaging(VoucherPaging request);
        List<Voucher> SearchVoucher(SearchVoucherRequest request);
        Voucher GetByIdCustom (int id);
    }
}
