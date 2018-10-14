using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        [AuthorizeRoles(Enums.RoleType.Room, Enums.RoleType.Supervisor)]
        [Route("api/Requests", Name = "CreateRequest")]
        [HttpPost]
        public IHttpActionResult CreateRequest([FromBody] RequestModel requestModel)
        {
            requestModel.RoomId = UserId;
            if (requestModel.Technician != null) 
            _requestFacade.AssignRequest(Mapper.Map<RequestDto>(requestModel));
                 
           else _requestFacade.CreateRequest(Mapper.Map<RequestDto>(requestModel));
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


        [AuthorizeRoles(Enums.RoleType.Admin, Enums.RoleType.Receptionist, Enums.RoleType.Supervisor, Enums.RoleType.Waiter, Enums.RoleType.Technician)]
        [Route("api/Requests/", Name = "GetAllRequests")]
        [HttpGet]
        [ResponseType(typeof(List<RequestModel>))]
        public IHttpActionResult GetAllRequests(int page = Page, int pagesize = PageSize, long roomId = 0, long featureId = 0, string from = "", string to = "")
        {
            PagedResultsDto requests = _requestFacade.GetAllRequests(UserId, page, pagesize, UserRole, roomId, featureId, from, to);
            var data = Mapper.Map<List<RequestModel>>(requests.Data);
            if (data != null)
                foreach (var item in data)
                {
                    item.ImagesURL = new List<string>();
                    string path = HostingEnvironment.MapPath("~/Images/") + "\\" + "Ticket-" + item.RequestId;
                    var imageCounter = Directory.Exists(path) ? Directory
                        .GetFiles(path)
                        .Count(x => !Path.GetFileName(x).Contains("thumb")) : -1;
                    int id = 1;
                    while (id < imageCounter + 1)
                    {
                        item.ImagesURL.Add(Url.Link("RequestImage", new { RequestId = item.RequestId, imageId = id }));
                        id++;
                    }

                }
            return PagedResponse("GetAllRequests", page, pagesize, requests.TotalCount, data);
        }

        [Route("api/Requests/{RequestId:long}/Image/{imageId:int}", Name = "RequestImage")]
        public HttpResponseMessage GetRequestImage(long RequestId, int imageId, string type = "orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Ticket-" + RequestId)
                        .FirstOrDefault(x => Path.GetFileName(x).Split('.')[0] == imageId.ToString() &&
                                             !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Ticket-" + RequestId)
                        .FirstOrDefault(x => Path.GetFileName(x).Split('.')[0] == imageId.ToString() &&
                                             Path.GetFileName(x).Contains("thumb"));


                HttpResponseMessage Response = new HttpResponseMessage(HttpStatusCode.OK);

                byte[] fileData = File.ReadAllBytes(filePath);

                Response.Content = new ByteArrayContent(fileData);
                Response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return Response;
            }
            catch (Exception e)
            {
                return new HttpResponseMessage();
            }
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor, Enums.RoleType.Waiter, Enums.RoleType.Technician)]
        [Route("api/Requests/{requestId:long}/Approve", Name = "ApproveRequest")]
        [HttpPost]
        public IHttpActionResult ApproveRequest(long requestId, [FromBody] RequestModel requestModel)
        {
            _requestFacade.ApproveRequest(requestId, UserId, Mapper.Map<List<RequestDetailDto>>(requestModel.RequestDetails));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor, Enums.RoleType.Waiter, Enums.RoleType.Technician)]
        [Route("api/Requests/{requestId:long}/Reject", Name = "RejectRequest")]
        [HttpPost]
        public IHttpActionResult RejectRequest(long requestId, [FromBody] RequestModel requestModel)
        {
            _requestFacade.RejectRequest(requestId, UserId,Mapper.Map<RequestDto>(requestModel));
            return Ok();
        }
    }
}
