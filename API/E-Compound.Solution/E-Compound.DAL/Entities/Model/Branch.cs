using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Branch : Entity
    {
        public Branch()
        {
            BranchTranslations = new List<BranchTranslation>();
            RestaurantWaiters = new List<RestaurantWaiter>();
        }
        public long BranchId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Restaurant")]
        public long RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<BranchTranslation> BranchTranslations { get; set; }
        public virtual ICollection<RestaurantWaiter> RestaurantWaiters { get; set; }
    }
}
