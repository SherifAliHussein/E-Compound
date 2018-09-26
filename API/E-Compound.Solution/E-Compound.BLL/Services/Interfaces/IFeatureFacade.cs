using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.BLL.DTOs;

namespace E_Compound.BLL.Services.Interfaces
{
    public interface IFeatureFacade
    {
        void AddFeature(FeatureDto featureDto, long adminId, string path);
        void ActivateFeature(long featureId,long adminId);
        void DeActivateFeature(long featureId, long adminId);
        void DeleteFeature(long featureId, long adminId);
        PagedResultsDto GetAllFeatures(long adminId, int page, int pageSize);
        void UpdateFeature(FeatureDto featureDto, long adminId, string path);
        FeatureDto GetFeature(long featureId);
        PagedResultsDto GetAllActiveFeatures(long adminId, int page, int pageSize, string role);
        FeatureDto CheckFeatureAsRestaurant(long adminId);
       // List<ControlDto> GetAllControl();
        List<FeatureDto> GetAllFeatureForSupervisor(long userId);

        PagedResultsDto GeFeatureDetails(long featureControlId,int page,int pageSize);
        void AddFeatureDetail(FeatureDetailDto featureDetailDto, long adminId, string path);
        void UpdateFeatureDetail(FeatureDetailDto featureDetailDto, long adminId, string path);
        void DeleteFeatureDetail(long featureDetailId, long userId);
        void SwitchFeatureControlType(long featureControlId);
        FeatureInfoDto GetAllFeatureControlDetailDtos(long featureId);
        void AddFeatureDetailAvailability(FeatureDetailDto featureDetailDto, long adminId);
        void UpdateFeatureDetailAvailability(FeatureDetailDto featureDetailDto, long adminId);
        void DeleteAvailable(long avialableId, long userId);
        List<FeatureNameDto> GetAllFeatureName(long userId,string userRole);
    }
}
