namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class CreateAttributeVM
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; }
        public bool Type { get; set; }

        public UpdateValueVM Value { get; set; }
    }
}
