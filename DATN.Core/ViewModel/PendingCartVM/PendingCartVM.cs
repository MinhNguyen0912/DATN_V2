using DATN.Core.Model;
using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.PendingCartVM
{
    public class PendingCartVM
    {
        public int PendingCartId { get; set; }
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<PendingCartVariantVM.PendingCartVariantVM> PendingCartVariants { get; set; }
    }
}
