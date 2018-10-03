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
using E_Compound.Common.CustomException;

namespace E_Compound.API.Controllers
{
    public class FeaturesController : BaseApiController
    {
        public IFeatureFacade _FeatureFacade { get; set; }
        public IRequestFacade _RequestFacade { get; set; }

        public FeaturesController(IFeatureFacade featureFacade,IRequestFacade requestFacade)
        {
            _FeatureFacade = featureFacade;
            _RequestFacade = requestFacade;
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Features", Name = "AddFeature")]
        [HttpPost]
        public IHttpActionResult AddFeature()
        {
            //_FeatureFacade.AddFeature(Mapper.Map<FeatureDto>(featureModel), UserId);
            //return Ok();

            if (!HttpContext.Current.Request.Files.AllKeys.Any())
                throw new ValidationException(ErrorCodes.EmptyImage);
            var httpPostedFile = HttpContext.Current.Request.Files[0];

            var featureModel = new JavaScriptSerializer().Deserialize<FeatureModel>(HttpContext.Current.Request.Form.Get(0));

            if (httpPostedFile == null)
                throw new ValidationException(ErrorCodes.EmptyImage);

            if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                throw new ValidationException(ErrorCodes.ImageExceedSize);


            if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                throw new ValidationException(ErrorCodes.InvalidImageType);

            var featureDto = Mapper.Map<FeatureDto>(featureModel);
            //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
            featureDto.Image = new MemoryStream();
            httpPostedFile.InputStream.CopyTo(featureDto.Image);

            _FeatureFacade.AddFeature(featureDto, UserId, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.Admin,Enums.RoleType.Supervisor)]
        [Route("api/Features/", Name = "GetAllFeatures")]
        [HttpGet]
        [ResponseType(typeof(List<FeatureModel>))]
        public IHttpActionResult GetAllFeatures(int page = Page, int pagesize = PageSize)
        {
            if (UserRole == Enums.RoleType.Admin.ToString())
            {
                PagedResultsDto features = _FeatureFacade.GetAllFeatures(UserId, page, pagesize);
                var data = Mapper.Map<List<FeatureModel>>(features.Data);
                foreach (var feature in data)
                {
                    feature.ImageURL = Url.Link("FeatureImage", new {feature.FeatureId});
                }
                return PagedResponse("GetAllFeatures", page, pagesize, features.TotalCount, data);
            }
            else
            {
                var feature =Mapper.Map<List<FeatureModel>>(_FeatureFacade.GetAllFeatureForSupervisor(UserId));
                return Ok(feature);
            }
        }
        [AuthorizeRoles(Enums.RoleType.Admin, Enums.RoleType.Supervisor, Enums.RoleType.Receptionist,Enums.RoleType.Waiter)]
        [Route("api/Features/Name", Name = "GetAllFeatureName")]
        [HttpGet]
        [ResponseType(typeof(List<FeatureNameModel>))]
        public IHttpActionResult GetAllFeatureName()
        {
            var feature = Mapper.Map<List<FeatureNameModel>>(_FeatureFacade.GetAllFeatureName(UserId,UserRole));
            return Ok(feature);
        }
        [AuthorizeRoles(Enums.RoleType.Admin,Enums.RoleType.Room)]
        [Route("api/Features/Activated", Name = "GetAllActiveFeatures")]
        [HttpGet]
        [ResponseType(typeof(List<FeatureModel>))]
        public IHttpActionResult GetAllActiveFeatures(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto features = _FeatureFacade.GetAllActiveFeatures(UserId, page, pagesize,UserRole);
            var data = Mapper.Map<List<FeatureModel>>(features.Data);
            foreach (var feature in data)
            {
                feature.ImageURL = Url.Link("FeatureImage", new { feature.FeatureId });
                if (feature.Type == Enums.FeatureType.Restaurant.ToString())
                {
                    foreach (var restaurant in feature.Restaurants)
                    {
                        restaurant.ImageURL = Url.Link("RestaurantLogo", new { restaurant.RestaurantId });
                    }
                }
            }
            return PagedResponse("GetAllActiveFeatures", page, pagesize, features.TotalCount, data);
        }
        [AuthorizeRoles(Enums.RoleType.Admin, Enums.RoleType.Supervisor, Enums.RoleType.Waiter)]
        [Route("api/Features/{featureId:long}", Name = "GetFeature")]
        [HttpGet]
        [ResponseType(typeof(FeatureModel))]
        public IHttpActionResult GetFeature(long featureId)
        {
            var feature =  Mapper.Map<FeatureModel>(_FeatureFacade.GetFeature(featureId));
            feature.ImageURL = Url.Link("FeatureImage", new { feature.FeatureId });

            return Ok(feature);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Features/{featureId:long}", Name = "DeleteFeature")]
        [HttpDelete]
        public IHttpActionResult DeleteFeature(long featureId)
        {
            _FeatureFacade.DeleteFeature(featureId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Features/{featureId:long}/Activate", Name = "ActivateFeature")]
        [HttpGet]
        public IHttpActionResult ActivateFeature(long featureId)
        {
            _FeatureFacade.ActivateFeature(featureId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Features/{featureId:long}/DeActivate", Name = "DeActivateFeature")]
        [HttpGet]
        public IHttpActionResult DeActivateFeature(long featureId)
        {
            _FeatureFacade.DeActivateFeature(featureId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Features", Name = "UpdateFeature")]
        [HttpPut]
        public IHttpActionResult UpdateFeature()
        {
            var featureModel =
                new JavaScriptSerializer().Deserialize<FeatureModel>(HttpContext.Current.Request.Form.Get(0));
            var featureDto = Mapper.Map<FeatureDto>(featureModel);
            if (featureDto.IsImageChange)
            {
                if (!HttpContext.Current.Request.Files.AllKeys.Any())
                    throw new ValidationException(ErrorCodes.EmptyImage);
                var httpPostedFile = HttpContext.Current.Request.Files[0];
                

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);

                featureDto.Image = new MemoryStream();
                httpPostedFile.InputStream.CopyTo(featureDto.Image);
            }
            _FeatureFacade.UpdateFeature(featureDto, UserId, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }


        [Route("api/Features/{featureId:long}/Image", Name = "FeatureImage")]
        public HttpResponseMessage GetFeatureImage(long featureId, string type = "orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Feature-" + featureId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(featureId.ToString()) && !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Feature-" + featureId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(featureId.ToString()) && Path.GetFileName(x).Contains("thumb"));


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
        [Route("api/Features/Restaurant", Name = "CheckFeatureAsRestaurant")]
        [HttpGet]
        public IHttpActionResult CheckFeatureAsRestaurant()
        {
            return Ok(Mapper.Map<FeatureModel>(_FeatureFacade.CheckFeatureAsRestaurant(UserId)));
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Features/Invitation", Name = "CheckFeatureAsInvitation")]
        [HttpGet]
        public IHttpActionResult CheckFeatureAsInvitation()
        {
            return Ok(Mapper.Map<FeatureModel>(_FeatureFacade.CheckFeatureAsInvitation(UserId)));
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Features/Ticket", Name = "CheckFeatureAsTicket")]
        [HttpGet]
        public IHttpActionResult CheckFeatureAsTicket()
        {
            return Ok(Mapper.Map<FeatureModel>(_FeatureFacade.CheckFeatureAsTicket(UserId)));
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Detail/{featureControlId:long}", Name = "GeFeatureDetails")]
        [HttpGet]
        [ResponseType(typeof(List<FeatureDetailModel>))]
        public IHttpActionResult GeFeatureDetails(long featureControlId, int page = Page, int pagesize = PageSize)
        {

            var featureDetail = _FeatureFacade.GeFeatureDetails(featureControlId, page, pagesize);
            var data = Mapper.Map<List<FeatureDetailModel>>(featureDetail.Data);
            foreach (var featureDetailModel in data)
            {
                featureDetailModel.ImageURL = Url.Link("FeatureDetailImage",
                    new
                    {
                        featureId = featureDetailModel.FeatureId,
                        featureDetailId = featureDetailModel.FeatureDetailId
                    });
            }
            return PagedResponse("GeFeatureDetails", page, pagesize, featureDetail.TotalCount, data);

        }

        [Route("api/Features/{featureId:long}/Detail/{featureDetailId:long}/Image", Name = "FeatureDetailImage")]
        public HttpResponseMessage GetFeatureDetailImage(long featureId, long featureDetailId, string type = "orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Feature-" + featureId + "\\Detail-" + featureDetailId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(featureDetailId.ToString()) && !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\" + "Feature-" + featureId + "\\Detail-" + featureDetailId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(featureDetailId.ToString()) && Path.GetFileName(x).Contains("thumb"));


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
        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Detail", Name = "AddFeatureDetail")]
        [HttpPost]
        public IHttpActionResult AddFeatureDetail()
        {
            HttpPostedFile httpPostedFile = null;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                httpPostedFile = HttpContext.Current.Request.Files[0];
                if (httpPostedFile == null)
                    throw new ValidationException(ErrorCodes.EmptyImage);

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);
            }
            var featureDetailModel = new JavaScriptSerializer().Deserialize<FeatureDetailModel>(HttpContext.Current.Request.Form.Get(0));

            

            var featureDetailDto = Mapper.Map<FeatureDetailDto>(featureDetailModel);
            //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
            featureDetailDto.Image = new MemoryStream();
            if (httpPostedFile != null) httpPostedFile.InputStream.CopyTo(featureDetailDto.Image);

            _FeatureFacade.AddFeatureDetail(featureDetailDto, UserId, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }
        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Detail", Name = "UpdateFeatureDetail")]
        [HttpPut]
        public IHttpActionResult UpdateFeatureDetail()
        {
            HttpPostedFile httpPostedFile = null;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                httpPostedFile = HttpContext.Current.Request.Files[0];
                if (httpPostedFile == null)
                    throw new ValidationException(ErrorCodes.EmptyImage);

                if (httpPostedFile.ContentLength > 2 * 1024 * 1000)
                    throw new ValidationException(ErrorCodes.ImageExceedSize);


                if (Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpg" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".png" &&
                    Path.GetExtension(httpPostedFile.FileName).ToLower() != ".jpeg")

                    throw new ValidationException(ErrorCodes.InvalidImageType);
            }
            var featureDetailModel = new JavaScriptSerializer().Deserialize<FeatureDetailModel>(HttpContext.Current.Request.Form.Get(0));



            var featureDetailDto = Mapper.Map<FeatureDetailDto>(featureDetailModel);
            //restaurantDto.Image = (MemoryStream) restaurant.Image.InputStream;
            featureDetailDto.Image = new MemoryStream();
            if (httpPostedFile != null) httpPostedFile.InputStream.CopyTo(featureDetailDto.Image);

            _FeatureFacade.UpdateFeatureDetail(featureDetailDto, UserId, HostingEnvironment.MapPath("~/Images/"));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Detail/{featureDetailId:long}", Name = "DeleteFeatureDetail")]
        [HttpDelete]
        public IHttpActionResult DeleteFeatureDetail(long featureDetailId)
        {
            _FeatureFacade.DeleteFeatureDetail(featureDetailId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Control/{featureControlId:long}/Switch", Name = "SwitchFeatureControlType")]
        [HttpGet]
        public IHttpActionResult SwitchFeatureControlType(long featureControlId)
        {
            _FeatureFacade.SwitchFeatureControlType(featureControlId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/Features/{featureId:long}/Info", Name = "GetFeatureInfo")]
        [HttpGet]
        [ResponseType(typeof(FeatureInfoModel))]
        public IHttpActionResult GetFeatureInfo(long featureId)
        {
            var featureInfo = Mapper.Map<FeatureInfoModel>(_FeatureFacade.GetAllFeatureControlDetailDtos(featureId));
            featureInfo.ImageURL = Url.Link("FeatureImage", new { featureInfo.FeatureId });
            foreach (var controlDetailModel in featureInfo.FeatureControl)
            {
                foreach (var featureDetailModel in controlDetailModel.FeatureDetails)
                {
                    featureDetailModel.ImageURL = Url.Link("FeatureDetailImage",
                        new
                        {
                            featureId = featureDetailModel.FeatureId,
                            featureDetailId = featureDetailModel.FeatureDetailId
                        });
                }
            }

            return Ok(featureInfo);
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Detail/Available", Name = "AddFeatureDetailAvailability")]
        [HttpPost]
        public IHttpActionResult AddFeatureDetailAvailability([FromBody] FeatureDetailModel featureDetailModel)
        {
            var featureDetailDto = Mapper.Map<FeatureDetailDto>(featureDetailModel);

            _FeatureFacade.AddFeatureDetailAvailability(featureDetailDto, UserId);
            return Ok();
        }
        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Detail/Available", Name = "UpdateFeatureDetailAvailability")]
        [HttpPut]
        public IHttpActionResult UpdateFeatureDetailAvailability([FromBody] FeatureDetailModel featureDetailModel)
        {
            var featureDetailDto = Mapper.Map<FeatureDetailDto>(featureDetailModel);
            
            _FeatureFacade.UpdateFeatureDetailAvailability(featureDetailDto, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor)]
        [Route("api/Features/Detail/Available/{availableId:long}", Name = "DeleteAvailable")]
        [HttpDelete]
        public IHttpActionResult DeleteAvailable(long availableId)
        {
            _FeatureFacade.DeleteAvailable(availableId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/Features/{featureId:long}/Requests/", Name = "GetAllRequestsByFeatureId")]
        [HttpGet]
        [ResponseType(typeof(List<RequestDetailModel>))]
        public IHttpActionResult GetAllRequestsByFeatureId(long featureId)
        {
            var data = Mapper.Map<List<RequestDetailModel>>(_RequestFacade.GetAllRequestDetailByFeatureId(featureId));

            return Ok(data);
        }


        [AuthorizeRoles(Enums.RoleType.Room)]
        [Route("api/Features/{featureId:long}/LastRequest/", Name = "GetLastRequestByFeaturedId")]
        [HttpGet]
        [ResponseType(typeof(List<RequestDetailModel>))]
        public IHttpActionResult GetLastRequestByFeaturedId(long featureId)
        {
            var data = Mapper.Map<RequestStatusModel>(_RequestFacade.GetLastRequestByFeaturedId(featureId,UserId));
            return Ok(data);
        }
    }
}
