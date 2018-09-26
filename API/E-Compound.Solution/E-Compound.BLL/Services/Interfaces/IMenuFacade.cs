using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IMenuFacade
    {
        void AddMenu(MenuDTO menuDto, long restaurantAdminId, string path);
        MenuDTO GetMenu(long menuId);
        PagedResultsDto GetAllMenusByRestaurantId(string language,long restaurantAdminId, int page, int pageSize);
        void DeleteMenu(long menuId);
        void DeActivateMenu(long menuId);
        void ActivateMenu(long menuId);
        void UpdateMenu(MenuDTO menuDto, long restaurantAdminId, string path);
        PagedResultsDto GetActivatedMenusByWaiterId(string language, long userId, int page, int pageSize);
        List<MenuDTO> GetAllAcivatedMenusNameByRestaurantId(string language, long restaurantAdminId);
        PagedResultsDto GetActivatedMenusByRestaurantId(string language, long restaurantId, int page, int pageSize);
    }
}
