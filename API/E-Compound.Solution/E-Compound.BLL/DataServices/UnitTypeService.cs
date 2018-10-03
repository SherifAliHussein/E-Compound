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
    public class UnitTypeService : Service<UnitType>, IUnitTypeService
    {
        public UnitTypeService(IRepositoryAsync<UnitType> repository) : base(repository)
        {

        }
        public PagedResultsDto GetAllPagingUnitTypes(long userId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Queryable().Count(x => !x.IsDeleted && x.CreationBy == userId);
            var data = _repository.Query(x => !x.IsDeleted && x.CreationBy == userId).Select()
                .OrderBy(x => x.UnitTypeId).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            results.Data = Mapper.Map<List<UnitType>, List<UnitTypeDto>>(data);
            return results;
        }

        public List<UnitType> GetUnitTypes(long userId)
        {
            var unitTypes = _repository.Query(x => x.CreationBy == userId && x.IsDeleted != true).Select().ToList();

            return unitTypes;
        }

       
    }
}
