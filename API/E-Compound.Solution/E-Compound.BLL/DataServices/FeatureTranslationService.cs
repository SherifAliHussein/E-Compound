using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class FeatureTranslationService:Service<FeatureTranslation>,IFeatureTranslationService
    {
        public FeatureTranslationService(IRepositoryAsync<FeatureTranslation> repository):base(repository)
        {
            _repository = repository;
        }

        public bool CheckFeatureNameExist(string featureName, string language, long featureId, long adminId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.FeatureName.ToLower() == featureName.ToLower() &&
                          x.FeatureId != featureId && !x.Feature.IsDeleted && x.Feature.CreationBy== adminId);
        }
    }
}
