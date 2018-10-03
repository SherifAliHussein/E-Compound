using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class InvetationFacade : BaseFacade, IInvetationFacade
    {
        private readonly IUnitTypeService _unitTypeService;
        private readonly IRoomService _roomService;
        private readonly IRequestService _requestService;
        private readonly IRequestDetailService _requestDetailService;
        public InvetationFacade(IUnitOfWorkAsync unitOfWork, IRequestService requestService, IRequestDetailService requestDetailService, IUnitTypeService unitTypeService, IRoomService roomService) : base(unitOfWork)
        {
            _requestService = requestService;
            _requestDetailService = requestDetailService;
            _unitTypeService = unitTypeService;
            _roomService = roomService;
        }

        public bool CheckUnitInvetationLimit(long featureId, long userId, DateTime dateTime,int limit)
        {
            bool returnStatus = false;
            int unitLimit = 0;
            var userUnit = _roomService.Query(x => x.UserId == userId).Select().FirstOrDefault();
            var check = userUnit.Requests.Where(x => x.CreateTime == dateTime && x.FeatureId == featureId &&
                                                     x.Status == Enums.RequestStatus.Pending &&
                                                     x.Status == Enums.RequestStatus.Approved).Select(s => s.Count);
            unitLimit = userUnit.Unit.UnitType.Limit;
            if (unitLimit >= (check.Sum() + limit))
            {
                returnStatus = true;
            }
            return returnStatus;
        }

    }
}
