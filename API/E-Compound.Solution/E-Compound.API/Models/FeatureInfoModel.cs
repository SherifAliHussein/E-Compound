using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class FeatureInfoModel
    {
        public long FeatureId { get; set; }
        public Dictionary<string, string> FeatureNameDictionary { get; set; }
        public string ImageURL { get; internal set; }
        public List<FeatureControlDetailModel> FeatureControl { get; set; }
    }
}