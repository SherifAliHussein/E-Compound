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
    public class CategoryService:Service<Category>,ICategoryService
    {
        public CategoryService(IRepositoryAsync<Category> repository):base(repository)
        {
            
        }

        public bool CategoryHasValidTemplates(long categoryId)
        {
            var category = Find(categoryId);
            var iis = category.Pages.Select(x => x.Template.ItemCount).Sum() >= category.Items.Count(x=>x.IsActive);
            return iis;
        }
        //public PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        //{
        //    var query = Queryable().Where(x => x.MenuId == menuId);
        //    PagedResultsDto results = new PagedResultsDto();
        //    results.TotalCount = query.Select(x => x).Count();
        //    results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(query.OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
        //        .Take(pageSize).ToList(), opt =>
        //    {
        //        opt.BeforeMap((src, dest) =>
        //            {
        //                foreach (Category category in src)
        //                {
        //                    category.CategoryTranslations = category.CategoryTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
        //                }

        //            }
        //        );
        //    });
        //    return results;
        //}
    }
}
