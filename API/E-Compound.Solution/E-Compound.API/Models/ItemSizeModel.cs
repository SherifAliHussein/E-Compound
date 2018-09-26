using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class ItemSizeModel
    {
        public long ItemSizeId { get; set; }
        public long SizeId { get; set; }
        public string SizeName { get; set; }
        public Dictionary<string, string> SizeNameDictionary { get; set; }
        public double Price { get; set; }
    }
}