using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.DAL.Entities.Model
{
    public class Technician : User
    {
        public Technician()
        {
            Requests = new List<Request>(); 
        }
        public virtual List<Request> Requests { get; set; }
        public UserCategory UserCategory { get; set; }
    }
}
