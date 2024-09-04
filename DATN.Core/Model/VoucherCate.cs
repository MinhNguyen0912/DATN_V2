using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class VoucherCate : BaseEntity
    {
        public int BatchId { get; set; }
        public int CategoryId { get; set; }
        public Batch Batch { get; set; }
        public Category Category { get; set; }
    }
}
