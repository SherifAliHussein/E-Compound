using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class RestaurantTypeTranslationService:Service<RestaurantTypeTranslation>,IRestaurantTypeTranslationService
    {
        public RestaurantTypeTranslationService(IRepositoryAsync<RestaurantTypeTranslation> repository) : base(repository)
        {
            _repository = repository;
        }

        public bool CheckRepeatedType(string typeName, string language,long restaurantTypeId, long userId)
        {
            var restaurantTypeTranslations =
                Query(x => x.Language.ToLower() == language.ToLower() &&
                           x.TypeName.ToLower() == typeName.ToLower() &&
                           !x.RestaurantType.IsDeleted && x.RestaurantTypeId != restaurantTypeId && x.RestaurantType.AdminId == userId).Select().ToList();
            return restaurantTypeTranslations.Count > 0;
        }

        public List<RestaurantType> GeRestaurantTypeTranslation(string language, long userId)
        {
            return Query(x => x.Language.ToLower() == language.ToLower() && !x.RestaurantType.IsDeleted && x.RestaurantType.AdminId == userId ).Select(x => x.RestaurantType).ToList();
        }
    }
}
