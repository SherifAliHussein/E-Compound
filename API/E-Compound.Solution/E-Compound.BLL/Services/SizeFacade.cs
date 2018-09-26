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
    public class SizeFacade:BaseFacade,ISizeFacade
    {
        private ISizeService _sizeService;
        private IRestaurantService _restaurantService;
        private ISizeTranslationService _sizeTranslationService;
        private IItemSizeService _itemSizeService;
        public SizeFacade(ISizeService sizeService, ISizeTranslationService sizeTranslationService, IRestaurantService restaurantService, IItemSizeService itemSizeService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _sizeTranslationService = sizeTranslationService;
            _sizeService = sizeService;
            _restaurantService = restaurantService;
            _itemSizeService = itemSizeService;
        }
        public SizeFacade(ISizeService sizeService, ISizeTranslationService sizeTranslationService, IRestaurantService restaurantService)
        {
            _sizeTranslationService = sizeTranslationService;
            _sizeService = sizeService;
            _restaurantService = restaurantService;

        }
        public PagedResultsDto GetAllSizes(string language,long userId, int page, int pageSize)
        {
            return _sizeTranslationService.GetAllSizes(language,userId, page, pageSize);
        }

        public void AddSize(SizeDto sizeDto,long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            ValidateSize(sizeDto, restaurantAdminId);
            var size = new Size();

            foreach (var sizeName in sizeDto.SizeNameDictionary)
            {
                size.SizeTranslations.Add(new SizeTranslation
                {
                    SizeName = sizeName.Value,
                    Language = sizeName.Key.ToLower()
                });
            }
            size.RestaurantId = restaurant.RestaurantId;
            _sizeTranslationService.InsertRange(size.SizeTranslations);
            _sizeService.Insert(size);
            SaveChanges();
        }
        public void DeleteSize(long sizeId)
        {
            var size = _sizeService.Find(sizeId);
            if (size == null) throw new NotFoundException(ErrorCodes.SizeNotFound);
            if(_itemSizeService.Query(x=>x.SizeId == sizeId && !x.Item.IsDeleted).Select().Any()) throw new ValidationException(ErrorCodes.SizeHasItems);
            size.IsDeleted = true;
            _sizeService.Update(size);
            SaveChanges();
        }
        public SizeDto GetSize(long sizeId)
        {
            var size = _sizeService.Find(sizeId);
            if (size == null) throw new NotFoundException(ErrorCodes.SizeNotFound);
            if (size.IsDeleted) throw new NotFoundException(ErrorCodes.SizeDeleted);
            //return Mapper.Map<Size, SizeDto>(size, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.SizeTranslations = src.SizeTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower())
            //                .ToList();
            //        }
            //    );
            //});
            return Mapper.Map<SizeDto>(size);
        }

        private void ValidateSize(SizeDto sizeDto,long restaurantAdminId)
        {
            foreach (var sizeName in sizeDto.SizeNameDictionary)
            {
                if (string.IsNullOrEmpty(sizeName.Value)) throw new ValidationException(ErrorCodes.EmptySizeName);
                if (sizeName.Value.Length > 100) throw new ValidationException(ErrorCodes.SizeNameExceedLength);
                if (sizeName.Value.Length < 3) throw new ValidationException(ErrorCodes.SizeNameMinimumLength);
                if (_sizeTranslationService.CheckSizeNameExist(sizeName.Value, sizeName.Key, sizeDto.SizeId,
                    restaurantAdminId)) throw new ValidationException(ErrorCodes.SizeNameAlreadyExist);
            }
        }

        public void UpdateSize(SizeDto sizeDto, long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            var size = _sizeService.Find(sizeDto.SizeId);
            if (size == null) throw new NotFoundException(ErrorCodes.SizeNotFound);
            ValidateSize(sizeDto, restaurantAdminId);
            foreach (var sizeName in sizeDto.SizeNameDictionary)
            {
                var sizeTranslation =
                    size.SizeTranslations.FirstOrDefault(x => x.Language.ToLower() == sizeName.Key.ToLower());
                if (sizeTranslation == null)
                {
                    size.SizeTranslations.Add(new SizeTranslation
                    {
                        Language = sizeName.Key.ToLower(),
                        SizeName = sizeName.Value
                    });
                }
                else
                {
                    sizeTranslation.SizeName = sizeName.Value;
                }
            }
            size.RestaurantId = restaurant.RestaurantId;
            _sizeService.Update(size);
            SaveChanges();
        }
    }
}
