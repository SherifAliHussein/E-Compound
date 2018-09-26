using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IRestaurantTypeTranslationService: IService<RestaurantTypeTranslation>
    {
        bool CheckRepeatedType(string typeName, string language, long restaurantTypeId, long userId);
        List<RestaurantType> GeRestaurantTypeTranslation(string language, long userId);
    }
}
