using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model
{
    public class CategoryProduct
    {
        [Key]
        public int CategoryProductId { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int ProductId { get; set; }
        public Product_EAV.Product_EAV? Product { get; set; }
    }
}
