using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.BLL.Services.ManageStorage;
using E_Compound.Common;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class CategoryFacade:BaseFacade,ICategoryFacade
    {
        private ICategoryService _categoryService;
        private ICategoryTranslationService _categoryTranslationService;
        private IMenuService _menuService;
        private IMenuTranslationService _menuTranslationService;
        private IManageStorage _manageStorage;
        private IRestaurantService _restaurantService;

        public CategoryFacade(ICategoryService categoryService, ICategoryTranslationService categoryTranslationService, IMenuService menuService, IManageStorage manageStorage, IMenuTranslationService menuTranslationService
            ,IRestaurantService restaurantService, IUnitOfWorkAsync unitOfWork):base(unitOfWork)
        {
            _categoryService = categoryService;
            _categoryTranslationService = categoryTranslationService;
            _menuService = menuService;
            _manageStorage = manageStorage;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
        }

        public CategoryFacade(ICategoryService categoryService, ICategoryTranslationService categoryTranslationService, IMenuService menuService, IManageStorage manageStorage, IMenuTranslationService menuTranslationService
        ,IRestaurantService restaurantService)
        {
            _categoryService = categoryService;
            _categoryTranslationService = categoryTranslationService;
            _menuService = menuService;
            _manageStorage = manageStorage;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
        }

        public void AddCategory(CategoryDTO categoryDto, string path)
        {
            ValidateCategory(categoryDto);
            var menu = _menuService.Find(categoryDto.MenuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.IsDeleted) throw new ValidationException(ErrorCodes.MenuDeleted);
            var category = Mapper.Map<Category>(categoryDto);
            foreach (var categoryName in categoryDto.CategoryNameDictionary)
            {
                category.CategoryTranslations.Add(new CategoryTranslation
                {
                    CategoryName = categoryName.Value,
                    Language = categoryName.Key
                });
            }
            _categoryTranslationService.InsertRange(category.CategoryTranslations);
            _categoryService.Insert(category);
            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + menu.RestaurantId+"\\"+ "Menu-" + menu.MenuId+"\\" + "Category-" + category.CategoryId, categoryDto.Image, category.CategoryId.ToString());
        }

        public CategoryDTO GetCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new NotFoundException(ErrorCodes.CategoryDeleted);
            //return Mapper.Map<Category, CategoryDTO>(category, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.CategoryTranslations = src.CategoryTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower())
            //                .ToList();
            //        }
            //    );
            //});
            return Mapper.Map<CategoryDTO>(category);
        }
        private void ValidateCategory(CategoryDTO categoryDto)
        {
            foreach (var categoryName in categoryDto.CategoryNameDictionary)
            {
                if (string.IsNullOrEmpty(categoryName.Value))
                    throw new ValidationException(ErrorCodes.EmptyCategoryName);
                if (categoryName.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.CategoryNameExceedLength);
                if (_categoryTranslationService.CheckCategoryNameExistForMenu(categoryName.Value, categoryName.Key,
                    categoryDto.CategoryId, categoryDto.MenuId))
                    throw new ValidationException(ErrorCodes.CategoryNameAlreadyExist);
            }
        }

        public PagedResultsDto GetAllCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.IsDeleted) throw new ValidationException(ErrorCodes.MenuDeleted);
            var results = _categoryTranslationService.GetAllCategoriesByMenuId(language, menuId, page, pageSize);
            //results.IsParentTranslated = language == Strings.DefaultLanguage || _menuTranslationService.CheckMenuByLanguage(menuId, language);
            return results;
        }

        public List<CategoryNamesDTO> GetAllCategoriesNameByMenuId(string language, long menuId)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.IsDeleted) throw new ValidationException(ErrorCodes.MenuDeleted);
            return _categoryTranslationService.GetAllCategoriesNameByMenuId(language, menuId);
        }

        public PagedResultsDto GetActivatedCategoriesByMenuId(string language, long menuId, int page, int pageSize)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.IsDeleted) throw new ValidationException(ErrorCodes.MenuDeleted);
            var results = _categoryTranslationService.GetActivatedCategoriesByMenuId(language, menuId, page, pageSize);
            //results.IsParentTranslated = language == Strings.DefaultLanguage || _menuTranslationService.CheckMenuByLanguage(menuId, language);
            return results;
        }

        public void ActivateCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (category.Items.Count(m => m.IsActive) <= 0)
                throw new ValidationException(ErrorCodes.CategoryItemsDoesNotActivated);
            if (Strings.SupportedLanguages.Any(x => !category.CategoryTranslations.Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.CategoryIsNotTranslated);
            //if(!_categoryService.CategoryHasValidTemplates(categoryId))
            //    throw new ValidationException(ErrorCodes.CategoryTemplatesInvalid);
            category.IsActive = true;
            _categoryService.Update(category);
            SaveChanges();
        }

        public void DeActivateCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            category.IsActive = false;
            _categoryService.Update(category);
            var menuHasCategoryActivated = category.Menu.Categories.Any(x => x.IsActive);
            if (!menuHasCategoryActivated)
            {
                category.Menu.IsActive = false;
                _menuService.Update(category.Menu);
            }
            SaveChanges();
        }
        public void DeleteCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            category.IsDeleted = true;
            category.IsActive = false;
            _categoryService.Update(category);
            var menuHasCategoryActivated = category.Menu.Categories.Any(x => x.IsActive);
            if (!menuHasCategoryActivated)
            {
                category.Menu.IsActive = false;
                _menuService.Update(category.Menu);
                CheckRestaurantHasMenuActivated(category);
            }
            SaveChanges();
        }
        private void CheckRestaurantHasMenuActivated(Category category)
        {
            var restaurantHasMenuActivated = category.Menu.Restaurant.Menus.Any(x => x.IsActive);
            if (!restaurantHasMenuActivated)
            {
                category.Menu.Restaurant.IsActive = false;
                _restaurantService.Update(category.Menu.Restaurant);
            }
        }

        public void UpdateCategory(CategoryDTO categoryDto, string path)
        {
            ValidateCategory(categoryDto);
            var category = _categoryService.Find(categoryDto.CategoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);

            foreach (var categoryName in categoryDto.CategoryNameDictionary)
            {
                var categoryTranslation =
                    category.CategoryTranslations.FirstOrDefault(x => x.Language.ToLower() == categoryName.Key.ToLower());
                if (categoryTranslation == null)
                {
                    category.CategoryTranslations.Add(new CategoryTranslation
                    {
                        Language = categoryName.Key.ToLower(),
                        CategoryName = categoryName.Value
                    });
                }
                else
                {
                    categoryTranslation.CategoryName = categoryName.Value;
                }
            }
            _categoryService.Update(category);
            SaveChanges();
            if (categoryDto.IsImageChange)
                _manageStorage.UploadImage(path + "\\" + "Restaurant-" + category.Menu.RestaurantId + "\\" + "Menu-" + category.MenuId + "\\" +"Category-" + category.CategoryId
                    , categoryDto.Image, categoryDto.CategoryId.ToString());
        }
    }
}
