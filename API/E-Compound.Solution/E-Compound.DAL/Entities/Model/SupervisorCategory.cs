﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;

namespace E_Compound.DAL.Entities.Model
{
    public class SupervisorCategory:Entity
    {
        public long SupervisorCategoryId { get; set; }

        [ForeignKey("UserCategory")]
        public long UserCategoryId { get; set; }
        public virtual UserCategory UserCategory { get; set; }

        [ForeignKey("Supervisor")]
        public long SupervisorId { get; set; }
        public virtual Supervisor Supervisor { get; set; }

    }
}