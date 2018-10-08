using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class UnitDto
    {
        public long UnitId { get; set; }
        public long UnitTypeId { get; set; }
        public  UnitTypeDto UnitType { get; set; }
        public string Name { get; set; }
        public long CreationBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public bool IsLimit { get; set; }
    }
}
