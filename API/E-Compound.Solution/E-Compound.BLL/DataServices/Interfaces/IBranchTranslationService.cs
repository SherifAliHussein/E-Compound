using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IBranchTranslationService:IService<BranchTranslation>
    {
        bool CheckBranchTitleExist(string branchTitle, string language, long branchId, long restaurantAdminId);
        PagedResultsDto GetAllBranchesByRestaurantAdminId(string language, long restaurantAdminId, int page,int pageSize);
    }
}
