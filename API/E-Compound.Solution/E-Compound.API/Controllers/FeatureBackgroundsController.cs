using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
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
using static System.Web.Hosting.HostingEnvironment;


namespace E_Compound.API.Controllers
{
    public class FeatureBackgroundsController : BaseApiController
    {
        private IFeaturesBackgroundFacade _backgroundFacade;
        public FeatureBackgroundsController(IFeaturesBackgroundFacade backgroundFacade)
        {
            _backgroundFacade = backgroundFacade;
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/FeatureBackgrounds/", Name = "AddFeatureBackground")]
        [HttpPost]
        public IHttpActionResult AddFeatureBackground()
        {

            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyBackgroundImage);
            var httpPostedFile = HttpContext.Current.Request.Files[0];

            var backgroundModel = new JavaScriptSerializer().Deserialize<FeaturesBackgroundModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null)
                throw new ValidationException(ErrorCodes.EmptyBackgroundImage);

            if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                throw new ValidationException(ErrorCodes.ImageExceedSize);


            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var backgroundDto = Mapper.Map<FeaturesBackgroundDto>(backgroundModel);
            backgroundDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(backgroundDto.Image);
            backgroundDto.UserId = UserId;
            _backgroundFacade.AddBackground(backgroundDto, MapPath("~/Images"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/FeatureBackgrounds/{backgroundId:long}/Activate", Name = "ActivateFeatureBackground")]
        [HttpGet]
        public IHttpActionResult ActivateFeatureBackground(long backgroundId)
        {
            _backgroundFacade.ActivateBackground(backgroundId, UserId);
            return Ok();
        }

        [Route("api/FeatureBackgrounds/{backgroundId:long}/Image/", Name = "FeatureBackgroundImage")]
        public HttpResponseMessage GetFeatureBackgroundImage(long backgroundId, string type = "orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(MapPath("~/Images/Features Background"))
                        .FirstOrDefault(x => Path.GetFileName(x).Split('.')[0] == backgroundId.ToString() && !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(MapPath("~/Images/Features Background"))
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(backgroundId.ToString()) && Path.GetFileName(x).Contains("thumb"));


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

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/FeatureBackgrounds/", Name = "GetAllFeatureBackground")]
        [HttpGet]
        [ResponseType(typeof(List<FeaturesBackgroundModel>))]
        public IHttpActionResult GetAllFeatureBackground(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto backgorundObj = _backgroundFacade.GetAllBackgrounds(page, pagesize, UserId);

            var data = Mapper.Map<List<FeaturesBackgroundModel>>(backgorundObj.Data);

            foreach (var backgroundModel in data)
            {

                backgroundModel.ImageUrl = Url.Link("FeatureBackgroundImage", new { backgroundId= backgroundModel.FeaturesBackgroundId });

            }
            return PagedResponse("GetAllFeatureBackground", page, pagesize, backgorundObj.TotalCount, data);

        }


        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/FeatureBackgrounds/Activated/", Name = "GetFeatureBackground")]
        [HttpGet]
        [ResponseType(typeof(List<FeaturesBackgroundModel>))]
        public IHttpActionResult GetFeatureBackground(int page = Page, int pagesize = PageSize)
        {
            var backgorundObj = Mapper.Map<FeaturesBackgroundModel>(_backgroundFacade.GetActivateFeaturesBackground(UserId));
            backgorundObj.ImageUrl = Url.Link("FeatureBackgroundImage", new { backgroundId = backgorundObj.FeaturesBackgroundId });
            
            return Ok(backgorundObj);

        }

    }
}
