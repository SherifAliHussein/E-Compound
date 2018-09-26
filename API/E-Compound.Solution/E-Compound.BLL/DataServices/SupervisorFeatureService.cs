using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class SupervisorFeatureService:Service<SupervisorFeature>,ISupervisorFeatureService
    {
        public SupervisorFeatureService(IRepositoryAsync<SupervisorFeature> repository):base(repository)
        {
            _repository = repository;
        }
        //public List<SupervisorDto> GetAllSupervisors(long adminId, int page, int pageSize)
        //{
        //    var supervisors = _repository
        //        .Query(x => !x.Feature.IsDeleted && !x.Supervisor.IsDeleted && x.Supervisor.AdminId == adminId && x.Feature.CreationBy == adminId).Select()
        //        .OrderBy(x => x.SupervisorId).Skip((page - 1) * pageSize)
        //        .Take(pageSize).GroupBy(x=> { x.Supervisor }).ToList();
        //    foreach (var supervisor in supervisors)
        //    {
        //        supervisor.
        //    }
        //    return supervisors;
        //}
        public void DeleteRange(List<SupervisorFeature> features)
        {
            foreach (var supervisorFeature in features)
            {
                _repository.Delete(supervisorFeature.SupervisorFeatureId);
            }
        }
    }
}
