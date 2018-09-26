using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
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
    public class TemplatesController : BaseApiController
    {
        private ITemplateFacade _templateFacade;
        public TemplatesController(ITemplateFacade templateFacade)
        {
            _templateFacade = templateFacade;
        }
        [Route("api/Templates/{templateId:long}", Name = "GetTemplateImage")]
        public HttpResponseMessage GetTemplateImage(long templateId, string type = "orignal")
        {
            try
            {
                string filePath = type == "orignal"
                    ? Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\Templates\\" + templateId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(templateId.ToString()) && !Path.GetFileName(x).Contains("thumb"))
                    : Directory.GetFiles(HostingEnvironment.MapPath("~/Images/") + "\\Templates\\" + templateId)
                        .FirstOrDefault(x => Path.GetFileName(x).Contains(templateId.ToString()) && Path.GetFileName(x).Contains("thumb"));


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

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Templates", Name = "GetTemplates")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<TemplateModel>))]
        public IHttpActionResult GetTemplates()
        {
            var templates = Mapper.Map<List<TemplateModel>>(_templateFacade.GetAllTemplates());
            foreach (var template in templates)
            {
                template.ImageURL = Url.Link("GetTemplateImage", new { templateId = template.Id});
            }
            //templates.Add(new TemplateModel {Id = 2, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL});
            //templates.Add(new TemplateModel { Id = 3, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            //templates.Add(new TemplateModel { Id = 4, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            //templates.Add(new TemplateModel { Id = 5, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            //templates.Add(new TemplateModel { Id = 6, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            //templates.Add(new TemplateModel { Id = 7, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            //templates.Add(new TemplateModel { Id = 8, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            //templates.Add(new TemplateModel { Id = 9, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            //templates.Add(new TemplateModel { Id = 10, ItemCount = 4, ImageURL = templates.FirstOrDefault().ImageURL });
            return Ok(templates);

        }

    }
}
