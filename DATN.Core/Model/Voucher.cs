using DATN.Core.Enum;
using DATN.Core.Models;
namespace DATN.Core.Model
{
    public class Voucher : BaseEntity
    {
        public string? Code { get; set; } // Mã voucher
        public VoucherStatus Status { get; set; } // Trạng thái voucher
        public DateTime? ReleaseDate { get; set; } //Ngày phát hành voucher cho người dùng       
        public DateTime? ExpiryDate { get; set; } // Ngày hết hạn của voucher
        public int? BatchId { get; set; }           
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }
        public Batch? Batch { get; set; }        
    }
}
