using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IRequestFacade
    {
        void CreateRequest(RequestDto requestDto);
        void ApproveRequest(long requestId,long userId, List<RequestDetailDto> requestDetailDtos);
        void RejectRequest(long requestId, long userId);
        PagedResultsDto GetAllRequests(long userId, int page, int pageSize, string role, long roomId, long featureId, string from, string to);
        List<RequestDetailDto> GetAllRequestDetailByFeatureId(long featureId);
        RequestStatusDto GetLastRequestByFeaturedId(long featureId, long roomId);
    }
}
