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
    public class UnitController : BaseApiController
    {
        private IUnitFacade _unitFacade;
        public UnitController(IUnitFacade unitFacade)
        {
            _unitFacade = unitFacade;
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/UnitsPaging", Name = "GetAllPagingUnits")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<UnitModel>))]
        public IHttpActionResult GetAllPagingUnits(int page = Page, int pagesize = PageSize)
        {
            var units = _unitFacade.GetAllPagingUnits(UserId, page, pagesize);
            var data = Mapper.Map<List<UnitModel>>(units.Data);

            return PagedResponse("GetAllPagingUnits", page, pagesize, units.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Units", Name = "AddUnit")]
        [HttpPost]
        public IHttpActionResult AddUnit([FromBody] UnitModel unitModel)
        {
            _unitFacade.AddUnit(Mapper.Map<UnitDto>(unitModel), UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Units", Name = "UpdateUnit")]
        [HttpPut]
        public IHttpActionResult UpdateUnit([FromBody] UnitModel unitModel)
        {
            _unitFacade.UpdateUnit(UserId, Mapper.Map<UnitDto>(unitModel));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Units/{unitId:long}", Name = "DeleteUnit")]
        [HttpDelete]
        public IHttpActionResult DeleteUnit(long unitId)
        {
            _unitFacade.DeleteUnit(UserId, unitId);
            return Ok();
        }
    }
}
