using DATN.Core.Model.Product_EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.Product_EAV
{
    public class AttributeValueVM_EAV
    {
        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string ValueText { get; set; }

        // Navigation property
        public Attribute_EAV Attribute { get; set; }
        public ICollection<VariantAttribute> VariantAttributes { get; set; }
    }
}
