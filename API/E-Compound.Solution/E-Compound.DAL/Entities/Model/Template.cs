using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class Template:Entity
    {
        public Template()
        {
            Pages = new List<Page>();
        }
        public long Id { get; set; }
        
        public virtual ICollection<Page> Pages { get; set; }
        public int ItemCount { get; set; }


    }
}
