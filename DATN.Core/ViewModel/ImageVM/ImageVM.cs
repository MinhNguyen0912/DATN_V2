using DATN.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace DATN.Core.ViewModel.ImagePath
{
    public class ImageVM
    {
        public int ImageId { get; set; }
        public int? TypeId { get; set; }
        public string? ImagePath { get; set; }
        public int? ProductId { get; set; }
        public bool IsDefault { get; set; }
    }

    public class CreateImageProductVM
    {
        public string? ImagePath { get; set; }
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsDefault { get; set; }
    }

    public class UpdateImageProductVM
    {
        public int ImageId { get; set; }
        public string? ImagePath { get; set; }
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsDefault { get; set; }
    }

}
