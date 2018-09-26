using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class CategoryNameModel
    {
        public long CategoryId { get; set; }
        public Dictionary<string, string> CategoryNameDictionary { get; set; }
        public int ItemCount { get; set; }
    }
}