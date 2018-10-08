using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.Configuration;
using E_Compound.API.Models;
using E_Compound.BLL;
using E_Compound.BLL.DTOs;
using E_Compound.Common;

namespace E_Compound.API.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {

            var mapperConfiguration = new MapperConfigurationExpression();

            mapperConfiguration.CreateMap<FeatureDto, FeatureModel>()
                .ForMember(dto => dto.Type, m => m.MapFrom(src => src.Type.ToString()));
            mapperConfiguration.CreateMap<FeatureModel, FeatureDto>()
                .ForMember(dto => dto.Type, m => m.MapFrom(src => Enum.Parse(typeof(Enums.FeatureType), src.Type)));
       

            mapperConfiguration.CreateMap<ReceptionistModel, ReceptionistDto>();
            mapperConfiguration.CreateMap<ReceptionistDto, ReceptionistModel>();

            mapperConfiguration.CreateMap<SupervisorModel, SupervisorDto>();
            mapperConfiguration.CreateMap<SupervisorDto, SupervisorModel>();

            mapperConfiguration.CreateMap<TechnicianModel, TechnicianDto>();
            mapperConfiguration.CreateMap<TechnicianDto, TechnicianModel>();

            mapperConfiguration.CreateMap<FeatureDetailModel, FeatureDetailDto>();
            mapperConfiguration.CreateMap<FeatureDetailDto, FeatureDetailModel>();

            mapperConfiguration.CreateMap<UnitTypeDto, UnitTypeModel>().ReverseMap();
            mapperConfiguration.CreateMap<UnitDto, UnitModel>().ReverseMap();

            mapperConfiguration.CreateMap<UserCategoryDto, UserCategoryModel>().ReverseMap();

            mapperConfiguration.CreateMap<FeatureInfoDto, FeatureInfoModel>();
            

            mapperConfiguration.CreateMap<RequestModel, RequestDto>()
                .ForMember(dto => dto.InvetationType, m => m.MapFrom(src => Enum.Parse(typeof(Enums.InvetationType),src.InvetationType)))
                .ForMember(dto => dto.Status, m => m.MapFrom(src => Enum.Parse(typeof(Enums.RoleType),src.Status)))
                .ForMember(dto => dto.Type, m => m.MapFrom(src => Enum.Parse(typeof(Enums.RoleType), src.Type)));
            mapperConfiguration.CreateMap<RequestDto, RequestModel>()
                .ForMember(dto => dto.InvetationType, m => m.MapFrom(src => src.InvetationType.ToString()))
                .ForMember(dto => dto.Status, m => m.MapFrom(src => src.Status.ToString()))
                .ForMember(dto => dto.Type, m => m.MapFrom(src => src.Type.ToString()));



            mapperConfiguration.CreateMap<RestaurantTypeModel, RestaurantTypeDto>();
            mapperConfiguration.CreateMap<RestaurantTypeDto, RestaurantTypeModel>();
            mapperConfiguration.CreateMap<RestaurantModel, RestaurantDTO>();
            mapperConfiguration.CreateMap<RestaurantDTO, RestaurantModel>();

            mapperConfiguration.CreateMap<MenuModel, MenuDTO>();
            mapperConfiguration.CreateMap<MenuDTO, MenuModel>();

            mapperConfiguration.CreateMap<RestaurantNameDto,RestaurantNameModel>();

            mapperConfiguration.CreateMap<CategoryModel, CategoryDTO>();
            mapperConfiguration.CreateMap<CategoryDTO, CategoryModel>();

            mapperConfiguration.CreateMap<SizeModel, SizeDto>();
            mapperConfiguration.CreateMap<SizeDto, SizeModel>();

            mapperConfiguration.CreateMap<SideItemModel, SideItemDTO>();
            mapperConfiguration.CreateMap<SideItemDTO, SideItemModel>();


            mapperConfiguration.CreateMap<ItemModel, ItemDTO>();
            mapperConfiguration.CreateMap<ItemDTO, ItemModel>();

            mapperConfiguration.CreateMap<ItemSizeDto, ItemSizeModel>();
            mapperConfiguration.CreateMap<ItemSizeModel, ItemSizeDto>();


            mapperConfiguration.CreateMap<ItemNamesDto, ItemNameModel>();

            mapperConfiguration.CreateMap<RestaurantWaiterModel, RestaurantWaiterDTO>();
            mapperConfiguration.CreateMap<RestaurantWaiterDTO, RestaurantWaiterModel>();


            mapperConfiguration.CreateMap<BackgroundModel, BackgroundDto>();
            mapperConfiguration.CreateMap<BackgroundDto, BackgroundModel>();

            mapperConfiguration.CreateMap<ResturantInfoModel, ResturantInfoDto>();
            mapperConfiguration.CreateMap<ResturantInfoDto, ResturantInfoModel>();


            mapperConfiguration.CreateMap<TemplateDTO, TemplateModel>();

            mapperConfiguration.CreateMap<CategoryNamesDTO, CategoryNameModel>();

            mapperConfiguration.CreateMap<PageModel, PageDTO>();

            mapperConfiguration.CreateMap<PageTemplateDTO, PageTemplateModel>()
                .ForMember(dest => dest.ItemModels, m => m.MapFrom(src => src.ItemDto));

            mapperConfiguration.CreateMap<CategoryPageTemplateDTO, CategoryPageTemplateModel>();


            mapperConfiguration.CreateMap<BranchModel, BranchDto>();
            mapperConfiguration.CreateMap<BranchDto, BranchModel>();

            mapperConfiguration.CreateMap<FeedBackModel, FeedBackDto>();
            mapperConfiguration.CreateMap<FeedBackDto, FeedBackModel>();

            mapperConfiguration.CreateMap<FeaturesBackgroundModel, FeaturesBackgroundDto>();
            mapperConfiguration.CreateMap<FeaturesBackgroundDto, FeaturesBackgroundModel>();


            mapperConfiguration.CreateMap<ControlModel, ControlDto>();
            mapperConfiguration.CreateMap<ControlDto, ControlModel>();

            mapperConfiguration.CreateMap<FeatureControlModel, FeatureControlDto>()
                .ForMember(dto => dto.ControlType,m => m.MapFrom(src => Enum.Parse(typeof(Enums.ControlType), src.ControlType)));
            mapperConfiguration.CreateMap<FeatureControlDto, FeatureControlModel>()
                .ForMember(dto => dto.ControlType, m => m.MapFrom(src => src.ControlType.ToString()));

            mapperConfiguration.CreateMap<RequestStatusDto, RequestStatusModel>()
               .ForMember(dto => dto.Status, m => m.MapFrom(src => src.Status.ToString()));

            ECompoundBLLConfig.RegisterMappings(mapperConfiguration);

            //Mapper.Initialize(m =>
            //{
            //    m.CreateMap<RestaurantTypeModel, RestaurantTypeDto>();
            //    m.CreateProfile("ff",expression => {});
            //    //m.AddProfile(ECatalogBLLConfig);
            //});

        }

    }
}