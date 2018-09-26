using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IitemFacade
    {
        void AddItem(ItemDTO itemDto, string path);
        ItemDTO GetItem(long itemId, string language);
        PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize);
        void DeleteItem(long itemId);
        void UpdateItem(ItemDTO itemDto, string path);
        List<ItemNamesDto> GetAllItemNamesByCategoryId(string language, long categoryId);
        void TranslateItem(ItemDTO itemDto, string language);
        void ActivateItem(long itemId);
        void DeActivateItem(long itemId);
        PagedResultsDto GetActivatedItemsByCategoryId(string language, long categoryId, int page, int pageSize);
        CategoryPageTemplateDTO GetActivatedItemsWithTemplatesByCategoryId(string language, long categoryId);
        void UpdateItemOrder(List<ItemNamesDto> itemNamesDto);
        void LikeItem(long itemId);
        void DislikeItem(long itemId);
    }
}
