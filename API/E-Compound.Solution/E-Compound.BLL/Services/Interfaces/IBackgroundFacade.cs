using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IBackgroundFacade
    {
        void AddBackground(BackgroundDto backgroundDto, string path);
        BackgroundDto GetBackground(long backgroundId);
        PagedResultsDto GetAllBackgrounds(  int page, int pageSize, long userId);
        void ActivateBackground(long backgroundId, long userId);
        void DeActivateBackground(long backgroundId);
        void DeleteBackground(long backgroundId);
        void UpdateBackground(BackgroundDto backgroundDto, string path);
        PagedResultsDto GetActivatedBackgroundByUserId(long userId, int page, int pageSize);
    }
}
