using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IFeatureTranslationService:IService<FeatureTranslation>
    {
        bool CheckFeatureNameExist(string featureName, string language, long featureId, long adminId);
    }
}
