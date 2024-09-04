using DATN.Core.Model.Product_EAV;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.ViewModel.Product_EAV;

namespace DATN.Core.ViewModel.PendingCartVariantVM
{
    public class PendingCartVariantVM
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public VariantVM Variant { get; set; }
        public int PendingCartId { get; set; }
        public int Quantity { get; set; }
        public PendingCartVM.PendingCartVM PendingCart { get; set; }
    }
}
