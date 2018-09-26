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
    public class FloorFacade:BaseFacade,IFloorFacade
    {
        private IFloorService _floorService;
        public FloorFacade(IUnitOfWorkAsync unitOfWork, IFloorService floorService) : base(unitOfWork)
        {
            _floorService = floorService;
        }
        public void AddFloor (FloorDto floorDto, long adminId)
        {
            ValidateFloor(floorDto, adminId);
            var floor = Mapper.Map<Floor>(floorDto);
            floor.AdminId = adminId;
            _floorService.Insert(floor);
            SaveChanges();
        }

        public PagedResultsDto GetAllFloor(long adminId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _floorService.Query(x => x.AdminId == adminId && !x.IsDeleted).Select().Count();
            results.Data = Mapper.Map<List<FloorDto>>(pageSize > 0
                ? _floorService.Query(x => x.AdminId == adminId && !x.IsDeleted).Select().OrderBy(x => x.FloorId)
                    .Skip((page - 1) * pageSize).Take(pageSize).ToList()
                : _floorService.Query(x => x.AdminId == adminId && !x.IsDeleted).Select().ToList());
            return results;
        }
        private void ValidateFloor(FloorDto floorDto, long adminId)
        {
            if (_floorService.CheckFloorRepeated(floorDto, adminId)) throw new ValidationException(ErrorCodes.FloorNameRepeated);
        }

        public void UpdateFloor (FloorDto floorDto, long adminId)
        {
            ValidateFloor(floorDto, adminId);
            var floor = _floorService.Find(floorDto.FloorId);
            floor.FloorName = floorDto.FloorName;
            _floorService.Update(floor);
            SaveChanges();
        }

        public void DeleteFloor (long floorId)
        {
            var floor = _floorService.Find(floorId);
            floor.IsDeleted = true;
            _floorService.Update(floor);
            SaveChanges();
        }
    }
}
