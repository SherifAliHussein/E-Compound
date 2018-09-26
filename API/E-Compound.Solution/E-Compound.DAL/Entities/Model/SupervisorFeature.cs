using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class SupervisorFeature:Entity
    {
        public long SupervisorFeatureId { get; set; }

        [ForeignKey("Feature")]
        public long FeatureId { get; set; }
        public virtual Feature Feature { get; set; }


        [ForeignKey("Supervisor")]
        public long SupervisorId { get; set; }
        public virtual Supervisor Supervisor { get; set; }
    }
}
