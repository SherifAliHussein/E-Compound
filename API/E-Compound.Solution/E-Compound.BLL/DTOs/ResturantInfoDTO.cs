using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class ResturantInfoDto
    {

        public long BackgroundId { get; set; }
        public string BackgroundUrl { get; set; }
        public long ResturentId { get; set; }
        public string LogoUrl { get; set; }
        public string RestaurantName { get; set; }
        public int Rate { get; set; }

    }
}
