using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class BuildingFacade:BaseFacade,IBuildingFacade
    {
        private IBuildingService _buildingService;
        public BuildingFacade(IUnitOfWorkAsync unitOfWork, IBuildingService buildingService) : base(unitOfWork)
        {
            _buildingService = buildingService;
        }

        public void AddBuilding(BuildingDto buildingDto,long adminId)
        {
            ValidateBuilding(buildingDto,adminId);
            var building = Mapper.Map<Building>(buildingDto);
            building.AdminId = adminId;
            _buildingService.Insert(building);
            SaveChanges();
        }

        public PagedResultsDto GetAllBuilding(long adminId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _buildingService.Query(x => x.AdminId == adminId && !x.IsDeleted).Select().Count();
            results.Data = Mapper.Map<List<BuildingDto>>(pageSize > 0
                ? _buildingService.Query(x => x.AdminId == adminId && !x.IsDeleted).Select().OrderBy(x => x.BuildingId)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToList()
                : _buildingService.Query(x => x.AdminId == adminId && !x.IsDeleted).Select().ToList());
            return results;
        }
        private void ValidateBuilding(BuildingDto buildingDto, long adminId)
        {
            if(_buildingService.CheckBuildingRepeated(buildingDto,adminId)) throw new ValidationException(ErrorCodes.BuildingNameRepeated);
        }

        public void UpdateBuilding(BuildingDto buildingDto, long adminId)
        {
            ValidateBuilding(buildingDto,adminId);
            var building = _buildingService.Find(buildingDto.BuildingId);
            building.BuildingName = buildingDto.BuildingName;
            _buildingService.Update(building);
            SaveChanges();
        }

        public void DeleteBuilding(long buildingId)
        {
            var building = _buildingService.Find(buildingId);
            building.IsDeleted = true;
            _buildingService.Update(building);
            SaveChanges();
        }
    }
}
