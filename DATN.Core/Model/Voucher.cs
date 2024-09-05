using DATN.Core.Enum;
using DATN.Core.Models;
namespace DATN.Core.Model
{
    public class Voucher : BaseEntity
    {
        public string? Code { get; set; }
        public VoucherStatus Status { get; set; }
        public DateTime? ReleaseDate { get; set; }        
        public DateTime? ExpiryDate { get; set; }
        public int? BatchId { get; set; }           
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public Batch? Batch { get; set; }        
    }
}
