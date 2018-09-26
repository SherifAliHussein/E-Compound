using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.DAL.Entities.Model
{
    public class Receptionist:User
    {
        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin{ get; set; }
    }
}
