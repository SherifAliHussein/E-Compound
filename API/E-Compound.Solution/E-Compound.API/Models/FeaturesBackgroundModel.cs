using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class FeaturesBackgroundModel
    {
        public long FeaturesBackgroundId { get; set; }
        public long UserId { get; set; }
        public bool IsSelected { get; set; }
        public string ImageUrl { get; set; }
        public bool IsImageChange { get; set; }
    }
}