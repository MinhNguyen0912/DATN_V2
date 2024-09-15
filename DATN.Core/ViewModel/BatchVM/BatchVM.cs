using DATN.Core.Enum;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.BatchVM
{
    public class BatchVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public VoucherType? Type { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ExpirationDate { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Voucher>? Vouchers { get; set; }
    }
}
