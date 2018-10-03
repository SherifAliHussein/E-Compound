using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class UserCategoryModel
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