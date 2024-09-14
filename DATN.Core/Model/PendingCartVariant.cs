using DATN.Core.Model.Product_EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class PendingCartVariant
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public Variant? Variant { get; set; }
        public int? PendingCartId { get; set; }
        public int Quantity { get; set; }
        public PendingCart? PendingCart { get; set; }
    }
}
