using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class RoomService:Service<Room>,IRoomService
    {
        public RoomService(IRepositoryAsync<Room> repository):base(repository)
        {
            _repository = repository;
        }
        public List<Room> GetAllRooms(long adminId, int page, int pageSize)
        {
            return _repository.Query(x => !x.IsDeleted && x.AdminId == adminId).Select()
                .OrderBy(x => x.UserId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public bool CheckUserNameDuplicated(string userName, long userId, long adminId)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && u.UserId != userId && u.AdminId == adminId);
        }

        /*public int GetRoomCountByPackageId(long packageId)
        {
            return _repository.Query(x => !x.IsDeleted && x.PackageId == packageId).Select().Count();
        }*/

        public Room RelationValidation(long userId, long unitId)
        {
            var room = _repository.Query(x => x.UnitId == unitId).Select()
                .FirstOrDefault();
            return room;
        }
    }
}
