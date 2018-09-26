using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Compound.Common;

namespace E_Compound.API.Models
{
    public class AvailableModel
    {
        public long AvailableId { get; set; }
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public long FeatureDetailId { get; set; }

        public Enums.Day Day { get; set; }
        public int Max { get; set; }
    }
}