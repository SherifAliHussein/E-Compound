using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IUnitTypeFacade
    {
        PagedResultsDto GetAllPagingUnitTypes(long userId, int page, int pageSize);
        void DeleteUnitType(long userId, long unitTypeId);
        void AddUnitType(UnitTypeDto unitTypeDto, long userId);
        void UpdateUnitType(long userId, UnitTypeDto unitTypeDto);
        List<UnitTypeDto> GetUnitTypes(long userId);
    }
}
