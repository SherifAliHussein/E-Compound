using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class UserCategoryTranslationModel
    {
        public long UserCategoryTranslationId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public UserCategoryModel UserCategory { get; set; }
    }
}