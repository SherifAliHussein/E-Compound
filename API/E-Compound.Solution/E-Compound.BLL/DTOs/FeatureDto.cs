using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.Common;

namespace E_Compound.BLL.DTOs
{
    public class FeatureDto
    {
        public long FeatureId { get; set; }
        public Dictionary<string,string> FeatureNameDictionary{ get; set; }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        //public bool HasDetails { get; set; }
        public MemoryStream Image { get; set; }
        public bool IsImageChange { get; set; }

        //public List<FeatureDetailDto> FeatureDetails { get; set; }
        public List<FeatureControlDto> FeatureControl { get; set; }
        public Enums.FeatureType Type { get; set; }
        public List<RestaurantNameDto> Restaurants { get; set; }
    }
}
