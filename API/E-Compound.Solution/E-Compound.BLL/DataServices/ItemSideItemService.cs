
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
    public class ItemSideItemService:Service<ItemSideItem>,IItemSideItemService
    {
        public ItemSideItemService(IRepositoryAsync<ItemSideItem> repository):base(repository)
        {
            
        }
    }
}
