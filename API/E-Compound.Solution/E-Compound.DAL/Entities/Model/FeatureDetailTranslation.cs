using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class FeatureDetailTranslation:Entity
    {
        public long FeatureDetailTranslationId { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        [ForeignKey("FeatureDetail")]
        public long FeatureDetailId { get; set; }
        public virtual FeatureDetail FeatureDetail { get; set; }
    }
}
