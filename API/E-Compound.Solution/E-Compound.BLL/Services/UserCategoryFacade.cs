using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class UserCategoryFacade : BaseFacade, IUserCategoryFacade
    {
        private IUserCategoryService _userCategoryService;
        //private IUnitService _unitService;
        private IUserCategoryTranslationService _userCategoryTranslationService;
        private readonly IRoomService _roomService;
        private readonly ISupervisorService _supervisorService;

        public UserCategoryFacade(IUserCategoryService userCategoryService, IUserCategoryTranslationService userCategoryTranslationService, IUnitOfWorkAsync unitOfWork, IRoomFacade roomFacade, IRoomService roomService, IUserService userService, ISupervisorService supervisorService) : base(unitOfWork)
        {
            _userCategoryService = userCategoryService;
            _userCategoryTranslationService = userCategoryTranslationService;
            _roomService = roomService;
            _supervisorService = supervisorService; 
        }

        public PagedResultsDto GetAllPagingUserCategories(long userId, int page, int pageSize)
        {
            var userCategories = _userCategoryService.GetAllPagingUserCategories(userId, page, pageSize);

            return userCategories;
        }

        public List<UserCategoryDto> GetUserCategories(long userId, string roleType)
        {
            if (roleType == Enums.RoleType.Room.ToString())
                userId = _roomService.Find(userId).AdminId;

            if (roleType == Enums.RoleType.Supervisor.ToString()) 
                userId = _supervisorService.Find(userId).AdminId;

            if (roleType == Enums.RoleType.Technician.ToString())
                userId = _supervisorService.Find(userId).AdminId;

            var userCategorys = _userCategoryService.GetUserCategories(userId);

            return Mapper.Map<List<UserCategoryDto>>(userCategorys);
        }

        public void DeleteUserCategory(long userId, long userCategoryId)
        {
            var userCategory = _userCategoryService.Find(userCategoryId);
            if (userCategory == null) throw new NotFoundException(ErrorCodes.UserCategoryNotFound);

            userCategory.IsDeleted = true;
            _userCategoryService.Update(userCategory);
            SaveChanges();
        }

        public void AddUserCategory(UserCategoryDto userCategoryDto, long userId)
        {
            var userCategory = Mapper.Map<UserCategory>(userCategoryDto);

            userCategory.CreateTime = DateTime.Now;
            userCategory.CreationBy = userId;
            userCategory.IsDeleted = false;
            userCategory.IsActive = true;

            _userCategoryTranslationService.InsertRange(userCategory.UserCategoryTranslations);
            _userCategoryService.Insert(userCategory);
            SaveChanges();
        }

        public void UpdateUserCategory(long userId, UserCategoryDto userCategoryDto)
        {
            var userCategory = _userCategoryService.Find(userCategoryDto.UserCategoryId);
            if (userCategory == null) throw new NotFoundException(ErrorCodes.UserCategoryNotFound);

            userCategory.ModifyTime = DateTime.Now;
            userCategory.ModifiedBy = userId;

            _userCategoryService.Update(userCategory);

            var userCategoryTrans = _userCategoryTranslationService.GetTransByUserCategoryId(userCategory.UserCategoryId);

            for (int i = 0; i < 2; i++)
            {
                if (userCategoryTrans[i].Language == "en-us")
                {
                    userCategoryTrans[i].Name = userCategoryDto.TitleDictionary["en-us"];
                    SaveChanges();
                }
                else if (userCategoryTrans[i].Language == "ar-eg")
                {
                    userCategoryTrans[i].Name = userCategoryDto.TitleDictionary["ar-eg"];
                    SaveChanges();
                }
            }
            SaveChanges();
        }

    }
}