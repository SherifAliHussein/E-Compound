using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using AutoMapper;
using E_Compound.API.Infrastructure;
using E_Compound.API.Models;
using E_Compound.API.Providers;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common;
using E_Compound.Common.CustomException;

namespace E_Compound.API.Controllers
{
    public class MenusController : BaseApiController
    {
        private IMenuFacade _menuFacade;
        private ICategoryFacade _categoryFacade;
        private IitemFacade _itemFacade;

        public MenusController(IMenuFacade menuFacade, ICategoryFacade categoryFacade,IitemFacade itemFacade)
        {
            _categoryFacade = categoryFacade;
            _menuFacade = menuFacade;
            _itemFacade = itemFacade;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus", Name = "AddMenu")]
        [HttpPost]
        public IHttpActionResult AddMenu()
        {
            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyCategoryImage);
            var httpPostedFile = HttpContext.Current.Request.Files[0];

            var menuModel = new JavaScriptSerializer().Deserialize<MenuModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null)
                throw new ValidationException(ErrorCodes.EmptyCategoryImage);

            if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                throw new ValidationException(ErrorCodes.ImageExceedSize);


            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var menuDto = Mapper.Map<MenuDTO>(menuModel);
            //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
            menuDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(menuDto.Image);

            _menuFacade.AddMenu(menuDto, UserId, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{MenuId:long}", Name = "GetMenu")]
        [HttpGet]
        [ResponseType(typeof(MenuModel))]
        public IHttpActionResult GetMenu(long menuId)
        {
            var menu = Mapper.Map<MenuModel>(_menuFacade.GetMenu(menuId));
            menu.ImageURL = Url.Link("MenuImage", new { menu.RestaurantId, menu.MenuId });
            return Ok(menu);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin,Enums.RoleType.Waiter)]
        [Route("api/Menus/", Name = "GetAllMenuForRestaurant")]
        [HttpGet]
        [ResponseType(typeof(List<MenuModel>))]
        public IHttpActionResult GetAllMenuForRestaurant( int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto menus;
            menus = UserRole == Enums.RoleType.RestaurantAdmin.ToString() ? _menuFacade.GetAllMenusByRestaurantId(Language, UserId, page, pagesize) : _menuFacade.GetActivatedMenusByWaiterId(Language, UserId, page, pagesize);
            var data = Mapper.Map<List<MenuModel>>(menus.Data);
            foreach (var menu in data)
            {
                menu.ImageURL = Url.Link("MenuImage", new { menu.RestaurantId, menu.MenuId });
            }
            return PagedResponse("GetAllMenuForRestaurant", page, pagesize, menus.TotalCount,data);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/Name", Name = "GetAllMenuNameForRestaurant")]
        [HttpGet]
        [ResponseType(typeof(List<MenuModel>))]
        public IHttpActionResult GetAllMenuNameForRestaurant()
        {
            var menus = _menuFacade.GetAllAcivatedMenusNameByRestaurantId(Language, UserId);
            return Ok(menus);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{menuId:long}", Name = "DeleteMenu")]
        [HttpDelete]
        public IHttpActionResult DeleteMenu(long menuId)
        {
            _menuFacade.DeleteMenu(menuId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{menuId:long}/Activate", Name = "ActivateMenu")]
        [HttpGet]
        public IHttpActionResult ActivateMenu(long menuId)
        {
            _menuFacade.ActivateMenu(menuId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{menuId:long}/DeActivate", Name = "DeActivateMenu")]
        [HttpGet]
        public IHttpActionResult DeActivateMenu(long menuId)
        {
            _menuFacade.DeActivateMenu(menuId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus", Name = "UpdateMenu")]
        [HttpPut]
        public IHttpActionResult UpdateMenu()
        {
            var menuModel =
                new JavaScriptSerializer().Deserialize<MenuModel>(HttpContext.Current.Request.Form.Get(0));
            var menuDto = Mapper.Map<MenuDTO>(menuModel);
            if (menuDto.IsImageChange)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);
                var httpPostedFile = HttpContext.Current.Request.Files[0];


                if (httpPostedFile == null)
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);
                
                menuDto.Image = new MemoryStream();
                httpPostedFile.InputStream.CopyTo(menuDto.Image);
            }
            _menuFacade.UpdateMenu(menuDto, UserId, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin, Enums.RoleType.Room)]
        [Route("api/Menus/{menuId:long}/Categories", Name = "GetAllCategoriesForMenu")]
        [HttpGet]
        [ResponseType(typeof(List<CategoryModel>))]
        public IHttpActionResult GetAllCategoriesForMenu(long menuId, int page = Page, int pagesize = PageSize)
        {
            //var categories = _categoryFacade.GetAllCategoriesByMenuId(Language, menuId, page, pagesize);
            PagedResultsDto categories;
            categories = UserRole == Enums.RoleType.RestaurantAdmin.ToString() ? _categoryFacade.GetAllCategoriesByMenuId(Language, menuId, page, pagesize) : _categoryFacade.GetActivatedCategoriesByMenuId(Language, menuId, page, pagesize);
            var data = Mapper.Map<List<CategoryModel>>(categories.Data);
            foreach (var category in data)
            {
                category.ImageURL = Url.Link("CategoryImage", new { category.RestaurantId, category.MenuId, category.CategoryId });
            }
            return PagedResponse("GetAllCategoriesForMenu", page, pagesize, categories.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Menus/{menuId:long}/Categories/Name", Name = "GetAllCategoriesNameForMenu")]
        [HttpGet]
        [ResponseType(typeof(List<CategoryNameModel>))]
        public IHttpActionResult GetAllCategoriesNameForMenu(long menuId)
        {
            var data = Mapper.Map<List<CategoryNameModel>>(_categoryFacade.GetAllCategoriesNameByMenuId(Language, menuId));
            return Ok(data);
        }

        [AuthorizeRoles(Enums.RoleType.Waiter)]
        [Route("api/Menus/OfflineData", Name = "GetAllMenuOfflineForRestaurant")]
        [HttpGet]
        [ResponseType(typeof(List<MenuModel>))]
        public IHttpActionResult GetAllMenuOfflineForRestaurant()
        {
            var menus = Mapper.Map<List<MenuModel>>(_menuFacade.GetActivatedMenusByWaiterId(Language, UserId, 1, 0).Data);
            //ImageConvert imageConvert = new ImageConvert();

            //Parallel.ForEach(menus, (menu) =>
            foreach (var menu in menus)
            {
                //menu.ImageURL = imageConvert.GetBase64FromImage(Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Restaurant-" + menu.RestaurantId + "\\" + "Menu-" + menu.MenuId)
                //    .FirstOrDefault(x => Path.GetFileName(x).Contains(menu.MenuId.ToString()) && !Path.GetFileName(x).Contains("thumb")));
                menu.ImageURL = Url.Link("MenuImage", new {menu.RestaurantId, menu.MenuId});

                menu.CategoryModels = Mapper.Map<List<CategoryModel>>(_categoryFacade
                    .GetActivatedCategoriesByMenuId(Language, menu.MenuId, 1, 0).Data);
                foreach (var category in menu.CategoryModels)
                {
                    //category.ImageURL = imageConvert.GetBase64FromImage(Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Restaurant-" + menu.RestaurantId + "\\" + "Menu-" + menu.MenuId + "\\" + "Category-" + category.CategoryId)
                    //.FirstOrDefault(x => Path.GetFileName(x).Contains(category.CategoryId.ToString()) && !Path.GetFileName(x).Contains("thumb")));
                    category.ImageURL = Url.Link("CategoryImage",
                        new {category.RestaurantId, category.MenuId, category.CategoryId});

                    category.CategoryPageTemplateModel =
                        Mapper.Map<CategoryPageTemplateModel>(
                            _itemFacade.GetActivatedItemsWithTemplatesByCategoryId(Language, category.CategoryId));
                    category.CategoryPageTemplateModel.MenuImageURL =
                        menu.ImageURL; // Url.Link("MenuImage", new { category.CategoryPageTemplateModel.RestaurantId, category.CategoryPageTemplateModel.MenuId });
                    category.CategoryPageTemplateModel.CategoryImageURL =
                        category
                            .ImageURL; // Url.Link("CategoryImage", new { category.CategoryPageTemplateModel.RestaurantId, category.CategoryPageTemplateModel.MenuId, category.CategoryPageTemplateModel.CategoryId });

                    foreach (var page in category.CategoryPageTemplateModel.Templates)
                    {

                        foreach (var item in page.ItemModels)
                        {
                            //item.ImageURL = imageConvert.GetBase64FromImage(Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Restaurant-" + menu.RestaurantId + "\\" + "Menu-" + menu.MenuId + "\\" + "Category-" + item.CategoryId + "\\Items")
                            //    .FirstOrDefault(x => Path.GetFileName(x).Contains(item.ItemID.ToString()) && !Path.GetFileName(x).Contains("thumb")));
                            item.ImageURL = Url.Link("ItemImage",
                                new {item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID});
                        }
                    }
                }
            }
            //});
            return Ok(menus);
        }
    }
}
