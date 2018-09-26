using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Guest.DAL.Entities.Model
{
    public class ControlTranslation:Entity
    {
        public long ControlTranslationId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        [ForeignKey("Control")]
        public long ControlId { get; set; }
        public virtual Control Control { get; set; }
    }
}
