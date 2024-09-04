using DATN.Core.Model.Product_EAV;
using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class PendingCart
    {
        public int PendingCartId { get; set; }
        public AppUser AppUser { get;  set; }
        public List<PendingCartVariant> PendingCartVariants { get;  set; }
    }
}
