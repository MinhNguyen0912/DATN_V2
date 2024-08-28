using DATN.Core.ViewModel.AttributeVM.AttributeDynamic;
using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.AttributeVM.AtttributeVariation
{
    public class CreateVariantProductVM
    {
        [Required(ErrorMessage = "Không được để trống")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public List<CreateVLVariantVM> Value { get; set; }
    }
}
