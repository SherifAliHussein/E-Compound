using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Package:Entity
    {

        public Package()
        {
            Rooms = new List<Room>();
        }
        public long PackageId { get; set; }
        public Guid PackageGuid { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Start { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime End { get; set; }

        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin { get; set; }
        public int MaxNumberOfRooms { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
