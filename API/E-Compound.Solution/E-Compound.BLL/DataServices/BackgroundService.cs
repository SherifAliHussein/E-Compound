using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.Common;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class BackgroundService : Service<Background>, IBackgroundService
    {
        public BackgroundService(IRepositoryAsync<Background> repository) : base(repository)
        {

        }
        public PagedResultsDto GetAllBackgrounds(int page, int pageSize,long userId)
        {
            var query = Queryable().Where(x => x.IsActive || x.UserId==userId); 
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
         
            results.Data = Mapper.Map<List<Background>, List<BackgroundDto>>(query.OrderBy(x => x.BackgroundId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList());
           
            return results;
        }
        public PagedResultsDto GetActivatedBackgroundByUserId(long userId, int page, int pageSize)
        {
            var query = Queryable().Where(x => x.IsActive && x.UserId == userId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
            results.Data = Mapper.Map<List<Background>, List<BackgroundDto>>(query.OrderBy(x => x.BackgroundId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList());
            return results;
        }
    }
}
