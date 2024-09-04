namespace DATN.Client.ViewModel
{
    public class AttributeGroupViewModel
    {
        public string AttributeName { get; set; }
        public int AttributeId { get; set; }
        public List<AttributeValueViewModel> AttributeValues { get; set; }
    }
    public class AttributeValueViewModel
    {
        public int AttributeValueId { get; set; }
        public string AttributeValue { get; set; }
    }
}
