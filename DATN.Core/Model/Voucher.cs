using DATN.Core.Enum;
using DATN.Core.Models;
namespace DATN.Core.Model
{
    public class Voucher : BaseEntity
    {
        public string Code { get; set; }
        public decimal? MinOrderAmount { get; set; }    
        public decimal? MaxDiscountAmount { get; set; }
        public VoucherStatus Status { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int? BatchId { get; set; }    
        public List<VoucherCate>? VoucherCates { get; set; } = new List<VoucherCate>();
        public List<VoucherProduct>? VoucherProducts { get; set; } = new List<VoucherProduct>();
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public Batch Batch { get; set; }        
    }
}
