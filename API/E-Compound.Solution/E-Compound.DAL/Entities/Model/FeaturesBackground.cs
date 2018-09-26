using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class FeaturesBackground : Entity
    {
        public FeaturesBackground()
        {

        }
        public long FeaturesBackgroundId { get; set; }
        public long UserId { get; set; }
    }
}
