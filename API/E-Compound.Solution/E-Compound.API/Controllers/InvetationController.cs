using System;
using System.Web.Http;
using E_Compound.API.Infrastructure;
using E_Compound.BLL.Services.Interfaces;

namespace E_Compound.API.Controllers
{
    public class InvetationController : BaseApiController
    {
        private readonly IInvetationFacade _invetationFacade;
        public InvetationController(IInvetationFacade invetationFacade)
        {
            _invetationFacade = invetationFacade;
        }

        [Route("api/Invetation/CheckUnitInvetationLimit", Name = "CheckUnitInvetationLimit")]
        [HttpGet]
        public IHttpActionResult GetAboutById(long featureId, string datetime, int limit)
        {
            bool requests = _invetationFacade.CheckUnitInvetationLimit(featureId, UserId, Convert.ToDateTime(datetime), limit);
            return Ok(requests);
        }
    }
}
