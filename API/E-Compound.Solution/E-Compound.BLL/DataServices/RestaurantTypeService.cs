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
    public class RestaurantTypeService:Service<RestaurantType>, IRestaurantTypeService
    {
        public RestaurantTypeService(IRepositoryAsync<RestaurantType> repository) : base(repository)
        {
            _repository = repository;
        }

        public long GetLastRecordId()
        {
            var lastType = Queryable().OrderBy(x => x.RestaurantTypeId).Select(x => x).ToList().LastOrDefault();
            return lastType?.RestaurantTypeId ?? 0;
        }
    }
}
