using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class SupervisorDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<FeatureDto> Features { get; set; }
        public List<UserCategoryDto> UserCategories { get; set; }
    }
}
