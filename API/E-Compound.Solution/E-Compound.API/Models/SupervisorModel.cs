using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class SupervisorModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<UserCategoryModel> UserCategories { get; set; }
        public List<FeatureModel> Features { get; set; }
    }
}