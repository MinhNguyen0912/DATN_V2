using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.Paging;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IInvoiceRepository : IBaseRepository<Invoice>
    {
        List<Invoice> GetInvoiceByUserId(Guid userId);
        //public Invoice GetByIdCustom(int id);
        public InvoicePaging GetInvoicePaging(InvoicePaging request);
        public List<InvoiceShowForClientVM> GetInvoiceByStatusAndUserId(Guid userId, InvoiceStatus status);
        public Invoice GetByIdCustom( int id);
        public List<Invoice> GetCustom();

    }
}
