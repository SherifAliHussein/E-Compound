using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Floor:Entity
    {
        public Floor()
        {
            Rooms = new List<Room>();
        }
        public long FloorId { get; set; }
        public string FloorName { get; set; }
        public virtual List<Room> Rooms { get; set; }
        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin { get; set; }
        public bool IsDeleted { get; set; }
    }
}
