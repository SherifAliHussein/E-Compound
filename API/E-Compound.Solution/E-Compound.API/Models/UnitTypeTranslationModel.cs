using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class UnitTypeTranslationModel
    {
        public long UnitTypeTranslationId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public long UnitTypeId { get; set; }
        public UnitTypeModel UnitType { get; set; }
    }
}