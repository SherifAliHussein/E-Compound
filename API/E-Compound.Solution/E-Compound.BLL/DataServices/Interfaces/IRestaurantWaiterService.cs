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
    public interface IRestaurantWaiterService:IService<RestaurantWaiter>
    {
        bool CheckUserNameDuplicated(string userName, long restaurantId);
        PagedResultsDto GetAllRestaurantWaiters(long restaurantId, int page, int pageSize, string language);
        List<RestaurantWaiter> GetAlRestaurantWaitersByRestaurantId(long restaurantId);
    }
}
