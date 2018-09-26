using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class ControlModel
    {
        public long ControlId { get; set; }
        public Dictionary<string, string> NameDictionary { get; set; }
    }
}