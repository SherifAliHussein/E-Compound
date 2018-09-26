using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class FloorService:Service<Floor>,IFloorService
    {
        public FloorService(IRepositoryAsync<Floor> repository):base(repository)
        {
            _repository = repository;
        }
        public bool CheckFloorRepeated(FloorDto floorDto, long adminId)
        {
            return _repository.Query(x => x.AdminId == adminId && x.FloorName == floorDto.FloorName && x.FloorId != floorDto.FloorId && !x.IsDeleted).Select().Any();
        }
    }
}
