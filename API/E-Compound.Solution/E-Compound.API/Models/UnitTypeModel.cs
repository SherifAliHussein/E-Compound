using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class UnitTypeModel
    {
        public long UnitTypeId { get; set; }
        public List<UnitTypeTranslationModel> UnitTypeTranslations { get; set; }
        public List<UnitModel> Units { get; set; }
        public int Limit { get; set; }
        public long CreationBy { get; set; }
        public AdminModel Creater { get; set; }
        public DateTime CreateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public AdminModel Modifier { get; set; }
        public DateTime ModifyTime { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
    }
}