﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IPackageService : IService<Package>
    {
        int GetUnitsCountByAdminId(long AdminId);
        //List<Package> GetAllPackagesByAdminId(long AdminId);
    }
}
