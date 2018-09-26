using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class RestaurantNameModel
    {
        public long RestaurantId { get; set; }
        public Dictionary<string, string> RestaurantNameDictionary { get; set; }
        public string ImageURL { get; set; }
    }
}