using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.Common;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class FeatureControl:Entity
    {
        public FeatureControl()
        {
            FeatureDetails = new List<FeatureDetail>();
        }
        public long FeatureControlId { get; set; }
        public int Order { get; set; }

        [ForeignKey("Feature")]
        public long FeatureId { get; set; }
        public virtual Feature Feature { get; set; }

        //[ForeignKey("Control")]
        //public long ControlId { get; set; }
        //public virtual Control Control { get; set; }
        public Enums.Control Control { get; set; }
        public Enums.ControlType ControlType { get; set; }
        public bool IsActive { get; set; }

        public virtual List<FeatureDetail> FeatureDetails { get; set; }

    }
}
