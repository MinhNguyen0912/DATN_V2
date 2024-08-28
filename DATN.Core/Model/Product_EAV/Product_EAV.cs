﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product_EAV
{
    public class Product_EAV
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        // Navigation property
        public ICollection<Variant> Variants { get; set; }
    }
}
