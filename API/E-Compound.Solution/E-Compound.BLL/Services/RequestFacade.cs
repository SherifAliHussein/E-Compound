using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.BLL.Services.ManageStorage;
using E_Compound.Common;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;
using E_Compound.Common.CustomException;

namespace E_Compound.BLL.Services
{
    public class RequestFacade : BaseFacade, IRequestFacade
    {
        private IRequestService _requestService;
        private ISupervisorCategoryService _supervisorCategoryService;
        private ITechnicianCategoryService _technicianCategoryService;
        private ISupervisorService _supervisorService;
        private IReceptionistService _receptionistService;
        private IRequestDetailService _requestDetailService;
        private IUserService _userService;
        private IFeatureDetailService _featureDetailService;
        private IRestaurantWaiterService _restaurantWaiterService;
        private readonly IManageStorage _manageStorage;
        public RequestFacade(IUnitOfWorkAsync unitOfWork, IRequestService requestService, IReceptionistService receptionistService, ISupervisorService supervisorService, IRequestDetailService requestDetailService, IUserService userService, IFeatureDetailService featureDetailService, IRestaurantWaiterService restaurantWaiterService, IManageStorage manageStorage, ISupervisorCategoryService supervisorCategoryService, ITechnicianCategoryService technicianCategoryService) : base(unitOfWork)
        {
            _requestService = requestService;
            _receptionistService = receptionistService;
            _supervisorService = supervisorService;
            _requestDetailService = requestDetailService;
            _userService = userService;
            _featureDetailService = featureDetailService;
            _restaurantWaiterService = restaurantWaiterService;
            _manageStorage = manageStorage;
            _supervisorCategoryService = supervisorCategoryService;
            _technicianCategoryService = technicianCategoryService;
        }

        public void CreateRequest(RequestDto requestDto)
        {
            Request request = Mapper.Map<Request>(requestDto);

            var user = _userService.Find(requestDto.RoomId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            request.CreateTime = DateTime.UtcNow;
            request.Status = Enums.RequestStatus.Pending;

            //if (requestDto.Type == Enums.FeatureType.Restaurant)
            //{
            //    foreach (var item in requestDto.RequestDetails)
            //    {
            //        request.RequestDetails.Add(new RequestDetail
            //        {
            //            ItemSizeId = item.ItemSizeId,
            //            Number = item.Number,
            //            //RequestId = requestId,
            //            Price = item.Price
            //        });
            //    }
            //}
            _requestDetailService.InsertRange(request.RequestDetails);
            _requestService.Insert(request);
            SaveChanges();
        }

        public void AssignRequest(RequestDto requestDto)
        {
            Request request = _requestService.Find(requestDto.RequestId);

            var user = _userService.Find(requestDto.RoomId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            request.CreateTime = DateTime.UtcNow;
            request.AssignedTime = DateTime.UtcNow; 
            request.Status = Enums.RequestStatus.Assigned;
            request.TechnicianId = requestDto.Technician.UserId;

            //   _requestDetailService.InsertRange(request.RequestDetails);
            _requestService.Update(request);
            SaveChanges();
        }

        public void CreateTicket(RequestDto requestDto, long userId, List<MemoryStream> files, string path)
        {
            var ticket = Mapper.Map<Request>(requestDto);
            ticket.CreationBy = userId;
            ticket.CreateTime = DateTime.Now;
            ticket.CreateTime = DateTime.UtcNow;
            ticket.Status = Enums.RequestStatus.Pending;
            _requestService.Insert(ticket);
            SaveChanges();
            var imageId = 1;
            foreach (var memoryStream in files)
            {
                _manageStorage.UploadImage(path + "\\" + "Ticket-" + ticket.RequestId, memoryStream, imageId.ToString());
                imageId++;
            }
        }

        public PagedResultsDto GetAllRequests(long userId, int page, int pageSize, string role, long roomId, long featureId, string from, string to)
        {
            var user = _userService.Find(userId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            int requestsCount = 0;
            List<RequestDto> requests = null;
            if (role == Enums.RoleType.Admin.ToString())
            {
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                requestsCount = _requestService.Query(x => x.Creater.AdminId == userId &&
                                                           (roomId <= 0 || x.CreationBy == roomId)
                                                           && (featureId <= 0 || x.FeatureId == featureId)
                                                           && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select().Count();
                requests = Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByAdmin(userId, page, pageSize, roomId, featureId, fromDateTime, toDateTime));
            }
            else if (role == Enums.RoleType.Room.ToString())
            {
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                requestsCount = _requestService.Query(x => x.CreationBy == userId
                                                           && (featureId <= 0 || x.FeatureId == featureId)
                                                           && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select().Count();
                requests = Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByRoom(userId, page, pageSize, featureId, fromDateTime, toDateTime));
            }
            else if (role == Enums.RoleType.Supervisor.ToString())
            {
                //var supervisor = _supervisorService.Find(userId);
                //var featureIds = supervisor.SupervisorFeatures.Select(x => x.FeatureId);

                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                requestsCount = _requestService
                    .Query(x => x.Feature.SupervisorFeatures.Select(s => s.SupervisorId).Contains(userId) &&
                                (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
                                && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime)
                    .Select().Count();
                var allRequests = Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsBySupervisor(userId, page, pageSize, roomId, featureId, fromDateTime, toDateTime));
                var supervisorCategory = _supervisorCategoryService.Query(x => x.SupervisorId == userId).Select();
                requests = new List<RequestDto>();

                foreach (var category in supervisorCategory)
                {
                    foreach (var allRequest in allRequests)
                    {
                        if (allRequest.Type != Enums.FeatureType.Ticket)
                        {
                            var checkifExist = requests.Any(x => x.RequestId == allRequest.RequestId);
                            if (!checkifExist)
                            {
                                requests.Add(allRequest);
                                continue;
                            }
                        }
                        if (allRequest.UserCategory != Convert.ToInt32(category.UserCategoryId)) continue;
                        {
                            if (requests.Count == 0) requests.Add(allRequest);

                            if (requests.Any(x => x.RequestId != allRequest.RequestId))  requests.Add(allRequest);
                        }
                    }

                }
            }
            else if (role == Enums.RoleType.Technician.ToString())
            { 
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                var allRequests = Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByTechnican(userId, page, pageSize, roomId, featureId, fromDateTime, toDateTime));
                var supervisorCategory = _technicianCategoryService.Query(x => x.TechnicianId == userId).Select();
                requests = new List<RequestDto>();
                requestsCount = requests.Count;

                foreach (var category in supervisorCategory)
                {
                    foreach (var allRequest in allRequests)
                    {
                        if (allRequest.Type != Enums.FeatureType.Ticket)
                        {
                            var checkifExist = requests.Any(x => x.RequestId == allRequest.RequestId);
                            if (!checkifExist)
                            {
                                requests.Add(allRequest);
                                continue;
                            }
                        }
                        if (allRequest.UserCategory != Convert.ToInt32(category.UserCategoryId)) continue;
                        {
                            if (requests.Count == 0) requests.Add(allRequest);

                            if (requests.Any(x => x.RequestId != allRequest.RequestId)) requests.Add(allRequest);
                        }
                    }

                }
            }
            else if (role == Enums.RoleType.Receptionist.ToString())
            {
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                requestsCount = _requestService
                    .Query(x => x.Feature.Creater.Receptionists.Select(r => r.UserId).Contains(userId) &&
                                (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
                                && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime)
                    .Select().Count();
                requests = Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByReceptionist(userId, page, pageSize, roomId, featureId, fromDateTime, toDateTime));
            }
            else if (role == Enums.RoleType.Waiter.ToString())
            {
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                var waiter = _restaurantWaiterService.Find(userId);
                requestsCount = _requestService.Query(x => x.RestaurantId == waiter.RestaurantId).Select().Count();
                requests = Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByWaiter(waiter.RestaurantId, page, pageSize, roomId, fromDateTime, toDateTime));
            }

            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = requestsCount,
                Data = requests
            };
            return results;
        }

        public List<RequestDetailDto> GetAllRequestDetailByFeatureId(long featureId)
        {
            var lk = _requestService.Query(x => x.FeatureId == featureId).Select().ToList();
            var lk1 = _requestService.Query(x => x.FeatureId == featureId && x.Status != Enums.RequestStatus.Rejected).Select().ToList();
            var requestDetails = Mapper.Map<List<RequestDetailDto>>(_requestDetailService
                .Query(x => x.Request.FeatureId == featureId && x.Request.Status != Enums.RequestStatus.Rejected &&
                            (x.FeatureDetail.FeatureControl.Control == Enums.Control.Available || x.FeatureDetail.FeatureControl.Control == Enums.Control.ListOfAvailable)).Select().ToList());
            return requestDetails;
        }

        public void ApproveRequest(long requestId, long userId, List<RequestDetailDto> requestDetailDtos)
        {
            Request request = _requestService.Find(requestId);
            var user = _userService.Find(userId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            if (request.Status == Enums.RequestStatus.Pending)
            { 
                request.ModifyTime = DateTime.UtcNow;
                request.ModifiedBy = userId;
                request.Status = Enums.RequestStatus.Approved;
                //if (requestDetailDtos != null && request.Feature.Type == Enums.FeatureType.Normal)
                //{
                //    foreach (var requestDetail in requestDetailDtos)
                //    {
                //        var price = _featureDetailService.Find(requestDetail.FeatureDetailId).Price;
                //        request.RequestDetails.Add(new RequestDetail
                //        {
                //            FeatureDetailId = requestDetail.FeatureDetailId,
                //            Number = requestDetail.Number,
                //            RequestId = requestId,
                //            Price = price
                //        });
                //    }
                //    _requestDetailService.InsertRange(request.RequestDetails);
                //}
          
            }
            else if(request.Status == Enums.RequestStatus.Assigned)
            {
                request.Status = Enums.RequestStatus.Approved;
            }
            else
            {
                throw new ValidationException(ErrorCodes.RequestStatusChanged);
            }
            _requestService.Update(request);
            SaveChanges();
        }

        public void RejectRequest(long requestId, long userId, RequestDto requestDto)
        {
            Request request = _requestService.Find(requestId);
            var user = _userService.Find(userId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            if (request.Status == Enums.RequestStatus.Pending || request.Status == Enums.RequestStatus.Assigned)
            {
                request.ModifyTime = DateTime.UtcNow;
                request.ModifiedBy = userId;
                request.TechnicianComment = requestDto.TechnicianComment;
                request.Status = Enums.RequestStatus.Rejected;
                _requestService.Update(request);
                SaveChanges();
            }
            else
                throw new ValidationException(ErrorCodes.RequestStatusChanged);
        }

        public RequestStatusDto GetLastRequestByFeaturedId(long featureId, long roomId)
        {
            return Mapper.Map<RequestStatusDto>(_requestService.Query(x => x.FeatureId == featureId && x.CreationBy == roomId).Select().OrderByDescending(x => x.CreateTime).FirstOrDefault());
        }
    }
}
