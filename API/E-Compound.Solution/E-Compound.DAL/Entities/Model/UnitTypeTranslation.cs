using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class UnitTypeTranslation:Entity
    {
        public long UnitTypeTranslationId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        [ForeignKey("UnitType")]
        public long UnitTypeId { get; set; }
        public virtual UnitType  UnitType{ get; set; }
    }
}
