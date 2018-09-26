using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class FeatureNameModel
    {
        public long FeatureId { get; set; }
        public Dictionary<string, string> FeatureNameDictionary { get; set; }
    }
}