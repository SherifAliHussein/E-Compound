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
using E_Compound.BLL.Services.Interfaces;
using E_Compound.API.Providers;
using E_Compound.BLL.DTOs;
using E_Compound.Common;

namespace E_Compound.API.Controllers
{
    public class UsersController : BaseApiController
    {
        private IUserFacade _userFacade;

        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        #region Receptionist
        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Receptionist", Name = "AddReceptionist")]
        [HttpPost]
        public IHttpActionResult AddReceptionist([FromBody] ReceptionistModel receptionistModel)
        {
            _userFacade.AddReceptionist(Mapper.Map<ReceptionistDto>(receptionistModel), UserId);
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Receptionist/", Name = "GetAllReceptionists")]
        [HttpGet]
        [ResponseType(typeof(List<ReceptionistModel>))]
        public IHttpActionResult GetAllReceptionists(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto users = _userFacade.GetAllUsers(UserId, page, pagesize,Enums.RoleType.Receptionist);
            var data = Mapper.Map<List<ReceptionistModel>>(users.Data);

            return PagedResponse("GetAllReceptionists", page, pagesize, users.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Receptionist/{receptionistId:long}", Name = "GetReceptionist")]
        [HttpGet]
        [ResponseType(typeof(ReceptionistModel))]
        public IHttpActionResult GetReceptionist(long receptionistId)
        {
            return Ok(Mapper.Map<ReceptionistModel>(_userFacade.GetReceptionist(receptionistId)));
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Receptionist/{receptionistId:long}", Name = "DeleteReceptionist")]
        [HttpDelete]
        public IHttpActionResult DeleteReceptionist(long receptionistId)
        {
            _userFacade.DeleteReceptionist(receptionistId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Receptionist/{receptionistId:long}/Activate", Name = "ActivateReceptionist")]
        [HttpGet]
        public IHttpActionResult ActivateReceptionist(long receptionistId)
        {
            _userFacade.ActivateReceptionist(receptionistId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Receptionist/{receptionistId:long}/DeActivate", Name = "DeActivateReceptionist")]
        [HttpGet]
        public IHttpActionResult DeActivateReceptionist(long receptionistId)
        {
            _userFacade.DeActivateReceptionist(receptionistId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Receptionist", Name = "UpdateReceptionist")]
        [HttpPut]
        public IHttpActionResult UpdateReceptionist([FromBody] ReceptionistModel receptionistModel)
        {
            _userFacade.UpdateReceptionist(Mapper.Map<ReceptionistDto>(receptionistModel), UserId);
            return Ok();
        }
        #endregion

        #region Supervisor
        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Supervisor", Name = "AddSupervisor")]
        [HttpPost]
        public IHttpActionResult AddSupervisor([FromBody] SupervisorModel supervisorModel)
        {
            _userFacade.AddSupervisor(Mapper.Map<SupervisorDto>(supervisorModel), UserId);
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Supervisor/", Name = "GetAllSupervisors")]
        [HttpGet]
        [ResponseType(typeof(List<SupervisorModel>))]
        public IHttpActionResult GetAllSupervisors(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto users = _userFacade.GetAllUsers(UserId, page, pagesize, Enums.RoleType.Supervisor);
            var data = Mapper.Map<List<SupervisorModel>>(users.Data);

            return PagedResponse("GetAllSupervisors", page, pagesize, users.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Supervisor/{supervisorId:long}", Name = "GetSupervisor")]
        [HttpGet]
        [ResponseType(typeof(SupervisorModel))]
        public IHttpActionResult GetSupervisor(long supervisorId)
        {
            return Ok(Mapper.Map<SupervisorModel>(_userFacade.GetSupervisor(supervisorId)));
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Supervisor/{supervisorId:long}", Name = "DeleteSupervisor")]
        [HttpDelete]
        public IHttpActionResult DeleteSupervisor(long supervisorId)
        {
            _userFacade.DeleteSupervisor(supervisorId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Supervisor/{supervisorId:long}/Activate", Name = "ActivateSupervisor")]
        [HttpGet]
        public IHttpActionResult ActivateSupervisor(long supervisorId)
        {
            _userFacade.ActivateSupervisor(supervisorId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Supervisor/{supervisorId:long}/DeActivate", Name = "DeActivateSupervisor")]
        [HttpGet]
        public IHttpActionResult DeActivateSupervisor(long supervisorId)
        {
            _userFacade.DeActivateSupervisor(supervisorId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Supervisor", Name = "UpdateSupervisor")]
        [HttpPut]
        public IHttpActionResult UpdateSupervisor([FromBody] SupervisorModel supervisorModel)
        {
            _userFacade.UpdateSupervisor(Mapper.Map<SupervisorDto>(supervisorModel), UserId);
            return Ok();
        }
        #endregion

        #region Technician
        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Technician", Name = "AddTechnician")]
        [HttpPost]
        public IHttpActionResult AddTechnician([FromBody] TechnicianModel technicianModel)
        {
            _userFacade.AddTechnician(Mapper.Map<TechnicianDto>(technicianModel), UserId);
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Technician/", Name = "GetAllTechnicians")]
        [HttpGet]
        [ResponseType(typeof(List<TechnicianModel>))]
        public IHttpActionResult GetAllTechnicians(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto users = _userFacade.GetAllUsers(UserId, page, pagesize, Enums.RoleType.Technician);
            var data = Mapper.Map<List<TechnicianModel>>(users.Data);

            return PagedResponse("GetAllTechnicians", page, pagesize, users.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Technician/{technicianId:long}", Name = "GetTechnician")]
        [HttpGet]
        [ResponseType(typeof(TechnicianModel))]
        public IHttpActionResult GetTechnician(long technicianId)
        {
            return Ok(Mapper.Map<TechnicianModel>(_userFacade.GetTechnician(technicianId)));
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Technician/{technicianId:long}", Name = "DeleteTechnician")]
        [HttpDelete]
        public IHttpActionResult DeleteTechnician(long technicianId)
        {
            _userFacade.DeleteTechnician(technicianId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Technician/{technicianId:long}/Activate", Name = "ActivateTechnician")]
        [HttpGet]
        public IHttpActionResult ActivateTechnician(long technicianId)
        {
            _userFacade.ActivateTechnician(technicianId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Technician/{technicianId:long}/DeActivate", Name = "DeActivateTechnician")]
        [HttpGet]
        public IHttpActionResult DeActivateTechnician(long technicianId)
        {
            _userFacade.DeActivateTechnician(technicianId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/Technician", Name = "UpdateTechnician")]
        [HttpPut]
        public IHttpActionResult UpdateTechnician([FromBody] TechnicianModel technicianModel)
        {
            _userFacade.UpdateTechnician(Mapper.Map<TechnicianDto>(technicianModel), UserId);
            return Ok();
        }
        #endregion

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/GetMaxAndConUsers", Name = "GetMaxAndConUsers")]
        [HttpGet]
        public IHttpActionResult GetMaxAndConUsers()
        {
            UserConsumedModel maxCon = Mapper.Map<UserConsumedModel>(_userFacade.GetMaxAndConsumedUsers(UserId));

            return Ok(maxCon);

        }

        [Route("api/Users/Register", Name = "Register")]
        [HttpPost]
        public IHttpActionResult Register([FromBody] AdminModel adminModel)
        {
            //if (Request.Headers.Authorization.Scheme == "X-Auth-Token" &&
            //    Request.Headers.Authorization.Parameter == "asdasdas")

            _userFacade.AddNewGlobalUser(Mapper.Map<AdminDto>(adminModel));
            return Ok();
            //return Unauthorized();
        }

        [Route("api/Users/", Name = "UpdateGlobalUser")]
        [HttpPut]
        public IHttpActionResult UpdateGlobalUser([FromBody] AdminModel adminModel)
        {
            //if (Request.Headers.Authorization.Scheme == "X-Auth-Token" &&
            //    Request.Headers.Authorization.Parameter == "asdasdas")

            _userFacade.UpdateGlobalUser(Mapper.Map<AdminDto>(adminModel));
            return Ok();
            //return Unauthorized();
        }
        [Route("api/Users/Package", Name = "UpdatePackage")]
        [HttpPut]
        public IHttpActionResult UpdatePackage([FromBody] AdminModel adminModel)
        {
            //if (Request.Headers.Authorization.Scheme == "X-Auth-Token" &&
            //    Request.Headers.Authorization.Parameter == "asdasdas")

            _userFacade.UpdateAdminPackage(Mapper.Map<AdminDto>(adminModel));
            return Ok();
            //return Unauthorized();
        }
    }
}
