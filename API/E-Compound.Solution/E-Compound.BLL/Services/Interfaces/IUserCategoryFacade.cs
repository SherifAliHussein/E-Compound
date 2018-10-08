using E_Compound.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.Common;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IUserCategoryFacade
    {
        PagedResultsDto GetAllPagingUserCategories(long userId, int page, int pageSize);
        List<UserCategoryDto> GetUserCategories(long userId, string roleType);
        void DeleteUserCategory(long userId, long userCategoryId);
        void AddUserCategory(UserCategoryDto userCategoryDto, long userId);
        void UpdateUserCategory(long userId, UserCategoryDto userCategoryDto);
    }
}
