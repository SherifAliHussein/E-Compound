using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class CategoryPageTemplateModel
    {
        public long RestaurantId { get; set; }
        public long MenuId { get; set; }
        public long CategoryId { get; set; }
        public Dictionary<string, string> MenuNameDictionary { get; set; }
        public string MenuImageURL { get; set; }
        public Dictionary<string, string> CategoryNameDictionary { get; set; }
        public string CategoryImageURL{ get; set; }
        public List<PageTemplateModel> Templates { get; set; }
    }
}