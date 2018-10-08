using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class TechnicianService : Service<Technician>, ITechnicianService
    {
        public TechnicianService(IRepositoryAsync<Technician> repository):base(repository)
        {
            _repository = repository;
        }

        public List<Technician> GetAllTechnicians(long adminId, int page, int pageSize)
        {
            var technicians = _repository
                .Query(x => !x.IsDeleted && x.AdminId == adminId).Select()
                .OrderBy(x => x.UserId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            return technicians;
        }

        public bool CheckUserNameDuplicated(string userName, long userId, long adminId)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && u.UserId != userId && u.AdminId == adminId);
        }
    }
}
