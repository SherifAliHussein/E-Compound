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
    public class FeedBackController : BaseApiController
    {
        private IFeedBackFacade _feedBackFacade;

        public FeedBackController(IFeedBackFacade feedBackFacade)
        {
            _feedBackFacade = feedBackFacade;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin, Enums.RoleType.Room)]
        [Route("api/FeedBacks/", Name = "GetAllFeedBack")]
        [HttpGet]
        [ResponseType(typeof(List<FeedBackModel>))]
        public IHttpActionResult GetAllFeedBack(int page = Page, int pagesize = PageSize)
        {
            //var categories = _categoryFacade.GetAllCategoriesByMenuId(Language, menuId, page, pagesize);
            PagedResultsDto feedbacks;
            feedbacks = _feedBackFacade.GetAllFeedBack(UserId,0, page, pagesize, UserRole);
            var data = Mapper.Map<List<FeedBackModel>>(feedbacks.Data);

            return PagedResponse("GetAllFeedBack", page, pagesize, feedbacks.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/FeedBacks", Name = "FeedBacks")]
        [HttpPost]
        public IHttpActionResult AddFeedBack([FromBody]FeedBackModel feedBackModel) 
        {
            feedBackModel.RoomId = UserId;
            _feedBackFacade.AddFeedBack(Mapper.Map<FeedBackDto>(feedBackModel), UserId);
            return Ok();
        }
    }
}
