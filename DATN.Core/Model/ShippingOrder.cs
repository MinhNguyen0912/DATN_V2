using DATN.Core.Models;

namespace DATN.Core.Model
{
    public class ShippingOrder : BaseEntity
    {
        public string? OrderCode { get; set; }
        public double Price { get; set; }
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
