using DATN.Core.Model.Product_EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.Product_EAV
{
    public class SpecificationVM
    {
        public int Id { get; set; }
        public int VariantId { get; set; }
        public Variant Variant { get; set; }

        public string Key { get; set; } // Ví dụ: "Color", "Size", "Weight"
        public string Value { get; set; } // Giá trị tương ứng: "Red", "Medium", "1kg"
    }
}
