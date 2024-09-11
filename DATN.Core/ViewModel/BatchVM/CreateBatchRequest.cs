using DATN.Core.Enum;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.BatchVM
{
    using System.ComponentModel.DataAnnotations;

    public class CreateBatchRequest
    {
        [Required(ErrorMessage = "Tên đợt phát hành là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên đợt phát hành không được vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Loại giảm giá là bắt buộc.")]
        public VoucherType? Type { get; set; }

        [Required(ErrorMessage = "Kiểu giảm giá là bắt buộc.")]
        public DiscountType DiscountType { get; set; }
        [Required(ErrorMessage = "Số tiền giảm giá là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền giảm giá phải là số dương.")]
        public decimal? DiscountAmount { get; set; }
        [Required(ErrorMessage = "Số tiền đơn hàng tối thiểu là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Số tiền đơn hàng tối thiểu phải là số dương.")]
        public decimal? MinOrderAmount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số tiền giảm giá tối đa phải là số dương.")]
        public decimal? MaxDiscountAmount { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Ngày bắt đầu phải hợp lệ.")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Ngày kết thúc phải hợp lệ.")]
        public DateTime? EndDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Thời gian hết hạn phải là số nguyên dương.")]
        public int? ExpirationDate { get; set; }

        public bool? IsActive { get; set; } = true;

        [Required(ErrorMessage = "Phải chọn ít nhất một danh mục.")]
        public List<int> CateItem { get; set; } = new List<int>();

        [Required(ErrorMessage = "Phải chọn ít nhất một sản phẩm.")]
        public List<int> ProdItem { get; set; } = new List<int>();

        public bool ApplyToAll { get; set; }

        // Custom validation to ensure either the validity period or expiration period is specified
        public bool IsValidPeriod
        {
            get
            {
                // Check if either StartDate/EndDate is set or ExpirationDate is set
                return (StartDate.HasValue && EndDate.HasValue) || ExpirationDate.HasValue;
            }
        }

        // Custom validation attribute to validate the IsValidPeriod property
        [Required(ErrorMessage = "Bạn phải chọn một loại thời gian áp dụng.")]
        public bool ValidateTimePeriod
        {
            get
            {
                // Validate based on the custom logic for the time period selection
                return IsValidPeriod;
            }
        }
        // Method to set default values for FreeShip type
        public void ApplyDefaultValuesForFreeShip()
        {
            if (Type == VoucherType.freeship)
            {
                DiscountType = DiscountType.Percent;
                DiscountAmount = 100;
            }
        }
    }

}
