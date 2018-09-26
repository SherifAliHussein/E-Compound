using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class RestaurantNameDto
    {
        public long RestaurantId { get; set; }

        public Dictionary<string, string> RestaurantNameDictionary { get; set; }

    }
}
