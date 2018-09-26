using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class BranchModel
    {
        public long BranchId { get; set; }
        public string BranchTitle { get; set; }
        public Dictionary<string, string> BranchTitleDictionary { get; set; }
        public string BranchAddress { get; set; }
        public Dictionary<string, string> BranchAddressDictionary { get; set; }
        public bool IsActive { get; set; }
    }
}