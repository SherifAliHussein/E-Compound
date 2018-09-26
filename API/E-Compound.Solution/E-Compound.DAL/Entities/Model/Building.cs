using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Building:Entity
    {
        public Building()
        {
            Rooms = new List<Room>();
        }
        public long BuildingId { get; set; }
        public string BuildingName { get; set; }
        public virtual List<Room> Rooms { get; set; }
        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin { get; set; }
        public bool IsDeleted { get; set; }
    }
}
