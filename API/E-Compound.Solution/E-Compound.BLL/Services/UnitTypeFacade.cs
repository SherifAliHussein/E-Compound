using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private IUnitTypeTranslationService _unitTypeTranslationService;


        public UnitTypeFacade(IUnitTypeService unitTypeService, IUnitTypeTranslationService unitTypeTranslationService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _unitTypeService = unitTypeService;
            _unitTypeTranslationService = unitTypeTranslationService;
        }

        public PagedResultsDto GetAllPagingUnitTypes(long userId, int page, int pageSize)
        {
            var unitTypes = _unitTypeService.GetAllPagingUnitTypes(userId, page, pageSize);

            return unitTypes;
        }

        public void DeleteUnitType(long unitTypeId)
        {
            var unitType = _unitTypeService.Find(unitTypeId);
            if (unitType == null) throw new NotFoundException(ErrorCodes.UnitTypeNotFound);
            unitType.IsDeleted = true;
            _unitTypeService.Update(unitType);
            SaveChanges();
        }

        public void AddUnitType(UnitTypeDto unitTypeDto, long userId)
        {
            var unitType = new UnitType();
            //var nameValid = _currencyService.emailValidation(currencyDto.ContactEmail);

            //foreach (var item in nameValid)
            //{
            //    if (nameValid != null && item.UserID == userID) throw new NotFoundException(ErrorCodes.RepeatedContactEmail);
            //}

            unitType.CreateTime = DateTime.Now;
            unitType.CreationBy = userId;
            unitType.IsDeleted = false;
            _unitTypeService.Insert(unitType);

            var unitTypeTrans = new UnitTypeTranslation();
            unitTypeTrans.Language = "en";
            unitTypeTrans.Name = unitTypeDto.TitleDictionary["en"];
            unitTypeTrans.UnitTypeId = unitType.UnitTypeId;
            _unitTypeTranslationService.Insert(unitTypeTrans);
            SaveChanges();
            unitTypeTrans.Language = "ar";
            unitTypeTrans.Name = unitTypeDto.TitleDictionary["ar"];
            unitTypeTrans.UnitTypeId = unitType.UnitTypeId;
            _unitTypeTranslationService.Insert(unitTypeTrans);
            SaveChanges();
        }

        public void UpdateUnitType(long userId, UnitTypeDto unitTypeDto)
        {
            var unitType = _unitTypeService.Find(unitTypeDto.UnitTypeId);
            if (unitType == null) throw new NotFoundException(ErrorCodes.UnitTypeNotFound);
            //var nameValid = _currencyService.emailValidation(currencyDto.ContactName);
            //foreach (var item in nameValid)
            //{
            //    if (nameValid != null && item.ContactID != contactDto.ContactID && item.UserID == userID) throw new NotFoundException(ErrorCodes.RepeatedContactEmail);
            //}
            // if (nameValid != null && nameValid.ContactID != contactDto.ContactID) throw new NotFoundException(ErrorCodes.RepeatedContactEmail);
            unitType.ModifyTime = DateTime.Now;
            unitType.ModifiedBy = userId;
            _unitTypeService.Update(unitType);

            var unitTypeTrans = _unitTypeTranslationService.GetTransByUnitTypeId(unitType.UnitTypeId);

            for (int i = 0; i < 2; i++)
            {
                if (unitTypeTrans[i].Language == "en")
                {
                    unitTypeTrans[i].Name = unitTypeDto.TitleDictionary["en"];
                    SaveChanges();
                }
                else if (unitTypeTrans[i].Language == "ar")
                {
                    unitTypeTrans[i].Name = unitTypeDto.TitleDictionary["ar"];
                    SaveChanges();
                }
            }
            SaveChanges();
        }

    }
}
