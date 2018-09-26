using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class PageDTO
    {
        public long CategoryId { get; set; }
        public long TemplateId { get; set; }
        public int PageNumber { get; set; }
    }
}
