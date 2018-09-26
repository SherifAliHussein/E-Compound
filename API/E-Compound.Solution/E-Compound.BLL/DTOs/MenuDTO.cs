using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class MenuDTO
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public Dictionary<string, string> MenuNameDictionary { get; set; }
        public bool IsActive { get; set; }
        public MemoryStream Image { get; set; }
        public bool IsImageChange { get; set; }
        public long RestaurantId { get; set; }

    }
}
