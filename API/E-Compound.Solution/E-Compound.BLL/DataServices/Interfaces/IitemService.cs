using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Service.Pattern;

namespace E_Compound.BLL.DataServices.Interfaces
{
    public interface IitemService:IService<Item>
    {
        PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize);
    }
}
