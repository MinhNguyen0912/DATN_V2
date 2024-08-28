using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model.Product_EAV
{
    public class VariantAttribute
    {
        [Key]
        public int VariantAttributeId { get; set; }
        public int VariantId { get; set; }
        public int AttributeValueId { get; set; }

        // Navigation properties
        public Variant Variant { get; set; }
        public AttributeValue_EAV AttributeValue { get; set; }


    }
}
