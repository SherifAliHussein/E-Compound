using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using E_Compound.DAL.Entities.Model;

namespace E_Compound.BLL.DataServices
{
    public class UnitService : Service<Unit>, IUnitService
    {
        public UnitService(IRepositoryAsync<Unit> repository) : base(repository)
        {

        }

        public PagedResultsDto GetAllPagingUnits(long userId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Queryable().Count(x => x.CreationBy == userId);
            results.Data = Mapper.Map<List<Unit>, List<UnitDto>>(_repository.Query(x => x.CreationBy == userId).Select().OrderBy(x => x.UnitId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList());
            return results;
        }
    }
}
