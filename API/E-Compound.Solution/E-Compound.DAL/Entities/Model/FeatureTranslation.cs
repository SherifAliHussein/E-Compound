using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class FeatureTranslation:Entity
    {
        public long FeatureTranslationId { get; set; }
        public string Language { get; set; }
        public string FeatureName { get; set; }

        [ForeignKey("Feature")]
        public long FeatureId { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
