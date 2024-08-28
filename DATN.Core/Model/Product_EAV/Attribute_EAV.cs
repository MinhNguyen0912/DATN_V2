using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model.Product_EAV
{
    public class Attribute_EAV
    {
        [Key]
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }

        // Navigation property
        public ICollection<AttributeValue_EAV> AttributeValues { get; set; }
    }
}
