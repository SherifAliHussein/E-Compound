using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class UserCategoryTranslationDto
    {
        public long UserCategoryTranslationId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        public long UserCategoryId { get; set; }
        public UserCategoryDto UserCategory { get; set; }
    }
}
