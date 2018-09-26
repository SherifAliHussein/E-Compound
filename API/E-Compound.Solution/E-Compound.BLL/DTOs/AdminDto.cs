using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class AdminDto
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid UserAccountId { get; set; }
        public int Limit { get; set; }
        public Guid PackageGuid { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsActive { get; set; }
        //public long ProductId { get; set; }
    }
}
