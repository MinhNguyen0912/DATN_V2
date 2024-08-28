using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public string? ImageUrl { get; set; }
        public ICollection<Product_EAV.Product_EAV>? ProductEAVs { get; set; }
    }
}
