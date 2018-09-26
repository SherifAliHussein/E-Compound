using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IRestaurantFacade
    {
        List<RestaurantTypeDto> GetAllRestaurantType(string language, long userId);
        bool AddRestaurantType(RestaurantTypeDto restaurantTypeDto, long userId);
        void UpdateRestaurantType(RestaurantTypeDto restaurantTypeDto, long userId);
        void AddRestaurant(RestaurantDTO restaurantDto,string path, long userId);
        RestaurantDTO GetRestaurant(long restaurantId, string language);
        PagedResultsDto GetAllRestaurant(string language,int page,int pageSize, long userId);
        void ActivateRestaurant(long restaurantId);
        void DeActivateRestaurant(long restaurantId);
        void DeleteRestaurant(long restaurantId);
        void UpdateRestaurant(RestaurantDTO restaurantDto, string path, long userId);
        void DeleteRestaurantType(long restaurantTypeId);
        RestaurantDTO CheckRestaurantReady(long restaurantAdminId);
        void PublishRestaurant(long restaurantAdminId);
        ResturantInfoDto GetGlobalRestaurantInfo(long userId, string role, string language);
        RestaurantTypeDto GetRestaurantTypeById(long restaurantTypeId);
        List<RestaurantNameDto> GetAllRestaurantsName(long adminId);
    }
}
