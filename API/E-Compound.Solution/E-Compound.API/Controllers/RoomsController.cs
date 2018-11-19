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
    public class RoomsController : BaseApiController
    {
        private IRoomFacade _roomFacade;
        public RoomsController(IRoomFacade roomFacade)
        {
            _roomFacade = roomFacade;
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms", Name = "AddRoom")]
        [HttpPost]
        public IHttpActionResult AddRoom([FromBody] RoomModel roomModel)
        {
            _roomFacade.AddRoom(Mapper.Map<RoomDto>(roomModel), UserId);
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms/", Name = "GetAllRoom")]
        [HttpGet]
        [ResponseType(typeof(List<RoomModel>))]
        public IHttpActionResult GetAllRoom(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto rooms = _roomFacade.GetAllRoom(UserId, page, pagesize);
            var data = Mapper.Map<List<RoomModel>>(rooms.Data);

            return PagedResponse("GetAllRoom", page, pagesize, rooms.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.Admin, Enums.RoleType.Supervisor, Enums.RoleType.Receptionist, Enums.RoleType.Waiter, Enums.RoleType.Technician)]
        [Route("api/Rooms/Name", Name = "GetAllRoomNames")]
        [HttpGet]
        [ResponseType(typeof(List<RoomNameModel>))]
        public IHttpActionResult GetAllRoomNames()
        {
            List<RoomNameModel> rooms = Mapper.Map<List<RoomNameModel>>(_roomFacade.GetAllRoomNames(UserId, UserRole));
            return Ok(rooms);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms/UsedUnits", Name = "GetUsedUnits")]
        [HttpGet]
        [ResponseType(typeof(List<RoomNameModel>))]
        public IHttpActionResult GetUsedUnits()
        {
            List<UnitModel> usedUnits = Mapper.Map<List<UnitModel>>(_roomFacade.GetUsedUnits(UserId));
            return Ok(usedUnits);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms/{roomId:long}", Name = "GetRoom")]
        [HttpGet]
        [ResponseType(typeof(RoomModel))]
        public IHttpActionResult GetRoom(long roomId)
        {
            return Ok(Mapper.Map<RoomModel>(_roomFacade.GetRoom(roomId)));
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms/{roomId:long}", Name = "DeleteRoom")]
        [HttpDelete]
        public IHttpActionResult DeleteRoom(long roomId)
        {
            _roomFacade.DeleteRoom(roomId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms/{roomId:long}/Activate", Name = "ActivateRoom")]
        [HttpGet]
        public IHttpActionResult ActivateRoom(long roomId)
        {
            _roomFacade.ActivateRoom(roomId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms/{roomId:long}/DeActivate", Name = "DeActivateRoom")]
        [HttpGet]
        public IHttpActionResult DeActivateRoom(long roomId)
        {
            _roomFacade.DeActivateRoom(roomId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Rooms/", Name = "UpdateRoom")]
        [HttpPut]
        public IHttpActionResult UpdateRoom([FromBody] RoomModel roomModel)
        {
            _roomFacade.UpdateRoom(Mapper.Map<RoomDto>(roomModel), UserId);
            return Ok();
        }

    }
}
