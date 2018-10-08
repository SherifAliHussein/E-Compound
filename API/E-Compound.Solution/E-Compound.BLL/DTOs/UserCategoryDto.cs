using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class UserCategoryDto
    {
        public long UserCategoryId { get; set; }
        public long CreationBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime ModifyTime { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime DeleteTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Dictionary<string, string> TitleDictionary { get; set; }

    }
}
