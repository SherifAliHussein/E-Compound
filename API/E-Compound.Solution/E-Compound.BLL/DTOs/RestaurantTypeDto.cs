using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class RestaurantTypeDto
    {
        public long RestaurantTypeId { get; set; }
        public string TypeName { get; set; }
        public Dictionary<string, string> TypeNameDictionary { get; set; }
    }
}
