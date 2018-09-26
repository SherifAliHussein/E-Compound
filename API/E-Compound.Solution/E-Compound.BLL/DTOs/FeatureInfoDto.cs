using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class FeatureInfoDto
    {
        public long FeatureId { get; set; }
        public Dictionary<string, string> FeatureNameDictionary { get; set; }
        public List<FeatureControlDetailDto> FeatureControl { get; set; }
    }
}
