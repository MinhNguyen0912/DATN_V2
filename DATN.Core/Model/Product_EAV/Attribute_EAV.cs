﻿using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product_EAV
{
    public class Attribute_EAV
    {
        [Key]
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }

        // Navigation property
        public ICollection<AttributeValue_EAV> AttributeValues { get; set; }
    }
}
