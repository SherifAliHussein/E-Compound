using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class CategoryPageTemplateDTO
    {
        public long RestaurantId { get; set; }
        public long MenuId { get; set; }
        public long CategoryId { get; set; }
        public Dictionary<string, string> MenuNameDictionary { get; set; }
        public Dictionary<string, string> CategoryNameDictionary { get; set; }
        public List<PageTemplateDTO> Templates { get; set; }
    }
}
