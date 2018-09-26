using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Compound.API.Models
{
    public class RequestStatusModel
    {
        public long RequestId { get; set; }
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Status { get; set; }
    }
}