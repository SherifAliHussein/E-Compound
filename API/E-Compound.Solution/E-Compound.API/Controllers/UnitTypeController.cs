using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI;
using AutoMapper;
using E_Compound.API.Models;
using E_Compound.API.Providers;
using E_Compound.API.Infrastructure;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common;

namespace E_Compound.API.Controllers
{
    public class UnitTypeController : BaseApiController
    {
        private readonly IUnitTypeFacade _unitTypeFacade;
        public UnitTypeController(IUnitTypeFacade unitTypeFacade)
        {
            _unitTypeFacade = unitTypeFacade;
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UnitTypesPaging", Name = "GetAllPagingUnitTypes")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<UnitTypeModel>))]
        public IHttpActionResult GetAllPagingUnitTypes(int page = Page, int pagesize = PageSize)
        {
            var unitTypes = _unitTypeFacade.GetAllPagingUnitTypes(UserId, page, pagesize);
            var data = Mapper.Map<List<UnitTypeModel>>(unitTypes.Data);

            return PagedResponse("GetAllPagingUnitTypes", page, pagesize, unitTypes.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UnitTypes", Name = "GetUnitTypes")]
        [HttpGet]
        [ResponseType(typeof(List<UnitTypeModel>))]
        public IHttpActionResult GetUnitTypes()
        {                                                                                                    //UserId
            var unitTypes = Mapper.Map<List<UnitTypeModel>>(_unitTypeFacade.GetUnitTypes(UserId));

            return Ok(unitTypes);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UnitTypes", Name = "AddUnitType")]
        [HttpPost]
        public IHttpActionResult AddUnitType([FromBody] UnitTypeModel unitTypeModel)
        {
            _unitTypeFacade.AddUnitType(Mapper.Map<UnitTypeDto>(unitTypeModel), UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UnitTypes", Name = "UpdateUnitType")]
        [HttpPut]
        public IHttpActionResult UpdateUnitType([FromBody] UnitTypeModel unitTypeModel)
        {
            _unitTypeFacade.UpdateUnitType(UserId, Mapper.Map<UnitTypeDto>(unitTypeModel));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UnitTypes/{unitTypeId:long}", Name = "DeleteUnitType")]
        [HttpDelete]
        public IHttpActionResult DeleteUnitType(long unitTypeId)
        {
            _unitTypeFacade.DeleteUnitType(unitTypeId);
            return Ok();
        }
    }
}
