using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface ICategoryFacade
    {
        void AddCategory(CategoryDTO categoryDto, string path);
        CategoryDTO GetCategory(long categoryId);
        PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize);
        void ActivateCategory(long categoryId);
        void DeActivateCategory(long categoryId);
        void DeleteCategory(long categoryId);
        void UpdateCategory(CategoryDTO categoryDto, string path);
        PagedResultsDto GetActivatedCategoriesByMenuId(string language, long menuId, int page, int pageSize);
        List<CategoryNamesDTO> GetAllCategoriesNameByMenuId(string language, long menuId);
    }
}
