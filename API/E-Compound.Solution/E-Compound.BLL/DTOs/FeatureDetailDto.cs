using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class FeatureDetailDto
    {
        public long FeatureDetailId { get; set; }
        public long FeatureId { get; set; }

        public Dictionary<string, string> DescriptionDictionary { get; set; }

        public bool IsFree { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        public long FeatureControlId { get; set; }
        public string Link { get; set; }
        public MemoryStream Image { get; set; }
        public bool IsImageChange { get; set; }
        public List<AvailableDto> Availables { get; set; }
    }
}
