using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product_EAV
{
    public class VariantAttribute
    {
        public int VariantAttributeId { get; set; }
        public int VariantId { get; set; }
        public int AttributeValueId { get; set; }

        // Navigation properties
        public Variant Variant { get; set; }
        public AttributeValue_EAV AttributeValue { get; set; }
        

    }
}
