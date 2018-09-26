using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class ReceptionistModel
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}