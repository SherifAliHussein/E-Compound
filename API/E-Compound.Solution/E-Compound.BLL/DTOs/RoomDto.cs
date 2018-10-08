using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class RoomDto
    {
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long UnitId { get; set; }
        public string UnitName { get; set; }

        //public long FloorId { get; set; }
        //public string FloorName { get; set; }

        //public long BuildingId { get; set; }
        //public string BuildingName { get; set; }
    }
}
