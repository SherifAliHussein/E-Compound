using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.DAL.Entities.Model
{
    public class Technician : User
    {
        public Technician()
        {
            //UserCategories = new List<UserCategory>();
            Requests = new List<Request>();
            TechnicianCategories = new List<TechnicianCategory>();
        }
        [ForeignKey("Admin")]
        public long AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        //[ForeignKey("Feature")]
        //public long FeatureId { get; set; }
        //public virtual Feature Feature { get; set; }
        //public virtual List<UserCategory> UserCategories { get; set; }
        public virtual List<Request> Requests { get; set; }
        //public virtual List<FeatureDetail> FeatureDetail { get; set; }
        public virtual List<TechnicianCategory> TechnicianCategories { get; set; }
    }
}
