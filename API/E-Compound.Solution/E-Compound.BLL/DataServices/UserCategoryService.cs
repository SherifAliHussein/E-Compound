using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class UserCategoryService : Service<UserCategory>, IUserCategoryService
    {
        public UserCategoryService(IRepositoryAsync<UserCategory> repository) : base(repository)
        {

        }
        public PagedResultsDto GetAllPagingUserCategories(long userId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Queryable().Count(x => !x.IsDeleted && x.CreationBy == userId);
            var data = _repository.Query(x => !x.IsDeleted && x.CreationBy == userId).Select()
                .OrderBy(x => x.UserCategoryId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            results.Data = Mapper.Map<List<UserCategory>, List<UserCategoryDto>>(data);
            return results;
        }

        public List<UserCategory> GetUserCategories(long userId)
        {
            var userCategories = _repository.Query(x => x.CreationBy == userId && x.IsDeleted != true).Select().ToList();

            return userCategories;
        }

    }
}
