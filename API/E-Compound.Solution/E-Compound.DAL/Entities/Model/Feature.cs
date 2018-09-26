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
    public class Feature:Entity
    {
        public Feature()
        {
            SupervisorFeatures = new List<SupervisorFeature>();
            FeatureTranslations = new List<FeatureTranslation>();
            //FeatureDetails = new List<FeatureDetail>();
            Requests = new List<Request>();
            Restaurants = new List<Restaurant>();
            FeatureControls = new List<FeatureControl>();
        }
        public long FeatureId { get; set; }
        public virtual List<SupervisorFeature> SupervisorFeatures { get; set; }
        public virtual List<FeatureTranslation> FeatureTranslations { get; set; }
        //public virtual List<FeatureDetail> FeatureDetails { get; set; }
        public virtual List<Restaurant> Restaurants { get; set; }
        public virtual List<Request> Requests { get; set; }

        [ForeignKey("Creater")]
        public long CreationBy { get; set; }
        public virtual Admin Creater { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }


        [ForeignKey("Modifier")]
        public long? ModifiedBy { get; set; }
        public virtual Admin Modifier { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ModifyTime { get; set; }


        [ForeignKey("Deleter")]
        public long? DeletedBy { get; set; }
        public virtual Admin Deleter { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DeleteTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        //public bool HasDetails { get; set; }
        public Enums.FeatureType Type { get; set; }
        public virtual List<FeatureControl> FeatureControls { get; set; }
    }

}
