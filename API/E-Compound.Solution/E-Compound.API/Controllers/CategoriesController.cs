using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    public class CategoriesController : BaseApiController
    {
        private ICategoryFacade _categoryFacade;
        private IitemFacade _itemFacade;
        private ITemplateFacade _templateFacade;

        public CategoriesController(ICategoryFacade categoryFacade,IitemFacade itemFacade, ITemplateFacade templateFacade)
        {
            _categoryFacade = categoryFacade;
            _itemFacade = itemFacade;
            _templateFacade = templateFacade;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories", Name = "AddCategory")]
        [HttpPost]
        public IHttpActionResult AddCategory()
        {

            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyCategoryImage);
            var httpPostedFile = HttpContext.Current.Request.Files[0];

            var categoryModel = new JavaScriptSerializer().Deserialize<CategoryModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null)
                throw new ValidationException(ErrorCodes.EmptyCategoryImage);

            if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                throw new ValidationException(ErrorCodes.ImageExceedSize);


            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var categoryDto = Mapper.Map<CategoryDTO>(categoryModel);
            //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
            categoryDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(categoryDto.Image);

            _categoryFacade.AddCategory(categoryDto, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}", Name = "GetCategory")]
        [HttpGet]
        [ResponseType(typeof(CategoryModel))]
        public IHttpActionResult GetCategory(long categoryId)
        {
            var category = Mapper.Map<CategoryModel>(_categoryFacade.GetCategory(categoryId));
            category.ImageURL = Url.Link("CategoryImage", new {category.RestaurantId, category.MenuId, category.CategoryId});
            
            return Ok(category);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}", Name = "DeleteCategory")]
        [HttpDelete]
        public IHttpActionResult DeleteCategory(long categoryId)
        {
            _categoryFacade.DeleteCategory(categoryId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/Activate", Name = "ActivateCategory")]
        [HttpGet]
        public IHttpActionResult ActivateCategory(long categoryId)
        {
            _categoryFacade.ActivateCategory(categoryId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/DeActivate", Name = "DeActivateCategory")]
        [HttpGet]
        public IHttpActionResult DeActivateCategory(long categoryId)
        {
            _categoryFacade.DeActivateCategory(categoryId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories", Name = "UpdateCategory")]
        [HttpPut]
        public IHttpActionResult UpdateCategory()
        {
            var categoryModel =
                new JavaScriptSerializer().Deserialize<CategoryModel>(HttpContext.Current.Request.Form.Get(0));
            var categoryDto = Mapper.Map<CategoryDTO>(categoryModel);
            if (categoryModel.IsImageChange)
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

                //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
                categoryDto.Image = new MemoryStream();
                httpPostedFile.InputStream.CopyTo(categoryDto.Image);
            }
            _categoryFacade.UpdateCategory(categoryDto, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/Items", Name = "GetAllItemsForCategory")]
        [HttpGet]
        [ResponseType(typeof(List<ItemModel>))]
        public IHttpActionResult GetAllItemsForCategory(long categoryId, int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto items;
            items = UserRole == Enums.RoleType.RestaurantAdmin.ToString() ? _itemFacade.GetAllItemsByCategoryId(Language, categoryId, page, pagesize) : _itemFacade.GetActivatedItemsByCategoryId(Language, categoryId, page, pagesize);

            //var items = _itemFacade.GetAllItemsByCategoryId(Language, categoryId, page, pagesize);
            var data = Mapper.Map<List<ItemModel>>(items.Data);
            foreach (var item in data)
            {
                item.ImageURL = Url.Link("ItemImage", new { item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID });
            }
            return PagedResponse("GetAllItemsForCategory", page, pagesize, items.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/Items/Name", Name = "GetAllItemNamesForCategory")]
        [HttpGet]
        [ResponseType(typeof(List<ItemModel>))]
        public IHttpActionResult GetAllItemNamesForCategory(long categoryId)
        {
            var data = Mapper.Map<List<ItemNameModel>>(_itemFacade.GetAllItemNamesByCategoryId(Language, categoryId));
            return Ok(data);
        }


        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Categories/{categoryId:long}/Template/", Name = "AddCategoryTemplate")]
        [HttpPost]
        public IHttpActionResult AddCategoryTemplate(long categoryId,[FromBody] CategoryTemplatesModel categoryTemplatesModel)
        {
            _templateFacade.AddTemplateForCategory(categoryId, Mapper.Map<List<PageDTO>>(categoryTemplatesModel.PageModels));
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin,Enums.RoleType.Room)]
        [Route("api/Categories/{categoryId:long}/Items/Templates", Name = "GetAllItemsWithTemplatesForCategory")]
        [HttpGet]
        [ResponseType(typeof(CategoryPageTemplateModel))]
        public IHttpActionResult GetAllItemsWithTemplatesForCategory(long categoryId)
        {
            var categoryPageTemplateModel = Mapper.Map<CategoryPageTemplateModel>(_itemFacade.GetActivatedItemsWithTemplatesByCategoryId(Language, categoryId));

            categoryPageTemplateModel.MenuImageURL = Url.Link("MenuImage", new { categoryPageTemplateModel.RestaurantId, categoryPageTemplateModel.MenuId });
            categoryPageTemplateModel.CategoryImageURL =  Url.Link("CategoryImage", new { categoryPageTemplateModel.RestaurantId, categoryPageTemplateModel.MenuId, categoryPageTemplateModel.CategoryId });
            foreach (var page in categoryPageTemplateModel.Templates)
            {
                page.ImageURL = Url.Link("GetTemplateImage", new { templateId = page.TemplateId });
                foreach (var item in page.ItemModels)
                {
                    item.ImageURL = Url.Link("ItemImage", new { item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID });
                }
            }
            return Ok(categoryPageTemplateModel);
        }
    }
}
