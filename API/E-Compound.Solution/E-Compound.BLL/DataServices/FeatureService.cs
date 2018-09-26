using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.Common;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class FeatureService:Service<Feature>,IFeatureService
    {
        public FeatureService(IRepositoryAsync<Feature> repository):base(repository)
        {
            _repository = repository;
        }
        public List<Feature> GetAllFeaturesAdminId(long adminId, int page, int pageSize)
        {
            List<Feature> features;
            if (pageSize > 0)
                features = _repository.Query(x => !x.IsDeleted && x.CreationBy == adminId).Select()
                    .OrderBy(x => x.FeatureId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                features = _repository.Query(x => !x.IsDeleted && x.CreationBy == adminId).Select()
                    .OrderBy(x => x.FeatureId).ToList();

            return features;
        }

        public List<Feature> GetAllActiveFeaturesAdminId(long adminId, int page, int pageSize)
        {
            List<Feature> features;
            if (pageSize > 0)
                features = _repository.Query(x => !x.IsDeleted && x.CreationBy == adminId && x.IsActive).Select()
                    .OrderBy(x => x.FeatureId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                features = _repository.Query(x => !x.IsDeleted && x.CreationBy == adminId && x.IsActive).Select()
                    .OrderBy(x => x.FeatureId).ToList();

            return features;
        }

        public Feature CheckFeatureAsRestaurant(long adminId)
        {
            return _repository.Query(x => x.CreationBy == adminId && x.Type == Enums.FeatureType.Restaurant && !x.IsDeleted).Select().FirstOrDefault();
        }
    }
}
