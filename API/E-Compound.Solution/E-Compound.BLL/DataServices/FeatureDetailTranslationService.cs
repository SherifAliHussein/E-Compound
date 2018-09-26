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
    public class FeatureDetailTranslationService:Service<FeatureDetailTranslation>,IFeatureDetailTranslationService
    {
        public FeatureDetailTranslationService(IRepositoryAsync<FeatureDetailTranslation> repository):base(repository)
        {
            _repository = repository;
        }
        public bool CheckFeatureDetailDescriptionExist(string featureDetailDescription, string language, long featureDetailId, long adminId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.Description.ToLower() == featureDetailDescription.ToLower() &&
                          x.FeatureDetailId != featureDetailId && !x.FeatureDetail.IsDeleted && x.FeatureDetail.CreationBy == adminId);
        }
    }
}
