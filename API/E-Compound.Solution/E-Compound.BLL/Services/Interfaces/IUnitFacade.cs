using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IUnitFacade
    {
        PagedResultsDto GetAllPagingUnits(long userId, int page, int pageSize);
        void AddUnit(UnitDto unitDto, long userId);
        void UpdateUnit(long userId, UnitDto unitDto);
        void DeleteUnit(long userId, long unitId);
    }
}
