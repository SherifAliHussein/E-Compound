using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Guest.DAL.Entities.Model
{
    public class Control:Entity
    {
        public Control()
        {
            ControlTranslations = new List<ControlTranslation>();
            FeatureControls = new List<FeatureControl>();
        }
        public long ControlId { get; set; }

        public virtual List<ControlTranslation> ControlTranslations { get; set; }
        public virtual List<FeatureControl> FeatureControls { get; set; }
    }
}
