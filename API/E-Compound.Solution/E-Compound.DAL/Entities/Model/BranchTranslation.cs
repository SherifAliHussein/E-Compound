using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class BranchTranslation : Entity
    {
        public long BranchTranslationId { get; set; }
        public string Language { get; set; }
        public string BranchTitle { get; set; }
        public string BranchAddress { get; set; }

        public long BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
