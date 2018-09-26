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
    public class Available:Entity
    {
        public long AvailableId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime From { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime To { get; set; }

        [ForeignKey("FeatureDetail")]
        public long FeatureDetailId { get; set; }
        public virtual FeatureDetail FeatureDetail { get; set; }

        public Enums.Day Day { get; set; }
        public int Max { get; set; }
        public bool IsDeleted { get; set; }
    }
}
