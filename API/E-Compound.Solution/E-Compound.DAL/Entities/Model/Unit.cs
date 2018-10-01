using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Unit:Entity
    {
        public long UnitId { get; set; }

        [ForeignKey("UnitType")]
        public long UnitTypeId { get; set; }
        public virtual UnitType UnitType { get; set; }
        public string Name { get; set; }

        [ForeignKey("Creater")]
        public long CreationBy { get; set; }
        public virtual Admin Creater { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }


        [ForeignKey("Modifier")]
        public long? ModifiedBy { get; set; }
        public virtual Admin Modifier { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ModifyTime { get; set; }
    }
}
