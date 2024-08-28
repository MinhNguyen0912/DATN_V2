using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.InvoiceDetailVM;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IInvoiceDetailRepository : IBaseRepository<InvoiceDetail>
    {
        public List<InvoiceDetailForCommentVM> GetInvoiceDetailByInvoiceId(int invoiceId, Guid userId);
    }
}
