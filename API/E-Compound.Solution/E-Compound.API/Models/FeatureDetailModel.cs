using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class FeatureDetailModel
    {
        public long FeatureDetailId { get; set; }
        public long FeatureId { get; set; }

        public Dictionary<string, string> DescriptionDictionary { get; set; }

        public bool IsFree { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public long FeatureControlId { get; set; }

        public string Link { get; set; }
        public string ImageURL { get; internal set; }
        public bool IsImageChange { get; set; }
        public List<AvailableModel> Availables { get; set; }
    }
}