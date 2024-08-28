using DATN.Core.ViewModel.InvoiceDetailVM;

namespace DATN.Core.ViewModel.InvoiceVM
{
    public class InvoiceResponse
    {
        public int? InvoiceId { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public List<InvoiceDetailClientData> InvoiceDetails { get; set; }
    }
}
