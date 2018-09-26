using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class FeatureModel
    {
        public long FeatureId { get; set; }
        public Dictionary<string, string> FeatureNameDictionary { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        //public bool HasDetails { get; set; }
        //public List<FeatureDetailModel> FeatureDetails { get; set; }
        public List<FeatureControlModel> FeatureControl { get; set; }

        public string ImageURL { get; internal set; }
        public bool IsImageChange { get; set; }

        public string Type { get; set; }
        public List<RestaurantNameModel> Restaurants { get; set; }
    }
}