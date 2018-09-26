using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.BLL.Services.ManageStorage;
using E_Compound.Common;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class BackgroundFacade : BaseFacade, IBackgroundFacade
    {
        private readonly IBackgroundService _backgroundService;
        private readonly IRestaurantService _restaurantService;
        private readonly IManageStorage _manageStorage;

        public BackgroundFacade(IBackgroundService backgroundService, IManageStorage manageStorage, IRestaurantService restaurantService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _restaurantService = restaurantService;
            _backgroundService = backgroundService;
            _manageStorage = manageStorage;
        }

        public BackgroundFacade(IBackgroundService backgroundService, IManageStorage manageStorage, IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
            _backgroundService = backgroundService;
            _manageStorage = manageStorage;
        }

        public void AddBackground(BackgroundDto backgroundDto, string path)
        {
            var background = Mapper.Map<Background>(backgroundDto);

            background.CreationTime = DateTime.Now;
            background.LastModificationTime = DateTime.Now;
            background.DeletionTime = DateTime.Now;
            background.IsActive = true;
            _backgroundService.Insert(background);
            SaveChanges();

            var changeResturentBackground = _restaurantService.GetRestaurantByAdminId(backgroundDto.UserId);
            changeResturentBackground.BackgroundId = background.BackgroundId;
            _restaurantService.Update(changeResturentBackground);
            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + "Background", backgroundDto.Image, background.BackgroundId.ToString());
        }

        public BackgroundDto GetBackground(long backgroundId)
        {
            var background = _backgroundService.Find(backgroundId);
            if (background == null) throw new NotFoundException(ErrorCodes.BackgroundNotFound);
            if (background.IsDeleted) throw new NotFoundException(ErrorCodes.BackgroundDeleted);
            return Mapper.Map<Background, BackgroundDto>(background);
        }

        public PagedResultsDto GetAllBackgrounds(int page, int pageSize, long userId)
        {
            var backgroundObj = _backgroundService.GetAllBackgrounds(page, pageSize, userId);
            if (backgroundObj == null) throw new NotFoundException(ErrorCodes.BackgroundNotFound);
            
            foreach (var background in (IEnumerable<BackgroundDto>)backgroundObj.Data)
            {
                var checkResturentBackground = _restaurantService.GetRestaurantByAdminId(userId).BackgroundId == background.BackgroundId;
                if (checkResturentBackground)
                    background.IsActive = true;
                else
                    background.IsActive = false;

            }
            var results = backgroundObj;
            return results;
        }

        public PagedResultsDto GetActivatedBackgroundByUserId(long userId, int page, int pageSize)
        {
            var backgroundObj = _backgroundService.Find(userId);
            if (backgroundObj == null) throw new NotFoundException(ErrorCodes.BackgroundNotFound);
            if (backgroundObj.IsDeleted) throw new ValidationException(ErrorCodes.BackgroundDeleted);
            var results = _backgroundService.GetActivatedBackgroundByUserId(userId, page, pageSize);
            return results;
        }

        public void ActivateBackground(long backgroundId, long userId)
        {
            var changeResturentBackground = _restaurantService.GetRestaurantByAdminId(userId);
            changeResturentBackground.BackgroundId = backgroundId;
            _restaurantService.Update(changeResturentBackground);
            //var background = _backgroundService.Find(backgroundId);
            //if (background == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            //background.IsActive = true;
            //_backgroundService.Update(background);
            SaveChanges();
        }

        public void DeActivateBackground(long backgroundId)
        {
            var background = _backgroundService.Find(backgroundId);
            if (background == null) throw new NotFoundException(ErrorCodes.BackgroundNotFound);
            background.IsActive = false;
            _backgroundService.Update(background);
            SaveChanges();
        }
        public void DeleteBackground(long backgroundId)
        {
            var background = _backgroundService.Find(backgroundId);
            if (background == null) throw new NotFoundException(ErrorCodes.BackgroundNotFound);
            background.IsDeleted = true;
            background.IsActive = false;
            _backgroundService.Update(background);

            SaveChanges();
        }

        public void UpdateBackground(BackgroundDto backgroundDto, string path)
        {
            var background = _backgroundService.Find(backgroundDto.BackgroundId);
            if (background == null) throw new NotFoundException(ErrorCodes.BackgroundNotFound);

            _backgroundService.Update(background);
            SaveChanges();
            if (backgroundDto.IsImageChange)
                _manageStorage.UploadImage(path + "\\" + "Background", backgroundDto.Image, background.BackgroundId.ToString());
        }
    }
}
