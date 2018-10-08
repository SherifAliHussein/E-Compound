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
    public class TechnicianCategoryService : Service<TechnicianCategory>, ITechnicianCategoryService
    {
        public TechnicianCategoryService(IRepositoryAsync<TechnicianCategory> repository):base(repository)
        {
            _repository = repository;
        }
     
        public void DeleteRange(List<TechnicianCategory> categories)
        {
            foreach (var technicianCategory in categories)
            {
                _repository.Delete(technicianCategory.TechnicianCategoryId);
            }
        }
    }
}
