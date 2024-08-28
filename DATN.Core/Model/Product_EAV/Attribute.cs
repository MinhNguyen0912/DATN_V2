using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product_EAV
{
    public class Attribute
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }

        // Navigation property
        public ICollection<AttributeValue> AttributeValues { get; set; }
    }
}
