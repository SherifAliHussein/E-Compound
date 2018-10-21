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
    public interface IUnitService : IService<Unit>
    {
        PagedResultsDto GetAllPagingUnits(long userId, int page, int pageSize);
        Unit RelationValidation(long userId, long unitTypeId);
        int GetUnitCountByPackageId(long packageId);
        int GetConsumedUnits(long userId);
        Unit NameValidation(Unit unit);
    }
}
