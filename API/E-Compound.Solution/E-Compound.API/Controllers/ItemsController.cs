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
    public class ItemsController : BaseApiController
    {
        private IitemFacade _itemFacade;
        public ItemsController(IitemFacade itemFacade)
        {
            _itemFacade = itemFacade;
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items", Name = "AddItem")]
        [HttpPost]
        public IHttpActionResult AddItem()
        {

            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyItemImage);
            var httpPostedFile = HttpContext.Current.Request.Files[0];
            var httpPostedFile2 = HttpContext.Current.Request.Files[1];
            var httpPostedFile3 = HttpContext.Current.Request.Files[2];

            var itemModel = new JavaScriptSerializer().Deserialize<ItemModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null || httpPostedFile2 == null || httpPostedFile3 == null)
                throw new ValidationException(ErrorCodes.EmptyItemImage);

            if ((httpPostedFile.ContentLength > 2 * 1024 * 1000) || (httpPostedFile2.ContentLength > 2 * 1024 * 1000) || (httpPostedFile3.ContentLength > 2 * 1024 * 1000))
                throw new ValidationException(ErrorCodes.ImageExceedSize);


            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            if (Path.GetExtension(httpPostedFile2.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile2.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile2.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);


            if (Path.GetExtension(httpPostedFile3.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile3.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile3.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var itemDto = Mapper.Map<ItemDTO>(itemModel);

            itemDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(itemDto.Image);


            itemDto.Image2 = new MemoryStream();
            httpPostedFile2.InputStream.CopyTo(itemDto.Image2);

            itemDto.Image3 = new MemoryStream();
            httpPostedFile3.InputStream.CopyTo(itemDto.Image3);

            _itemFacade.AddItem(itemDto, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin,Enums.RoleType.Room)]
        [Route("api/Items/{itemId:long}", Name = "GetItem")]
        [HttpGet]
        [ResponseType(typeof(ItemModel))]
        public IHttpActionResult GetItem(long itemId)
        {
            var item = Mapper.Map<ItemModel>(_itemFacade.GetItem(itemId, Language));
            item.ImageURL = Url.Link("ItemImage", new { item.RestaurantId, item.MenuId, item.CategoryId, item.ItemID });
            return Ok(item);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items", Name = "UpdateItem")]
        [HttpPut]
        public IHttpActionResult UpdateItem()
        {
            var itemModel =
                new JavaScriptSerializer().Deserialize<ItemModel>(HttpContext.Current.Request.Form.Get(0));
            var itemDto = Mapper.Map<ItemDTO>(itemModel);
            if (itemModel.IsImageChange)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyItemImage);
                var httpPostedFile = HttpContext.Current.Request.Files["file"];


                if (httpPostedFile == null)
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);

                //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
                itemDto.Image = new MemoryStream();
                httpPostedFile.InputStream.CopyTo(itemDto.Image);
            }
            if (itemModel.IsImage2Change)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyItemImage);
                var httpPostedFile2 = HttpContext.Current.Request.Files["file2"];


                if (httpPostedFile2 == null)
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);

                if (httpPostedFile2.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile2.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile2.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile2.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);

                //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
                itemDto.Image2 = new MemoryStream();
                httpPostedFile2.InputStream.CopyTo(itemDto.Image2);
            }
            if (itemModel.IsImage3Change)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyItemImage);
                var httpPostedFile3 = HttpContext.Current.Request.Files["file3"];


                if (httpPostedFile3 == null)
                    throw new ValidationException(ErrorCodes.EmptyCategoryImage);

                if (httpPostedFile3.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile3.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile3.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile3.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);

                //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
                itemDto.Image3 = new MemoryStream();
                httpPostedFile3.InputStream.CopyTo(itemDto.Image3);
            }
            _itemFacade.UpdateItem(itemDto, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/{itemId:long}", Name = "DeleteItem")]
        [HttpDelete]
        public IHttpActionResult DeleteItem(long itemId)
        {
            _itemFacade.DeleteItem(itemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/Translate", Name = "TranslateItem")]
        [HttpPut]
        public IHttpActionResult TranslateItem([FromBody] ItemModel itemModel)
        {
            _itemFacade.TranslateItem(Mapper.Map<ItemDTO>(itemModel), Language);
            return Ok();
        }



        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/{itemId:long}/Activate", Name = "ActivateItem")]
        [HttpGet]
        public IHttpActionResult ActivateItem(long itemId)
        {
            _itemFacade.ActivateItem(itemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/{itemId:long}/DeActivate", Name = "DeActivateItem")]
        [HttpGet]
        public IHttpActionResult DeActivateItem(long itemId)
        {
            _itemFacade.DeActivateItem(itemId);
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Items/Order", Name = "UpdateItemOrder")]
        [HttpPut]
        public IHttpActionResult UpdateItemOrder([FromBody] ItemOrderModel itemOrder)
        {
            _itemFacade.UpdateItemOrder(Mapper.Map<List<ItemNamesDto>>(itemOrder.ItemNames));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/Items/{itemId:long}/Like", Name = "LikeItem")]
        [HttpGet]
        public IHttpActionResult LikeItem(long itemId)
        {
            _itemFacade.LikeItem(itemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/Items/{itemId:long}/Dislike", Name = "DislikeItem")]
        [HttpGet]
        public IHttpActionResult DislikeItem(long itemId)
        {
            _itemFacade.DislikeItem(itemId);
            return Ok();
        }
    }
}
