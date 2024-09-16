﻿using DATN.Core.ViewModel.Product_EAV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProdutEAVVM
{
    public class CreateVariantsVM
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal ImportPrice { get; set; }
        public decimal SellPrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public int YearOfManufacture { get; set; }
        public bool IsDefault { get; set; }
        public string attributeValueIds { get; set; }
        // List of specifications for each variant
        public List<CreateSpecificationsVM> Specifications { get; set; } = new List<CreateSpecificationsVM>();
    }
}
