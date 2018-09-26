using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class UserHubModels
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
        public string Message { get; set; }
    }
}