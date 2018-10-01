using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IUnitTypeService : IService<UnitType>
    {
        PagedResultsDto GetAllPagingUnitTypes(long userId, int page, int pageSize);
        List<UnitType> GetUnitTypes(long userId);
    }
}
