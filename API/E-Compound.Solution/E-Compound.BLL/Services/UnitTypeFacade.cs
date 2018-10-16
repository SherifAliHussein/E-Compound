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
    public class UnitTypeFacade : BaseFacade, IUnitTypeFacade
    {
        private IUnitTypeService _unitTypeService;
        private IUnitService _unitService;
        private IUnitTypeTranslationService _unitTypeTranslationService;


        public UnitTypeFacade(IUnitTypeService unitTypeService, IUnitService unitService, IUnitTypeTranslationService unitTypeTranslationService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _unitTypeService = unitTypeService;
            _unitTypeTranslationService = unitTypeTranslationService;
            _unitService = unitService;
        }

        public PagedResultsDto GetAllPagingUnitTypes(long userId, int page, int pageSize)
        {
            var unitTypes = _unitTypeService.GetAllPagingUnitTypes(userId, page, pageSize);

            return unitTypes;
        }

        public List<UnitTypeDto> GetUnitTypes(long userId)
        {
            var unitTypes = _unitTypeService.GetUnitTypes(userId);

            return Mapper.Map<List<UnitTypeDto>>(unitTypes);
        }

        public void DeleteUnitType(long userId, long unitTypeId)
        {
            var unitType = _unitTypeService.Find(unitTypeId);
            if (unitType == null) throw new NotFoundException(ErrorCodes.UnitTypeNotFound);

            var hasRelation = _unitService.RelationValidation(userId, unitTypeId);
            if (hasRelation != null) throw new NotFoundException(ErrorCodes.UnitTypeHasRelation);
            unitType.IsDeleted = true;
            _unitTypeService.Update(unitType);
            SaveChanges();
        }

        public void AddUnitType(UnitTypeDto unitTypeDto, long userId)
        {
            var unitType = Mapper.Map<UnitType>(unitTypeDto);
        
            unitType.CreateTime = DateTime.Now;
            unitType.AdminId = userId;
            unitType.IsDeleted = false;
            unitType.Limit = unitTypeDto.Limit;

            _unitTypeTranslationService.InsertRange(unitType.UnitTypeTranslations);
            _unitTypeService.Insert(unitType);
            SaveChanges();
        }

        public void UpdateUnitType(long userId, UnitTypeDto unitTypeDto)
        {
            var unitType = _unitTypeService.Find(unitTypeDto.UnitTypeId);
            if (unitType == null) throw new NotFoundException(ErrorCodes.UnitTypeNotFound);

            unitType.ModifyTime = DateTime.Now;
            unitType.Limit = unitTypeDto.Limit;
            unitType.ModifiedBy = userId;

            _unitTypeService.Update(unitType);

            var unitTypeTrans = _unitTypeTranslationService.GetTransByUnitTypeId(unitType.UnitTypeId);

            for (int i = 0; i < 2; i++)
            {
                if (unitTypeTrans[i].Language == "en-us")
                {
                    unitTypeTrans[i].Name = unitTypeDto.TitleDictionary["en-us"];
                    SaveChanges();
                }
                else if (unitTypeTrans[i].Language == "ar-eg")
                {
                    unitTypeTrans[i].Name = unitTypeDto.TitleDictionary["ar-eg"];
                    SaveChanges();
                }
            }
            SaveChanges();
        }

    }
}
