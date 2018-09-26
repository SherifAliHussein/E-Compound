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
    public interface IRestaurantService:IService<Restaurant>
    {
        PagedResultsDto GetAllRestaurant(string language, int page, int pageSize);
        Restaurant GetRestaurantByAdminId(long adminId);
        Restaurant GetRestaurantByWaiterId(long waiterId);
        //int GetAllResturantsLimits(long userId);
        List<Restaurant> GetRestaurantsName(long adminId);
    }
}
