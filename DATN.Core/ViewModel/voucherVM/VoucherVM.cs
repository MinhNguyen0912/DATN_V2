using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.voucherVM
{
    public class VoucherVM
    {
        public int Id { get; set; }
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
