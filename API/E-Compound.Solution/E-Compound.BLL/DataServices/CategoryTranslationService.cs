using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class CategoryTranslationService:Service<CategoryTranslation>,ICategoryTranslationService
    {
        public CategoryTranslationService(IRepositoryAsync<CategoryTranslation> repository):base(repository)
        {
        }

        public bool CheckCategoryNameExistForMenu(string categoryName, string language, long categoryId, long menuId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.CategoryName.ToLower() == categoryName.ToLower() &&
                          x.CategoryId != categoryId && x.Category.MenuId== menuId && !x.Category.IsDeleted);
        }

        public PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Category.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category).Count(x => !x.IsDeleted);
            List<Category> categories;
            if (pageSize > 0)
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            //results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            foreach (Category category in src)
            //            {
            //                category.CategoryTranslations = category.CategoryTranslations
            //                    .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            }

            //        }
            //    );
            //});
            return results;
        }

        public PagedResultsDto GetActivatedCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Category.IsDeleted && x.Category.IsActive && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category).Count(x => !x.IsDeleted);
            List<Category> categories;
            if (pageSize > 0)
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Category.IsActive && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Category.IsActive && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            //results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            foreach (Category category in src)
            //            {
            //                category.CategoryTranslations = category.CategoryTranslations
            //                    .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            }

            //        }
            //    );
            //});
            return results;
        }

        public List<CategoryNamesDTO> GetAllCategoriesNameByMenuId(string language, long menuId)
        {
            List<Category> categories;
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Category.MenuId == menuId && x.Category.IsActive)
                .Select(x => x.Category).OrderBy(x => x.CategoryId).ToList();
            return Mapper.Map<List<Category>, List<CategoryNamesDTO>>(categories);
            //return Mapper.Map<List<Category>, List<CategoryNamesDTO>>(categories, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            foreach (Category category in src)
            //            {
            //                category.CategoryTranslations = category.CategoryTranslations
            //                    .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            }

            //        }
            //    );
            //});
        }

        public bool CheckCategoryByLanguage(long categoryId, string language)
        {
            return _repository.Query(x => x.CategoryId == categoryId && x.Language.ToLower() == language.ToLower() && !x.Category.IsDeleted).Select()
                .Any();
        }
    }
}
