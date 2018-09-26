using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.DTOs
{
    public class FeedBackDto
    {
        public long FeedBackId { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public long RestaurantId { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long RoomId { get; set; }
        public string RoomName { get; set; }

    }
}
