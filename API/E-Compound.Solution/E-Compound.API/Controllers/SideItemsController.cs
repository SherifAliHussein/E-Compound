using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using E_Compound.API.Infrastructure;
using E_Compound.API.Models;
using E_Compound.API.Providers;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common;

namespace E_Compound.API.Controllers
{
    public class SideItemsController : BaseApiController
    {
        private ISideItemFacade _sideItemFacade;

        public SideItemsController(ISideItemFacade sideItemFacade)
        {
            _sideItemFacade = sideItemFacade;
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems", Name = "AddSideItem")]
        [HttpPost]
        public IHttpActionResult AddSideItem([FromBody] SideItemModel sideItemModel)
        {
            _sideItemFacade.AddSideItem(Mapper.Map<SideItemDTO>(sideItemModel),UserId, Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems/", Name = "GetAllSideItems")]
        [HttpGet]
        [ResponseType(typeof(List<SideItemModel>))]
        public IHttpActionResult GetAllSideItems(int page = Page, int pagesize = PageSize)
        {
            var sideItems = _sideItemFacade.GetAllSideItems(Language,UserId, page, pagesize);
            return PagedResponse("GetAllSideItems", page, pagesize, sideItems.TotalCount, Mapper.Map<List<SideItemModel>>(sideItems.Data));
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems/{sideItemId:long}", Name = "DeleteSideItem")]
        [HttpDelete]
        public IHttpActionResult DeleteSideItem(long sideItemId)
        {
            _sideItemFacade.DeleteSideItem(sideItemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems", Name = "UpdateSideItem")]
        [HttpPut]
        public IHttpActionResult UpdateSideItem([FromBody] SideItemModel sideItemModel)
        {
            _sideItemFacade.UpdateSideItem(Mapper.Map<SideItemDTO>(sideItemModel), UserId, Language);
            return Ok();
        }
    }
}
