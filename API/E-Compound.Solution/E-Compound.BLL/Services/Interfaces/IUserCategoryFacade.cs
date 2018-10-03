using E_Compound.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IUserCategoryFacade
    {
        PagedResultsDto GetAllPagingUserCategories(long userId, int page, int pageSize);
        List<UserCategoryDto> GetUserCategories(long userId);
        void DeleteUserCategory(long userId, long userCategoryId);
        void AddUserCategory(UserCategoryDto userCategoryDto, long userId);
        void UpdateUserCategory(long userId, UserCategoryDto userCategoryDto);
    }
}
