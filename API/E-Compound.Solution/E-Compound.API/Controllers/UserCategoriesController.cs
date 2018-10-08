using E_Compound.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using E_Compound.API.Models;
using E_Compound.API.Providers;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common;

namespace E_Compound.API.Controllers
{
    public class UserCategoriesController : BaseApiController
    {
        private readonly IUserCategoryFacade _userCategoryFacade;
        public UserCategoriesController(IUserCategoryFacade userCategoryFacade)
        {
            _userCategoryFacade = userCategoryFacade;
        }

        [AuthorizeRoles(Enums.RoleType.Admin, Enums.RoleType.Room)]
        [Route("api/UserCategoriesPaging", Name = "GetAllPagingUserCategories")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<UserCategoryModel>))]
        public IHttpActionResult GetAllPagingUserCategories(int page = Page, int pagesize = PageSize)
        {
            var userCategories = _userCategoryFacade.GetAllPagingUserCategories(UserId, page, pagesize);
            var data = Mapper.Map<List<UserCategoryModel>>(userCategories.Data);

            return PagedResponse("GetAllPagingUserCategories", page, pagesize, userCategories.TotalCount, data);
        }

       // [AuthorizeRoles(Enums.RoleType.Admin,Enums.RoleType.Room)]
        [Route("api/UserCategories", Name = "GetUserCategories")]
        [HttpGet]
       [ResponseType(typeof(List<UserCategoryModel>))]
        public IHttpActionResult GetUserCategories()
        {                                                                                                    //UserId
            var userCategories = Mapper.Map<List<UserCategoryModel>>(_userCategoryFacade.GetUserCategories(UserId,UserRole));
            return Ok(userCategories);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UserCategories", Name = "AddUserCategory")]
        [HttpPost]
        public IHttpActionResult AddUserCategory([FromBody] UserCategoryModel userCategoryModel)
        {
            _userCategoryFacade.AddUserCategory(Mapper.Map<UserCategoryDto>(userCategoryModel), UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UserCategories", Name = "UpdateUserCategory")]
        [HttpPut]
        public IHttpActionResult UpdateUserCategory([FromBody] UserCategoryModel userCategoryModel)
        {
            _userCategoryFacade.UpdateUserCategory(UserId, Mapper.Map<UserCategoryDto>(userCategoryModel));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UserCategories/{userCategoryId:long}", Name = "DeleteUserCategory")]
        [HttpDelete]
        public IHttpActionResult DeleteUserCategory(long userCategoryId)
        {
            _userCategoryFacade.DeleteUserCategory(UserId, userCategoryId);
            return Ok();
        }
    }
}
