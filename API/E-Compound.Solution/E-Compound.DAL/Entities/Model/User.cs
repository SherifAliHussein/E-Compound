using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;
using E_Compound.Common;

namespace E_Compound.DAL.Entities.Model
{
    public class User:Entity
    {
        [Key]
        public long UserId { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public Enums.RoleType Role { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
    }
}
