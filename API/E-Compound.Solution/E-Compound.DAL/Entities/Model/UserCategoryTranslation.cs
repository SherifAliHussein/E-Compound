using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class UserCategoryTranslation : Entity
    {
        public long UserCategoryTranslationId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        [ForeignKey("UserCategory")]
        public long UserCategoryId { get; set; }
        public virtual UserCategory UserCategory { get; set; }

    }
}
