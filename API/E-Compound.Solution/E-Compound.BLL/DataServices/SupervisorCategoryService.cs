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
    public class SupervisorCategoryService : Service<SupervisorCategory>, ISupervisorCategoryService
    {
        public SupervisorCategoryService(IRepositoryAsync<SupervisorCategory> repository):base(repository)
        {
            _repository = repository;
        }

        public void DeleteRange(List<SupervisorCategory> categories)
        {
            foreach (var supervisorCategory in categories)
            {
                _repository.Delete(supervisorCategory.SupervisorCategoryId);
            }
        }
    }
}
