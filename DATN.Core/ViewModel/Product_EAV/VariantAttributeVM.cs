using DATN.Core.Model.Product_EAV;

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
