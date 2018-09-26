using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class CategoryNamesDTO
    {
        public long CategoryId { get; set; }
        public Dictionary<string, string> CategoryNameDictionary { get; set; }
        public int ItemCount { get; set; }
    }
}
