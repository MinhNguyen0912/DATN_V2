using DATN.Core.Model.Product_EAV;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.Product_EAV
{
    public class VariantAttributeVM
    {
        public int VariantAttributeId { get; set; }
        public int VariantId { get; set; }
        public int AttributeValueId { get; set; }

        // Navigation properties
        public Variant Variant { get; set; }
        public AttributeValue_EAV AttributeValue { get; set; }
    }
}
