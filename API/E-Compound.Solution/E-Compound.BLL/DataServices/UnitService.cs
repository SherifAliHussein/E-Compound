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
            results.TotalCount = _repository.Queryable().Count(x => x.AdminId == userId);
            results.Data = Mapper.Map<List<Unit>, List<UnitDto>>(_repository.Query(x => x.AdminId == userId && !x.IsDeleted).Select().OrderBy(x => x.UnitId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList());
            return results;
        }

        public Unit RelationValidation(long userId, long unitTypeId)
        {
            var unit = _repository.Query(x => x.UnitTypeId == unitTypeId && x.AdminId == userId).Select()
                .FirstOrDefault();
            return unit;
        }

        public int GetUnitCountByPackageId(long packageId)
        {
            return _repository.Query(x => !x.IsDeleted && x.PackageId == packageId).Select().Count();
        }

        public int GetConsumedUnits(long userId)
        {
            return _repository.Query(x => !x.IsDeleted && x.AdminId == userId).Select().Count();
        }
    }
}
