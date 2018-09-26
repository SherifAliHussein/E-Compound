using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class FeatureDetail:Entity
    {
        public FeatureDetail()
        {
            FeatureDetailTranslations = new List<FeatureDetailTranslation>();
            RequestDetails = new List<RequestDetail>();
        }
        public long FeatureDetailId { get; set; }

        [ForeignKey("Creater")]
        public long CreationBy { get; set; }
        public virtual Supervisor Creater { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }


        [ForeignKey("Modifier")]
        public long? ModifiedBy { get; set; }
        public virtual Supervisor Modifier { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ModifyTime { get; set; }


        [ForeignKey("Deleter")]
        public long? DeletedBy { get; set; }
        public virtual Supervisor Deleter { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DeleteTime { get; set; }

        public bool IsFree { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("FeatureControl")]
        public long FeatureControlId { get; set; }
        public virtual FeatureControl FeatureControl { get; set; }

        public virtual List<FeatureDetailTranslation> FeatureDetailTranslations { get; set; }
        public virtual List<RequestDetail> RequestDetails { get; set; }
        public virtual List<Available> Availables { get; set; }
        public string Link { get; set; }


    }
}
