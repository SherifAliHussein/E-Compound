using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class RequestDetail:Entity
    {
        public long RequestDetailId { get; set; }

        [ForeignKey("FeatureDetail")]
        public long? FeatureDetailId { get; set; }
        public virtual FeatureDetail FeatureDetail { get; set; }


        [ForeignKey("ItemSize")]
        public long? ItemSizeId { get; set; }
        public virtual ItemSize ItemSize { get; set; }

        [ForeignKey("Request")]
        public long RequestId { get; set; }
        public virtual Request Request { get; set; }

        public int Number { get; set; }
        
        public decimal Price { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? From { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? To { get; set; }
    }
}
