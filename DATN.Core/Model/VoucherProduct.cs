using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class VoucherProduct : BaseEntity
    {
        public int VoucherId { get; set; }
        public int ProductId { get; set; }
        public Voucher Voucher { get; set; }
        public Product_EAV.Product_EAV Product { get; set; }
    }
}
