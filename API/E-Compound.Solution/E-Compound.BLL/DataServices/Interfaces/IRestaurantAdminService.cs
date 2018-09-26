using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IRestaurantAdminService:IService<RestaurantAdmin>
    {
        bool CheckUserNameDuplicated(string userName,long restaurantId);
    }
}
