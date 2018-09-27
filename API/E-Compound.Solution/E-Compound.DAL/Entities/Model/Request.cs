using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.Common;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Request:Entity
    {
        public Request()
        {
            RequestDetails = new List<RequestDetail>();
        }
        public long RequestId { get; set; }
        [ForeignKey("Creater")]
        public long CreationBy { get; set; }
        public virtual Room Creater { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }

        [ForeignKey("Modifier")]
        public long? ModifiedBy { get; set; }
        public virtual User Modifier { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ModifyTime { get; set; }

        [ForeignKey("Feature")]
        public long FeatureId { get; set; }
        public virtual Feature Feature { get; set; }

        [ForeignKey("Restaurant")]
        public long? RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public Enums.RequestStatus Status { get; set; }
        public virtual List<RequestDetail> RequestDetails { get; set; }
        public string Comment { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? RequestTime { get; set; }

        public int Count { get; set; }
        public Enums.InvetationType InvetationType { get; set; }
    }
}
