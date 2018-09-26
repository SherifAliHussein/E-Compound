using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Room: User
    {
        public Room()
        {
            Requests = new List<Request>();
            FeedBacks = new List<FeedBack>();
        }

        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        public virtual List<Request> Requests { get; set; }
        public virtual List<FeedBack> FeedBacks { get; set; }
        [ForeignKey("Package")]
        public long PackageId { get; set; }
        public virtual Package Package { get; set; }

        [ForeignKey("Building")]
        public long? BuildingId { get; set; }
        public virtual Building Building { get; set; }

        [ForeignKey("Floor")]
        public long? FloorId { get; set; }
        public virtual Floor Floor { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [ForeignKey("Unit")]
        public long UnitId { get; set; }
        public virtual Unit Unit { get; set; }

    }
}
