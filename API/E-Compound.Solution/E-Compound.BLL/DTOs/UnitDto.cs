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
    }
}
