﻿using DATN.Core.Enum;
using DATN.Core.Models;

namespace DATN.Core.Model
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public InvoiceStatus Status { get; set; }
        //public decimal? TotalAmount { get; set; }
        //public decimal? Discount { get; set; }
        public decimal? FinalAmount { get; set; }

        // Foreign Key
        public Guid UserId { get; set; }
        // Navigation property
        public AppUser User { get; set; }
        // Navigation property
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        // Navigation property
        public PaymentInfo? PaymentInfo { get; set; }
        public Voucher? Voucher { get; set; }
        public string? Note { get; set; }
        public int? VoucherId { get; set; }
        public List<ShippingOrder>? ShippingOrders { get; set; }


    }
}
