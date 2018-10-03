using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Win32;

namespace E_Compound.DAL.Entities.Model
{
    public class Technician : User
    {
        public Technician()
        {
            Requests = new List<Request>(); 
        }
         
        public virtual List<Request> Requests { get; set; }   
        [Column(TypeName = "datetime2")]
        public DateTime CreateTime { get; set; }

        public UserCategory UserCategory { get; set; }
    }
}
