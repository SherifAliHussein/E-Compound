﻿using System;
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

namespace E_Compound.API.Controllers
{
    public class RequestsController : BaseApiController
    {
        private IRequestFacade _requestFacade;
        public RequestsController(IRequestFacade requestFacade)
        {
            _requestFacade = requestFacade;
        }

        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/Requests", Name = "CreateRequest")]
        [HttpPost]
        public IHttpActionResult CreateRequest([FromBody] RequestModel requestModel)
        {
            requestModel.RoomId = UserId;
            _requestFacade.CreateRequest(Mapper.Map<RequestDto>(requestModel));
            return Ok();
        }

        //request ticket
        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/TicketRequests", Name = "CreateRequestTicket")]
        [HttpPost]
        public IHttpActionResult CreateRequestTicket()
        {

            var httpRequest = HttpContext.Current.Request;
            List<MemoryStream> files = new List<MemoryStream>();
            for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
            {
                var stream = new MemoryStream();
                HttpContext.Current.Request.Files[i].InputStream.CopyTo(stream);
                files.Add(stream);
            }
            var requestModel =
                new JavaScriptSerializer().Deserialize<RequestModel>(HttpContext.Current.Request.Form.Get(0));
            requestModel.Status = "0";
            requestModel.RoomId = UserId;
            _requestFacade.CreateTicket(Mapper.Map<RequestDto>(requestModel), UserId, files, HostingEnvironment.MapPath("~/Images/"));
            return Ok();

            requestModel.RoomId = UserId;
            _requestFacade.CreateRequest(Mapper.Map<RequestDto>(requestModel));
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.Admin, Enums.RoleType.Receptionist, Enums.RoleType.Supervisor, Enums.RoleType.Waiter)]
        [Route("api/Requests/", Name = "GetAllRequests")]
        [HttpGet]
        [ResponseType(typeof(List<RequestModel>))]
        public IHttpActionResult GetAllRequests(int page = Page, int pagesize = PageSize, long roomId = 0, long featureId = 0, string from = "", string to = "")
        {
            PagedResultsDto requests = _requestFacade.GetAllRequests(UserId, page, pagesize, UserRole, roomId, featureId, from, to);
            var data = Mapper.Map<List<RequestModel>>(requests.Data);

            return PagedResponse("GetAllRequests", page, pagesize, requests.TotalCount, data);
        }



        [AuthorizeRoles(Enums.RoleType.Supervisor, Enums.RoleType.Waiter)]
        [Route("api/Requests/{requestId:long}/Approve", Name = "ApproveRequest")]
        [HttpPost]
        public IHttpActionResult ApproveRequest(long requestId, [FromBody] RequestModel requestModel)
        {
            _requestFacade.ApproveRequest(requestId, UserId, Mapper.Map<List<RequestDetailDto>>(requestModel.RequestDetails));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor, Enums.RoleType.Waiter)]
        [Route("api/Requests/{requestId:long}/Reject", Name = "RejectRequest")]
        [HttpGet]
        public IHttpActionResult RejectRequest(long requestId)
        {
            _requestFacade.RejectRequest(requestId, UserId);
            return Ok();
        }
    }
}
