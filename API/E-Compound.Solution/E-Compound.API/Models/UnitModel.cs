using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class UnitModel
    {
        public long UnitId { get; set; }
        public long UnitTypeId { get; set; }
        public UnitTypeModel UnitType { get; set; }
        public string Name { get; set; }
    }
}