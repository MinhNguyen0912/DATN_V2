using DATN.Core.Enum;
using DATN.Core.ViewModels.Paging;

namespace DATN.Core.ViewModel.Paging
{
    public class InvoicePaging : PagingRequestBase<InvoiceVM.InvoiceVM>
    {
        public InvoiceStatus Status { get; set; } = InvoiceStatus.All;
    }
}
