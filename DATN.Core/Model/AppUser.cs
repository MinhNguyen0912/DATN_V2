using DATN.Core.Model;
using Microsoft.AspNetCore.Identity;

namespace DATN.Core.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool isActive { get; set; }
        public bool IsSentMail { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;


        // Navigation property
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Voucher>? Vouchers { get; set; }
        public ICollection<ShippingOrder>? ShippingOrders { get; set; }
    }
}
