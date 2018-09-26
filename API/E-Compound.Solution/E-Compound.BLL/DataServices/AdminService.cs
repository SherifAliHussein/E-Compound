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
    public class AdminService:Service<Admin>,IAdminService
    {
        public AdminService(IRepositoryAsync<Admin> repository):base(repository)
        {
            
        }
        public Admin GetAdminByAccountId(Guid userAccountId)
        {
            return _repository.Query(x => x.UserAccountId == userAccountId).Select().FirstOrDefault();
        }
    }
}
