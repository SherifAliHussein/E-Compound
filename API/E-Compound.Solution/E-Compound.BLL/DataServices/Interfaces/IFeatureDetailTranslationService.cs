using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IFeatureDetailTranslationService:IService<FeatureDetailTranslation>
    {
        bool CheckFeatureDetailDescriptionExist(string featureDetailDescription, string language, long featureDetailId, long adminId);
    }
}
