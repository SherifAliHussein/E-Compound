using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.Common;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class UserService:Service<User>,IUserService
    {
        public UserService(IRepositoryAsync<User> repository):base(repository)
        {
            _repository = repository;
        }

        public User ValidateUser(string userName, string password)
        {
            return _repository.Query(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password && !u.IsDeleted).Select().FirstOrDefault();

        }
        public User CheckUserIsDeleted(string userName, string password)
        {
            return _repository.Query(u => u.UserName.ToLower() == userName.ToLower() && u.Password == password).Select().FirstOrDefault();

        }
        public bool CheckUserNameDuplicated(string userName, long userId)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && u.UserId != userId);
        }
    }
}
