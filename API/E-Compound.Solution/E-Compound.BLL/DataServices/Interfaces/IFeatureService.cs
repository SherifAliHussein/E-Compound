using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IFeatureService:IService<Feature>
    {
        List<Feature> GetAllFeaturesAdminId(long adminId, int page, int pageSize);
        List<Feature> GetAllActiveFeaturesAdminId(long adminId, int page, int pageSize);
        Feature CheckFeatureAsRestaurant(long adminId);
        Feature CheckFeatureAsInvitation(long adminId);
        Feature CheckFeatureAsTicket(long adminId);
    }
}
