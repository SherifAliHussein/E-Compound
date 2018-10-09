using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IRequestService:IService<Request>
    {
        List<Request> GetAllRequestsByAdmin(long adminId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime);
        List<Request> GetAllRequestsBySupervisor(long userId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime);
        List<Request> GetAllRequestsByTechnican(long userId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime);
        List<Request> GetAllRequestsByReceptionist(long userId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime);
        List<Request> GetAllRequestsByWaiter(long restaurantId, int page, int pageSize, long roomId, DateTime fromDateTime, DateTime toDateTime);
        List<Request> GetAllRequestsByRoom(long roomId, int page, int pageSize, long featureId, DateTime fromDateTime, DateTime toDateTime);
    }
}
