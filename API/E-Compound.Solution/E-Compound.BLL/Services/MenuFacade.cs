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
    public class MenuFacade:BaseFacade,IMenuFacade
    {
        private IMenuService _menuService;
        private IMenuTranslationService _menuTranslationService;
        private IRestaurantService _restaurantService;
        private IRestaurantTranslationService _restaurantTranslationService;
        private IRestaurantWaiterService _restaurantWaiterService;
        private IManageStorage _manageStorage;


        public MenuFacade(IMenuService menuService,IMenuTranslationService menuTranslationService, IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService
            ,IRestaurantWaiterService restaurantWaiterService, IManageStorage manageStorage, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _menuService = menuService;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _restaurantWaiterService = restaurantWaiterService;
            _manageStorage = manageStorage;
        }

        public MenuFacade(IMenuService menuService, IMenuTranslationService menuTranslationService, IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService
            , IRestaurantWaiterService restaurantWaiterService)
        {
            _menuService = menuService;
            _menuTranslationService = menuTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _restaurantWaiterService = restaurantWaiterService;
        }

        public void AddMenu(MenuDTO menuDto,long restaurantAdminId, string path)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            ValidateMenu(menuDto,restaurant.RestaurantId);
            var menu = new Menu();
            foreach (var menuName in menuDto.MenuNameDictionary)
            {
                menu.MenuTranslations.Add(new MenuTranslation
                {
                    MenuName = menuName.Value,
                    Language = menuName.Key.ToLower()
                });
            }
            menu.RestaurantId = restaurant.RestaurantId;
            _menuTranslationService.InsertRange(menu.MenuTranslations);
            _menuService.Insert(menu);
            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + menu.RestaurantId + "\\" + "Menu-" + menu.MenuId, menuDto.Image, menu.MenuId.ToString());
        }

        public MenuDTO GetMenu(long menuId)
        {
            var menu = _menuService.Find(menuId);
            if(menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if(menu.IsDeleted) throw new NotFoundException(ErrorCodes.MenuDeleted);
            //return Mapper.Map<Menu, MenuDTO>(menu, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.MenuTranslations = src.MenuTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower())
            //                .ToList();
            //        }
            //    ); 
            //});
            return Mapper.Map<MenuDTO>(menu);
        }
        private void ValidateMenu(MenuDTO menuDto, long restaurantId)
        {
            foreach (var menuName in menuDto.MenuNameDictionary)
            {
                if (string.IsNullOrEmpty(menuName.Value))
                    throw new ValidationException(ErrorCodes.EmptyMenuName);
                if (menuName.Value.Length > 300)

                    throw new ValidationException(ErrorCodes.MenuNameExceedLength);
                if (_menuTranslationService.CheckMenuNameExistForRestaurant(menuName.Value, menuName.Key, menuDto.MenuId,
                    restaurantId)) throw new ValidationException(ErrorCodes.MenuNameAlreadyExist);
            }
        }

        public PagedResultsDto GetAllMenusByRestaurantId(string language,long restaurantAdminId, int page, int pageSize)
        {
            var result = _menuTranslationService.GetAllMenusByRestaurantAdminId(language, restaurantAdminId, page, pageSize);
           // result.IsParentTranslated = language == Strings.DefaultLanguage || _restaurantTranslationService.CheckRestaurantByLanguage(restaurantAdminId, language);
            return result;
        }

        public List<MenuDTO> GetAllAcivatedMenusNameByRestaurantId(string language, long restaurantAdminId)
        {
            return _menuTranslationService.GetAllMenusNameByRestaurantAdminId(language, restaurantAdminId);
        }

        public PagedResultsDto GetActivatedMenusByWaiterId(string language, long userId, int page, int pageSize)
        {
            var waiter = _restaurantWaiterService.Find(userId);
            if(waiter == null) throw new NotFoundException(ErrorCodes.RestaurantAdminNotFound);
            var result = _menuTranslationService.GetActivatedMenusByRestaurantId(language, waiter.RestaurantId, page, pageSize);
            //result.IsParentTranslated = language == Strings.DefaultLanguage || _restaurantTranslationService.CheckRestaurantByLanguage(restaurantAdminId, language);
            return result;
        }

        public PagedResultsDto GetActivatedMenusByRestaurantId(string language, long restaurantId, int page, int pageSize)
        {
            var result = _menuTranslationService.GetActivatedMenusByRestaurantId(language, restaurantId, page, pageSize);
            //result.IsParentTranslated = language == Strings.DefaultLanguage || _restaurantTranslationService.CheckRestaurantByLanguage(restaurantAdminId, language);
            return result;
        }
        public void ActivateMenu(long menuId)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (menu.Categories.Count(m => m.IsActive) <= 0)
                throw new ValidationException(ErrorCodes.MenuCategoriesDoesNotActivated);
            if (Strings.SupportedLanguages.Any(x => !menu.MenuTranslations.Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.MenuIsNotTranslated);
            menu.IsActive = true;
            _menuService.Update(menu);
            SaveChanges();
        }

        public void DeActivateMenu(long menuId)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            menu.IsActive = false;
            _menuService.Update(menu);
            var restaurantHasMenuActivated = menu.Restaurant.Menus.Any(x => x.IsActive);
            if (!restaurantHasMenuActivated)
            {
                menu.Restaurant.IsActive = false;
                menu.Restaurant.IsReady = false;
                _restaurantService.Update(menu.Restaurant);
            }
            SaveChanges();
        }
        public void DeleteMenu(long menuId)
        {
            var menu = _menuService.Find(menuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            menu.IsDeleted = true;
            menu.IsActive = false;
            _menuService.Update(menu);
            var restaurantHasMenuActivated = menu.Restaurant.Menus.Any(x => x.IsActive);
            if (!restaurantHasMenuActivated)
            {
                menu.Restaurant.IsActive = false;
                menu.Restaurant.IsReady = false;
                _restaurantService.Update(menu.Restaurant);
            }
            SaveChanges();
        }

        public void UpdateMenu(MenuDTO menuDto, long restaurantAdminId, string path)
        {
            var menu = _menuService.Find(menuDto.MenuId);
            if (menu == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            ValidateMenu(menuDto,menu.RestaurantId);
            foreach (var menuName in menuDto.MenuNameDictionary)
            {
                var menuTranslation =
                    menu.MenuTranslations.FirstOrDefault(x => x.Language.ToLower() == menuName.Key.ToLower());
                if (menuTranslation == null)
                {
                    menu.MenuTranslations.Add(new MenuTranslation
                    {
                        Language = menuName.Key.ToLower(),
                        MenuName = menuName.Value
                    });
                }
                else
                {
                    menuTranslation.MenuName = menuName.Value;
                }
            }
            _menuService.Update(menu);
            SaveChanges();
            if (menuDto.IsImageChange)
                _manageStorage.UploadImage(path + "\\" + "Restaurant-" + menu.RestaurantId + "\\" + "Menu-" + menu.MenuId, menuDto.Image, menu.MenuId.ToString());
        }
    }
}
