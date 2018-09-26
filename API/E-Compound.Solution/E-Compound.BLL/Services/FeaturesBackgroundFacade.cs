using System;
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
    public class FeaturesBackgroundFacade : BaseFacade, IFeaturesBackgroundFacade
    {
        private readonly IFeaturesBackgroundService _backgroundService;
        private readonly IManageStorage _manageStorage;
        private IAdminService _adminService;
        private IRoomService _roomService;
        public FeaturesBackgroundFacade(IFeaturesBackgroundService backgroundService, IManageStorage manageStorage , IUnitOfWorkAsync unitOfWork, IAdminService adminService, IRoomService roomService) : base(unitOfWork)
        {
            _backgroundService = backgroundService;
            _manageStorage = manageStorage;
            _adminService = adminService;
            _roomService = roomService;
        }
        
        public void AddBackground(FeaturesBackgroundDto backgroundDto, string path)
        {
            var background = Mapper.Map<FeaturesBackground>(backgroundDto);
            
            _backgroundService.Insert(background);
            SaveChanges();
            var admin = _adminService.Find(backgroundDto.UserId);
            admin.FeaturesBackgroundId = background.FeaturesBackgroundId;
           _adminService.Update(admin);
            SaveChanges();

            _manageStorage.UploadImage(path + "\\" + "Features Background", backgroundDto.Image, background.FeaturesBackgroundId.ToString());
        }

        public PagedResultsDto GetAllBackgrounds(int page, int pageSize, long userId)
        {
            var backgroundObj = _backgroundService.GetAllBackgrounds(page, pageSize, userId);
            if (backgroundObj == null) throw new NotFoundException(ErrorCodes.BackgroundNotFound);

            var featureBackground = _adminService.Find(userId).FeaturesBackgroundId;
            if (featureBackground > 0)
            {
                foreach (var background in (IEnumerable<FeaturesBackgroundDto>)backgroundObj.Data)
                {
                    var checkResturentBackground = featureBackground == background.FeaturesBackgroundId;
                    if (checkResturentBackground)
                        background.IsSelected = true;
                    else
                        background.IsSelected = false;

                }
            }
            else
            {
                var allbackground = (IEnumerable<FeaturesBackgroundDto>) backgroundObj.Data;
                allbackground.FirstOrDefault(x => x.FeaturesBackgroundId == Strings.FeaturesBackgroundId).IsSelected =
                    true;
            }
            var results = backgroundObj;
            return results;
        }

        public FeaturesBackgroundDto GetActivateFeaturesBackground(long roomId)
        {
            var room = _roomService.Find(roomId);
            var featureBackgroundId = _adminService.Find(room.AdminId).FeaturesBackgroundId;
            FeaturesBackgroundDto backgroundDto = new FeaturesBackgroundDto();
            backgroundDto = Mapper.Map<FeaturesBackgroundDto>(featureBackgroundId > 0 ? _backgroundService.Find(featureBackgroundId) : _backgroundService.Find(Strings.FeaturesBackgroundId));
            return backgroundDto;
        }

        public void ActivateBackground(long backgroundId, long userId)
        {
            var admin = _adminService.Find(userId);
            admin.FeaturesBackgroundId = backgroundId;
            _adminService.Update(admin);
            SaveChanges();
        }
        
        
    }
}
