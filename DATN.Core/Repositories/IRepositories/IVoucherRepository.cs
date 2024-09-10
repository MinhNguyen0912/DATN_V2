using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IVoucherRepository : IBaseRepository<Voucher>
    {
        List<Voucher> GetAllVouchers();
        VoucherPaging GetVoucherPaging(VoucherPaging request);
    }
}
