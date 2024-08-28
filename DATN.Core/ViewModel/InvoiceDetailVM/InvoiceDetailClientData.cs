namespace DATN.Core.ViewModel.InvoiceDetailVM
{
    public class InvoiceDetailClientData
    {
        public int InvoiceDetailId { get; set; }
        public Product_EAV.ProductVM_EAV Product { get; set; }
        public int Quantity { get; set; }
    }
}
