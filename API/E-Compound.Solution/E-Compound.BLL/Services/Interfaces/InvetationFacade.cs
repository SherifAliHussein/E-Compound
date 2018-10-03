using System;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IInvetationFacade
    {
        bool CheckUnitInvetationLimit(long featureId, long userId, DateTime dateTime, int limit); 
    }
}
