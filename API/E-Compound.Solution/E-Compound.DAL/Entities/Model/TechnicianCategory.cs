using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class TechnicianCategory : Entity
    {
        public long TechnicianCategoryId { get; set; }

        [ForeignKey("UserCategory")]
        public long UserCategoryId { get; set; }
        public virtual UserCategory UserCategory { get; set; }

        [ForeignKey("Technician")]
        public long TechnicianId { get; set; }
        public virtual Technician Technician { get; set; }
    }
}
