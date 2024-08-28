using DATN.Core.Model.Product_EAV;

namespace DATN.Core.ViewModel.Product_EAV
{
    public class AttributeVM_EAV
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }

        // Navigation property
        public ICollection<AttributeValue_EAV> AttributeValues { get; set; }
    }
}
