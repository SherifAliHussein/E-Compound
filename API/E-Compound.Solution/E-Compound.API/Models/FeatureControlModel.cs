using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Compound.Common;

namespace E_Compound.API.Models
{
    public class FeatureControlModel
    {
        public long FeatureControlId { get; set; }
        public int Order { get; set; }
        public long FeatureId { get; set; }
        //public long ControlId { get; set; }
        //public ControlModel Control { get; set; }
        public Enums.Control Control { get; set; }

        public string ControlType { get; set; }
        //public List<FeatureDetailModel> FeatureDetails { get; set; }
    }
}