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
    public class PackageService : Service<Package>, IPackageService
    {
        public PackageService(IRepositoryAsync<Package> repository):base(repository)
        {
            
        }
        public int GetRoomsCountByAdminId(long AdminId)
        {
            return _repository.Query(x => x.AdminId == AdminId).Select(x => x.MaxNumberOfRooms).Sum();
        }

        //public List<Package> GetAllPackagesByAdminId(long AdminId)
        //{
        //    return _repository.Query(x => x.AdminId == AdminId).Include(x => x.Rooms).Select().ToList();
        //}
    }

}
