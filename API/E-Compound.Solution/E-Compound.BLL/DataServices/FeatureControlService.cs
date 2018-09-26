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
    public class FeatureControlService:Service<FeatureControl>,IFeatureControlService
    {
        public FeatureControlService(IRepositoryAsync<FeatureControl> repository):base(repository)
        {
            _repository = repository;
        }

        public void UpdateRange(List<FeatureControl> featureControls)
        {
            foreach (var featureControl in featureControls)
            {
                _repository.Update(featureControl);
            }
        }
    }
}
