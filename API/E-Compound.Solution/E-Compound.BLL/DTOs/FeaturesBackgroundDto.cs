using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class FeaturesBackgroundDto
    {
        public long FeaturesBackgroundId { get; set; }
        public MemoryStream Image { get; set; }
        public long UserId { get; set; }
        public bool IsSelected { get; set; }
    }
}
