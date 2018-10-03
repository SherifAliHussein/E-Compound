using System;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IInvetationFacade
    {
        UnitDto CheckUnitInvetationLimit(long featureId, long userId, DateTime dateTime, int limit); 
    }
}
