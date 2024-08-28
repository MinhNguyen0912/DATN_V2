using DATN.Core.Enum;

namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class CreateValueVM
    {
        public string Value { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsShow { get; set; } = true;
        public ValuesType ValuesType { get; set; } = ValuesType.dynamic;
    }
}
