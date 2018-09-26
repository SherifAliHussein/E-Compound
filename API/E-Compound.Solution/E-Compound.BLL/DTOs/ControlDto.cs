using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class ControlDto
    {
        public long ControlId { get; set; }
        public Dictionary<string, string> NameDictionary { get; set; }

    }
}
