using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.Common;

namespace E_Compound.BLL.DTOs
{
    public class RequestDto
    {
        public long RequestId { get; set; }
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public string Modifier { get; set; }
        public long ModifyBy { get; set; }
        public long FeatureId { get; set; }
        public Dictionary<string, string> FeatureNameDictionary { get; set; }
        public Enums.RequestStatus Status { get; set; }
        public Enums.FeatureType Type { get; set; }

        public long? RestaurantId { get; set; }

        public Dictionary<string, string> RestaurantName { get; set; }
        public List<RequestDetailDto> RequestDetails { get; set; }
        public string Comment { get; set; }
        public DateTime? RequestTime { get; set; }
        public int Count { get; set; }
        public Enums.InvetationType InvetationType { get; set; }
        public String Title { get; set; }
        public int UserCategory { get; set; }
    }
}
