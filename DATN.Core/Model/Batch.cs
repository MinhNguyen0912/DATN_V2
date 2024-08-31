using DATN.Core.Enum;
using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Batch : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public VoucherType Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int Quantity { get; set; }
        public int QuantityUsed { get; set; }
        public List<Voucher> Vouchers { get; set; }


    }
}
