using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Compound.BLL.DTOs;

namespace E_Compound.API.Models
{
    public class ItemOrderModel
    {
        public List<ItemNamesDto> ItemNames{ get; set; }
    }
}