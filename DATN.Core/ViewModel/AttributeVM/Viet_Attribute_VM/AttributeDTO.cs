using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.Viet_Attribute_VM
{
        public class AttributeDTO
        {
            public int AttributeId { get; set; }
            public string AttributeName { get; set; }
            public List<AttributeValueDTO> AttributeValues { get; set; }
        }

}
