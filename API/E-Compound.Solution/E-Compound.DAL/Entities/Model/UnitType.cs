using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class UnitType:Entity
    {
        public UnitType()
        {
            UnitTypeTranslations = new List<UnitTypeTranslation>();
            Units = new List<Unit>();
        }
        public long UnitTypeId { get; set; }
        public virtual ICollection<UnitTypeTranslation> UnitTypeTranslations { get; set; }
        public virtual ICollection<Unit> Units{ get; set; }
        public int Limit { get; set; }
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


        [ForeignKey("Deleter")]
        public long? DeletedBy { get; set; }
        public virtual Admin Deleter { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DeleteTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
