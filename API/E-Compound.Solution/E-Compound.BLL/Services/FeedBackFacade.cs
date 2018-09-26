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
    public class FeedBackFacade : BaseFacade, IFeedBackFacade
    {
        private IFeedBackService _feedBackService;
        private IRoomService _roomService;
        private IRestaurantService _restaurantService;

        public FeedBackFacade(IFeedBackService feedBackService, IUnitOfWorkAsync unitOfWork, IRoomService roomService, IRestaurantService restaurantService) : base(unitOfWork)
        {
            _feedBackService = feedBackService;
            _roomService = roomService;
            _restaurantService = restaurantService;
        }

        public void AddFeedBack(FeedBackDto feedBackDto, long userId)
        {
            //var waiter = _restaurantWaiterService.Find(userId);
            //if (waiter == null) throw new NotFoundException(ErrorCodes.RestaurantAdminNotFound);
            var feedback = Mapper.Map<FeedBack>(feedBackDto);
            //feedback.RestaurantId = waiter.RestaurantId;
            // feedback.CreateTime = DateTime.Now;
            _feedBackService.Insert(feedback);
            SaveChanges();
        }

        public PagedResultsDto GetAllFeedBack(long userId, long restaurantId, int page, int pageSize, string userRole)
        {
            PagedResultsDto results = new PagedResultsDto();
            if (userRole == Enums.RoleType.Room.ToString())
            {
                var restaurant = _restaurantService.Find(restaurantId);
                if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
                results = _feedBackService.GetAllFeedBack(restaurantId, page, pageSize);
            }
            else if (userRole == Enums.RoleType.RestaurantAdmin.ToString())
            {
                var restaurant = _restaurantService.GetRestaurantByAdminId(userId);
                if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
                results = _feedBackService.GetAllFeedBack(restaurant.RestaurantId, page, pageSize);
            }
            return results;

        }
    }
}
