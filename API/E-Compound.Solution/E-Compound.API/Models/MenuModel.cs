using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class MenuModel
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public Dictionary<string, string> MenuNameDictionary { get; set; }
        public bool IsActive { get; set; }
        public string ImageURL { get; set; }
        public bool IsImageChange { get; set; }
        public long RestaurantId { get; set; }

        public List<CategoryModel> CategoryModels { get; set; }
    }
}