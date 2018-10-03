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
    public class UnitFacade : BaseFacade, IUnitFacade
    {
        private IUnitService _unitService;
        private IRoomService _roomService;
        public UnitFacade(IUnitService unitService, IRoomService roomService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _unitService = unitService;
            _roomService = roomService;
        }

        public PagedResultsDto GetAllPagingUnits(long userId, int page, int pageSize)
        {
            var units = _unitService.GetAllPagingUnits(userId, page, pageSize);
            return units;
        }

        public void AddUnit(UnitDto unitDto, long userId)
        {
            var unit = Mapper.Map<Unit>(unitDto);

            unit.CreateTime = DateTime.Now;
            unit.CreationBy = userId;
            unit.Name = unitDto.Name;
            unit.UnitTypeId = unitDto.UnitTypeId;

            _unitService.Insert(unit);
            SaveChanges();
        }

        public void UpdateUnit(long userId, UnitDto unitDto)
        {
            var unit = _unitService.Find(unitDto.UnitId);
            if (unit == null) throw new NotFoundException(ErrorCodes.UnitNotFound);

            unit.ModifyTime = DateTime.Now;
            unit.ModifiedBy = userId;
            unit.Name = unitDto.Name;
            unit.UnitTypeId = unitDto.UnitTypeId;
            _unitService.Update(unit);
            
            SaveChanges();
        }

        public void DeleteUnit(long userId, long unitId)
        {
            var unit = _unitService.Find(unitId);
            if (unit == null) throw new NotFoundException(ErrorCodes.UnitNotFound);

            var hasRelation = _roomService.RelationValidation(userId, unitId);
            if (hasRelation != null) throw new NotFoundException(ErrorCodes.UnitHasRelation);
            unit.IsDeleted = true;
            _unitService.Update(unit);
            SaveChanges();
        }
    }
}
