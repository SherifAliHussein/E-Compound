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
    public class UserCategoryTranslationService : Service<UserCategoryTranslation>, IUserCategoryTranslationService
    {
        public UserCategoryTranslationService(IRepositoryAsync<UserCategoryTranslation> repository) : base(repository)
        {

        }

        public List<UserCategoryTranslation> GetTransByUserCategoryId(long userCategoryId)
        {
            var userCategoryTrans = _repository.Query().Select().Where(x => x.UserCategoryId == userCategoryId).ToList();
            return userCategoryTrans;
        }
    }
}
