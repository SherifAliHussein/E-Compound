using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class RestaurantWaiterDTO
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string> BranchTitleDictionary { get; set; }
        public long BranchId { get; set; }

        public string Start { get; set; }
        public string End { get; set; }
        //public long RestaurantId { get; set; }
    }
}
