using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class RestaurantTypeModel
    {
        public long RestaurantTypeId { get; set; }
        public string TypeName { get; set; }

        public Dictionary<string,string> TypeNameDictionary { get; set; }
    }
}