using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace E_Compound.API.Models
{
    public class CategoryModel
    {
        public long CategoryId { get; set; }
        public long MenuId { get; set; }
        public long RestaurantId { get; set; }
        public string CategoryName { get; set; }
        public Dictionary<string, string> CategoryNameDictionary { get; set; }
        public bool IsActive { get; set; }
        public string ImageURL { get; set; }
        public bool IsImageChange { get; set; }

        public CategoryPageTemplateModel CategoryPageTemplateModel { get; set; }
        
    }
}