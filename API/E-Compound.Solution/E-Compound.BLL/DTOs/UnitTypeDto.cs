using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class UnitTypeDto
    {
        public long UnitTypeId { get; set; }
        
        public List<UnitDto> Units { get; set; }
        public int Limit { get; set; }
        public long CreationBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }
    }
}
