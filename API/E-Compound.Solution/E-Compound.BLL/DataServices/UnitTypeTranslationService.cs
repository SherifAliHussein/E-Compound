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
    public class UnitTypeTranslationService : Service<UnitTypeTranslation>, IUnitTypeTranslationService
    {
        public UnitTypeTranslationService(IRepositoryAsync<UnitTypeTranslation> repository) : base(repository)
        {

        }

        public List<UnitTypeTranslation> GetTransByUnitTypeId(long unitTypeId)
        {
            var unitTypeTrans = _repository.Query().Select().Where(x => x.UnitTypeId == unitTypeId).ToList();
            return unitTypeTrans;
        }
    }
}
