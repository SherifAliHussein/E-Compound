using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.Common;

namespace E_Compound.BLL.DTOs
{
    public class FeatureControlDto
    {
        public long FeatureControlId { get; set; }
        public int Order { get; set; }
        public long FeatureId { get; set; }
        //public long ControlId { get; set; }
        public Enums.Control Control { get; set; }
        public Enums.ControlType ControlType { get; set; }
        //public List<FeatureDetailDto> FeatureDetails { get; set; }

    }
}
