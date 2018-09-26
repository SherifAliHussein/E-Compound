using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.Common;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class RequestService:Service<Request>,IRequestService
    {
        public RequestService(IRepositoryAsync<Request> repository):base(repository)
        {
            _repository = repository;
        }

        public List<Request> GetAllRequestsByAdmin(long adminId, int page, int pageSize,long roomId,long featureId, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            if (pageSize > 0)
                requests = _repository.Query(x => x.Creater.AdminId == adminId &&(roomId <= 0 || x.CreationBy==roomId) && (featureId <= 0 || x.FeatureId == featureId)
                && x.CreateTime>=fromDateTime && x.CreateTime<=toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                requests = _repository.Query(x => x.Creater.AdminId == adminId && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
                                                  && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).ToList();

            return requests;
        }

        public List<Request> GetAllRequestsBySupervisor(long userId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            if (pageSize > 0)
                requests = _repository.Query(x => x.Feature.SupervisorFeatures.Select(s => s.SupervisorId).Contains(userId) && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
                                                  && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x=>x.Status).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                requests = _repository.Query(x => x.Feature.SupervisorFeatures.Select(s => s.SupervisorId).Contains(userId) && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
                                                  && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).ToList();

            return requests;
        }
        public List<Request> GetAllRequestsByReceptionist(long userId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            if (pageSize > 0)
                requests = _repository.Query(x => x.Feature.Creater.Receptionists.Select(r => r.UserId).Contains(userId) && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
                                                  && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                requests = _repository.Query(x => x.Feature.Creater.Receptionists.Select(r => r.UserId).Contains(userId) && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
                                                  && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).ToList();

            return requests;
        }

        public List<Request> GetAllRequestsByWaiter(long restaurantId, int page, int pageSize, long roomId, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            if (pageSize > 0)
                requests = _repository.Query(x => x.RestaurantId.Value == restaurantId && (roomId <= 0 || x.CreationBy == roomId)&& x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                requests = _repository.Query(x => x.RestaurantId.Value == restaurantId && (roomId <= 0 || x.CreationBy == roomId) && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).ToList();

            return requests;
        }
    }
}
