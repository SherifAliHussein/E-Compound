using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IRoomService : IService<Room>
    {
        List<Room> GetAllRooms(long adminId, int page, int pageSize);
        bool CheckUserNameDuplicated(string userName, long userId, long adminId);
        int GetRoomCountByPackageId(long packageId);
        Room RelationValidation(long userId, long unitId);
    }
}
