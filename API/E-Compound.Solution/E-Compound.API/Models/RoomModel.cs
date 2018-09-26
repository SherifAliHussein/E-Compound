using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class RoomModel
    {
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public long FloorId { get; set; }
        public string FloorName { get; set; }

        public long BuildingId { get; set; }
        public string BuildingName { get; set; }
    }
}