using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace E_Compound.BLL.DTOs
{
    public class RestaurantDTO
    {
        public long RestaurantId { get; set; }
        public string RestaurantAdminUserName { get; set; }
        public string RestaurantAdminPassword { get; set; }

        public string RestaurantName { get; set; }
        public Dictionary<string, string> RestaurantNameDictionary { get; set; }
        public string RestaurantDescription { get; set; }
        public Dictionary<string, string> RestaurantDescriptionDictionary { get; set; }

        public long RestaurantTypeId { get; set; }
        public HttpPostedFile Logo { get; set; }
        public string RestaurantTypeName { get; set; }
        public Dictionary<string, string> RestaurantTypeNameDictionary { get; set; }

        public bool IsActive { get; set; }
        public MemoryStream Image { get; set; }
        public bool IsLogoChange { get; set; }
        public bool IsReady { get; set; }
       // public int WaitersLimit { get; set; }
       //public int ConsumedWaiters { get; set; }
    }
}
