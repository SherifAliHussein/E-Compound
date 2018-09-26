using E_Compound.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Enums.RoleType Role { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
