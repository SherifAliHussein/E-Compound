using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using E_Compound.BLL.DataServices;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.DAL;
using E_Compound.DAL.Entities.Model;
using Unity;
using Unity.Lifetime;
using E_Compound.Common;
using E_Compound.BLL.Services.ManageStorage;

namespace E_Compound.BLL
{
    public static class ECompoundBLLConfig
    {
        public static void RegisterMappings(MapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.CreateMap<User, UserDto>();
            mapperConfiguration.CreateMap<ReceptionistDto, Receptionist>();

            mapperConfiguration.CreateMap<Receptionist, ReceptionistDto>()
                .ForMember(dto => dto.Password, m => m.MapFrom(src => PasswordHelper.Decrypt(src.Password)));
            mapperConfiguration.CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();

            mapperConfiguration.CreateMap<FeatureDetailDto, FeatureDetail>()
                .ForMember(dto => dto.FeatureDetailTranslations, m => m.MapFrom(src => src.DescriptionDictionary.Select(x => new FeatureDetailTranslation {Description = x.Value, Language = x.Key}).ToList()));
            mapperConfiguration.CreateMap<FeatureDetail, FeatureDetailDto>()
                .ForMember(dto => dto.FeatureId, m => m.MapFrom(src => src.FeatureControl.FeatureId))
                .ForMember(dto => dto.DescriptionDictionary, m => m.MapFrom(src => src.FeatureDetailTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Description)))
                .ForMember(dto => dto.Availables, m => m.MapFrom(src => src.Availables.Where(x=>!x.IsDeleted).ToList()));

            mapperConfiguration.CreateMap<FeatureDto, Feature>()
                .ForMember(dto => dto.FeatureTranslations, m => m.MapFrom(src => src.FeatureNameDictionary.Select(x=>new FeatureTranslation {FeatureName = x.Value,Language = x.Key}).ToList()))
               // .ForMember(dto => dto.FeatureDetails, m => m.MapFrom(src => src.FeatureDetails))
                .ForMember(dto => dto.Restaurants, m => m.Ignore());
            mapperConfiguration.CreateMap<Feature, FeatureDto>()
                .ForMember(dto => dto.FeatureNameDictionary, m => m.MapFrom(src => src.FeatureTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.FeatureName)))
                //.ForMember(dto => dto.FeatureDetails, m => m.MapFrom(src => src.FeatureDetails.Where(x=>!x.IsDeleted)))
                .ForMember(dto => dto.FeatureControl, m => m.MapFrom(src => src.FeatureControls.Where(x=>x.IsActive).OrderBy(x=>x.Order)))
                .ForMember(dto => dto.Restaurants, m => m.MapFrom(src => src.Restaurants.Where(x => !x.IsDeleted)));

            mapperConfiguration.CreateMap<FeatureControl, FeatureControlDetailDto>()
                .ForMember(dto => dto.FeatureDetails,m => m.MapFrom(src => src.FeatureDetails.Where(x => !x.IsDeleted).ToList()));


            mapperConfiguration.CreateMap<Feature, FeatureInfoDto>()
                .ForMember(dto => dto.FeatureNameDictionary, m => m.MapFrom(src => src.FeatureTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.FeatureName)))
                .ForMember(dto => dto.FeatureControl, m => m.MapFrom(src => src.FeatureControls.Where(x => x.IsActive).OrderBy(x => x.Order)));

            mapperConfiguration.CreateMap<Supervisor, SupervisorDto>()
                .ForMember(dto => dto.Password, m => m.MapFrom(src => PasswordHelper.Decrypt(src.Password)))
                .ForMember(dto => dto.Features, m => m.MapFrom(src => src.SupervisorFeatures.Where(x=>!x.Feature.IsDeleted && x.Feature.IsActive).Select(x=>x.Feature).ToList())); 
            mapperConfiguration.CreateMap<SupervisorDto, Supervisor>();


            mapperConfiguration.CreateMap<RoomDto, Room>()
                .ForMember(dto => dto.UserName, m => m.MapFrom(src => src.RoomName))
                .ForMember(dto => dto.UserId, m => m.MapFrom(src => src.RoomId));
            mapperConfiguration.CreateMap<Room, RoomDto>()
                .ForMember(dto => dto.Password, m => m.MapFrom(src => PasswordHelper.Decrypt(src.Password)))
                .ForMember(dto => dto.RoomName, m => m.MapFrom(src => src.UserName))
                .ForMember(dto => dto.RoomId, m => m.MapFrom(src => src.UserId))
                .ForMember(dto => dto.BuildingName, m => m.MapFrom(src => src.Building.BuildingName))
                .ForMember(dto => dto.FloorName, m => m.MapFrom(src => src.Floor.FloorName));

            mapperConfiguration.CreateMap<RequestDto, Request>()
                .ForMember(dto => dto.CreationBy, m => m.MapFrom(src => src.RoomId))
                .ForMember(dto => dto.ModifiedBy, m => m.Ignore());
            mapperConfiguration.CreateMap<Request, RequestDto>()
                .ForMember(dto => dto.FeatureNameDictionary,
                    m => m.MapFrom(src => src.Feature.FeatureTranslations.ToDictionary(
                        translation => translation.Language.ToLower(), translation => translation.FeatureName)))
                .ForMember(dto => dto.RestaurantName,
                    m => m.MapFrom(src => src.Feature.Type == Enums.FeatureType.Restaurant? src.Restaurant.RestaurantTranslations.ToDictionary(
                        translation => translation.Language.ToLower(), translation => translation.RestaurantName)
                        : new Dictionary<string, string>()))
                .ForMember(dto => dto.RoomName, m => m.MapFrom(src => src.Creater.UserName))
                .ForMember(dto => dto.RoomId, m => m.MapFrom(src => src.Creater.UserId))
                .ForMember(dto => dto.CreateTime, m => m.MapFrom(src => src.CreateTime))
                .ForMember(dto => dto.ModifyTime, m => m.MapFrom(src => src.ModifyTime))
                .ForMember(dto => dto.Modifier, m => m.MapFrom(src => src.Modifier.UserName))
                .ForMember(dto => dto.RequestDetails, m => m.MapFrom(src => src.RequestDetails))
                .ForMember(dto => dto.Type, m => m.MapFrom(src => src.Feature.Type));


            mapperConfiguration.CreateMap<RequestDetail, RequestDetailDto>()
                .ForMember(dto => dto.DescriptionDictionary,
                    m => m.MapFrom(src => src.Request.Feature.Type == Enums.FeatureType.Normal? src.FeatureDetail.FeatureDetailTranslations.ToDictionary(
                        translation => translation.Language.ToLower(), translation => translation.Description)
                        : src.ItemSize.Item.ItemTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.ItemName)))
                .ForMember(dto => dto.Price, m => m.MapFrom(src => src.Price));
                //.ForMember(dto => dto.IsFree, m => m.MapFrom(src => src.FeatureDetail.IsFree));
            mapperConfiguration.CreateMap<RequestDetailDto, RequestDetail>();



            
            mapperConfiguration.CreateMap<RestaurantTypeTranslation, RestaurantTypeDto>();
            mapperConfiguration.CreateMap<RestaurantType, RestaurantTypeDto>()
                .ForMember(dto => dto.TypeNameDictionary, m => m.MapFrom(src => src.RestaurantTypeTranslations.ToDictionary(translation => translation.Language.ToLower(), translation=> translation.TypeName)));

            mapperConfiguration.CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(dto => dto.RestaurantName,
                    m => m.MapFrom(src => src.RestaurantTranslations.FirstOrDefault().RestaurantName))
                .ForMember(dto => dto.RestaurantDescription,
                    m => m.MapFrom(src => src.RestaurantTranslations.FirstOrDefault().RestaurantDescription))
                .ForMember(dto => dto.RestaurantTypeName,
                    m => m.MapFrom(src => src.RestaurantType.RestaurantTypeTranslations.FirstOrDefault().TypeName))
                .ForMember(dto => dto.RestaurantAdminPassword,
                    m => m.MapFrom(src => PasswordHelper.Decrypt(src.RestaurantAdmin.Password)))
                .ForMember(dto => dto.Image,m => m.Ignore())
               // .ForMember(dto => dto.ConsumedWaiters,m => m.MapFrom(src => src.RestaurantWaiters.Count(x=>!x.IsDeleted)))
                .ForMember(dto => dto.RestaurantTypeNameDictionary, m => m.MapFrom(src => src.RestaurantType.RestaurantTypeTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.TypeName)))
                .ForMember(dto => dto.RestaurantNameDictionary, m => m.MapFrom(src => src.RestaurantTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.RestaurantName)))
                .ForMember(dto => dto.RestaurantDescriptionDictionary, m => m.MapFrom(src => src.RestaurantTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.RestaurantDescription)));
            //.ForMember(dto => dto.RestaurantAdminUserName,
            //    m => m.MapFrom(src => PasswordHelper.Decrypt(src.RestaurantAdmin.UserName)));

            mapperConfiguration.CreateMap<Restaurant, RestaurantNameDto>()
                .ForMember(dto => dto.RestaurantNameDictionary, m => m.MapFrom(src => src.RestaurantTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.RestaurantName)));

            mapperConfiguration.CreateMap<CategoryDTO, Category>();
            mapperConfiguration.CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.CategoryName, m => m.MapFrom(src => src.CategoryTranslations.FirstOrDefault().CategoryName))
                .ForMember(dest => dest.RestaurantId, m => m.MapFrom(src => src.Menu.RestaurantId))
                .ForMember(dto => dto.CategoryNameDictionary, m => m.MapFrom(src => src.CategoryTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.CategoryName)));

            mapperConfiguration.CreateMap<MenuDTO, Menu>();
            mapperConfiguration.CreateMap<Menu, MenuDTO>()
                .ForMember(dest => dest.MenuName, m => m.MapFrom(src => src.MenuTranslations.FirstOrDefault().MenuName))
                .ForMember(dto => dto.MenuNameDictionary, m => m.MapFrom(src => src.MenuTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.MenuName)));

            mapperConfiguration.CreateMap<Menu, MenuWithCategoriesDTO>()
                .ForMember(dest => dest.MenuName, m => m.MapFrom(src => src.MenuTranslations.FirstOrDefault().MenuName))
                .ForMember(dest => dest.Categories, m => m.MapFrom(src => src.Categories));

            mapperConfiguration.CreateMap<ItemSize, SizeDto>()
                .ForMember(dest => dest.SizeName, m => m.MapFrom(src => src.Size.SizeTranslations.FirstOrDefault(x => x.Language.ToLower() == Thread.CurrentThread.CurrentCulture.Name.ToLower()).SizeName))
                .ForMember(dto => dto.SizeNameDictionary, m => m.MapFrom(src => src.Size.SizeTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.SizeName)));
            mapperConfiguration.CreateMap<ItemSideItem, SideItemDTO>()
                .ForMember(dest => dest.SideItemName, m => m.MapFrom(src => src.SideItem.SideItemTranslations.FirstOrDefault(x => x.Language.ToLower() == Thread.CurrentThread.CurrentCulture.Name.ToLower()).SideItemName))
                .ForMember(dest => dest.Value, m => m.MapFrom(src => src.SideItem.Value))
                .ForAllMembers(opts => opts.Condition(src =>
                {
                    var sideItemTranslation = src.SideItem.SideItemTranslations
                        .FirstOrDefault(x => x.Language.ToLower() ==
                                             Thread.CurrentThread.CurrentCulture.Name.ToLower());
                    return sideItemTranslation != null && sideItemTranslation
                                   .SideItemName != null;
                }));
            mapperConfiguration.CreateMap<ItemDTO, Item>();
            mapperConfiguration.CreateMap<Item, ItemDTO>()
                .ForMember(dest => dest.ItemName, m => m.MapFrom(src => src.ItemTranslations.FirstOrDefault().ItemName))
                .ForMember(dest => dest.ItemDescription, m => m.MapFrom(src => src.ItemTranslations.FirstOrDefault().ItemDescription))
                .ForMember(dest => dest.MenuId, m => m.MapFrom(src => src.Category.MenuId))
                .ForMember(dest => dest.RestaurantId, m => m.MapFrom(src => src.Category.Menu.RestaurantId))
                .ForMember(dest => dest.Sizes, m => m.MapFrom(src => src.ItemSizes.Where(x=>!x.Size.IsDeleted)))
                .ForMember(dest => dest.ItemSizes, m => m.MapFrom(src => src.ItemSizes.Where(x=>!x.Size.IsDeleted)))
                .ForMember(dest => dest.SideItems, m => m.MapFrom(src => src.ItemSideItems.Where(x=>!x.SideItem.IsDeleted)))
                .ForMember(dto => dto.ItemNameDictionary, m => m.MapFrom(src => src.ItemTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.ItemName)))
                .ForMember(dto => dto.ItemDescriptionDictionary, m => m.MapFrom(src => src.ItemTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.ItemDescription)));

            mapperConfiguration.CreateMap<ItemSize, ItemSizeDto>()
                .ForMember(dest => dest.SizeName, m => m.MapFrom(src => src.Size.SizeTranslations.FirstOrDefault().SizeName))
                .ForMember(dto => dto.SizeNameDictionary, m => m.MapFrom(src => src.Size.SizeTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.SizeName)));

            mapperConfiguration.CreateMap<SizeDto, Size>();
            mapperConfiguration.CreateMap<Size, SizeDto>()
                .ForMember(dest => dest.SizeName, m => m.MapFrom(src => src.SizeTranslations.FirstOrDefault().SizeName))
                .ForMember(dto => dto.SizeNameDictionary, m => m.MapFrom(src => src.SizeTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.SizeName)));

            mapperConfiguration.CreateMap<SideItemDTO, SideItem>();
            mapperConfiguration.CreateMap<SideItem, SideItemDTO>()
                .ForMember(dest => dest.SideItemName, m => m.MapFrom(src => src.SideItemTranslations.FirstOrDefault().SideItemName));

            mapperConfiguration.CreateMap<Item, ItemNamesDto>()
                .ForMember(dest => dest.ItemName, m => m.MapFrom(src => src.ItemTranslations.FirstOrDefault().ItemName));

            mapperConfiguration.CreateMap<RestaurantWaiterDTO, RestaurantWaiter>();
            mapperConfiguration.CreateMap<RestaurantWaiter, RestaurantWaiterDTO>()
                .ForMember(dto => dto.Password,m => m.MapFrom(src => PasswordHelper.Decrypt(src.Password)))
                .ForMember(dto => dto.BranchTitleDictionary, m => m.MapFrom(src => src.Branch.BranchTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.BranchTitle)));
                //.ForMember(dto => dto.Start, m => m.MapFrom(src => src.Package.Start.Date.ToShortDateString()))
                //.ForMember(dto => dto.End, m => m.MapFrom(src => src.Package.End.Date.ToShortDateString()));

            mapperConfiguration.CreateMap<Background, BackgroundDto>();
            mapperConfiguration.CreateMap< BackgroundDto, Background>();
            mapperConfiguration.CreateMap<Template, TemplateDTO>();

            mapperConfiguration.CreateMap<Category, CategoryNamesDTO>()
                .ForMember(dest => dest.ItemCount, m => m.MapFrom(src => src.Items.Count(x=>x.IsActive)))
                .ForMember(dto => dto.CategoryNameDictionary, m => m.MapFrom(src => src.CategoryTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.CategoryName)));

            mapperConfiguration.CreateMap<PageDTO, Page>();

            mapperConfiguration.CreateMap<BranchDto, Branch>();
            mapperConfiguration.CreateMap<Branch, BranchDto>()
                .ForMember(dest => dest.BranchTitle, m => m.MapFrom(src => src.BranchTranslations.FirstOrDefault().BranchTitle))
                .ForMember(dest => dest.BranchAddress, m => m.MapFrom(src => src.BranchTranslations.FirstOrDefault().BranchAddress))
                .ForMember(dto => dto.BranchTitleDictionary, m => m.MapFrom(src => src.BranchTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.BranchTitle)))
                .ForMember(dto => dto.BranchAddressDictionary, m => m.MapFrom(src => src.BranchTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.BranchAddress)));

            mapperConfiguration.CreateMap<FeedBackDto, FeedBack>();
            mapperConfiguration.CreateMap<FeedBack, FeedBackDto>()
                .ForMember(dto => dto.RoomName, m => m.MapFrom(src => src.Creater.UserName));
            
            mapperConfiguration.CreateMap<BuildingDto, Building>();
            mapperConfiguration.CreateMap<Building, BuildingDto>();

            mapperConfiguration.CreateMap<FloorDto, Floor>();
            mapperConfiguration.CreateMap<Floor, FloorDto>();

            mapperConfiguration.CreateMap<FeaturesBackground, FeaturesBackgroundDto>();
            mapperConfiguration.CreateMap<FeaturesBackgroundDto, FeaturesBackground>();

            mapperConfiguration.CreateMap<Available, AvailableDto>();
            mapperConfiguration.CreateMap<AvailableDto, Available>();
            mapperConfiguration.CreateMap<Room, RoomNameDto>()
                .ForMember(dto => dto.RoomName, m => m.MapFrom(src => src.UserName))
                .ForMember(dto => dto.RoomId, m => m.MapFrom(src => src.UserId));
            mapperConfiguration.CreateMap<Feature, FeatureNameDto>()
                .ForMember(dto => dto.FeatureNameDictionary,
                    m => m.MapFrom(src => src.FeatureTranslations.ToDictionary(
                        translation => translation.Language.ToLower(), translation => translation.FeatureName)));

            mapperConfiguration.CreateMap<Request, RequestStatusDto>()
                .ForMember(dto => dto.RoomName, m => m.MapFrom(src => src.Creater.UserName))
                .ForMember(dto => dto.RoomId, m => m.MapFrom(src => src.Creater.UserId))
                .ForMember(dto => dto.CreateTime, m => m.MapFrom(src => src.CreateTime))
                .ForMember(dto => dto.ModifyTime, m => m.MapFrom(src => src.ModifyTime));
            //mapperConfiguration.CreateMap<Control, ControlDto>()
            //    .ForMember(dto => dto.NameDictionary,
            //        m => m.MapFrom(src => src.ControlTranslations.ToDictionary(
            //            translation => translation.Language.ToLower(), translation => translation.Name)));

            Mapper.Initialize(mapperConfiguration); 
            //Mapper.Initialize(m =>
            //{
            //    m.CreateMap<User, UserDto>();

            //});
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            ECompoundDALConfig.RegisterTypes(container);
            container.RegisterType<IUserService, UserService>(new PerResolveLifetimeManager())
                .RegisterType<IRefreshTokenService, RefreshTokenService>(new PerResolveLifetimeManager())
                .RegisterType<ISupervisorService, SupervisorService>(new PerResolveLifetimeManager())
                .RegisterType<IReceptionistService, ReceptionistService>(new PerResolveLifetimeManager())
                .RegisterType<IFeatureService, FeatureService>(new PerResolveLifetimeManager())
                .RegisterType<IFeatureTranslationService, FeatureTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<ISupervisorFeatureService, SupervisorFeatureService>(new PerResolveLifetimeManager())
                .RegisterType<IFeatureDetailService, FeatureDetailService>(new PerResolveLifetimeManager())
                .RegisterType<IFeatureDetailTranslationService, FeatureDetailTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IManageStorage, ManageStorage>(new PerResolveLifetimeManager())
                .RegisterType<IRoomService, RoomService>(new PerResolveLifetimeManager())
                .RegisterType<IRequestService, RequestService>(new PerResolveLifetimeManager())
                .RegisterType<IRequestDetailService, RequestDetailService>(new PerResolveLifetimeManager())
                .RegisterType<IAdminService, AdminService>(new PerResolveLifetimeManager())
                .RegisterType<IPackageService, PackageService>(new PerResolveLifetimeManager())

                .RegisterType<IRestaurantTypeService, RestaurantTypeService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantTypeTranslationService, RestaurantTypeTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantService, RestaurantService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantTranslationService, RestaurantTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IMenuService, MenuService>(new PerResolveLifetimeManager())
                .RegisterType<IMenuTranslationService, MenuTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<ICategoryService, CategoryService>(new PerResolveLifetimeManager())
                .RegisterType<ICategoryTranslationService, CategoryTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IitemService, ItemService>(new PerResolveLifetimeManager())
                .RegisterType<IitemTranslationService, ItemTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantAdminService, RestaurantAdminService>(new PerResolveLifetimeManager())
                .RegisterType<ISizeService, SizeService>(new PerResolveLifetimeManager())
                .RegisterType<ISizeTranslationService, SizeTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<ISideItemService, SideItemService>(new PerResolveLifetimeManager())
                .RegisterType<ISideItemTranslationService, SideItemTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IItemSideItemService, ItemSideItemService>(new PerResolveLifetimeManager())
                .RegisterType<IItemSizeService, ItemSizeService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantWaiterService, RestaurantWaiterService>(new PerResolveLifetimeManager())
                .RegisterType<IBackgroundService, BackgroundService>(new PerResolveLifetimeManager())
                .RegisterType<ITemplateService, TemplateService>(new PerResolveLifetimeManager())
                .RegisterType<IPageService, PageService>(new PerResolveLifetimeManager())
                .RegisterType<IBranchService, BranchService>(new PerResolveLifetimeManager())
                .RegisterType<IBranchTranslationService, BranchTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IFeedBackService, FeedBackService>(new PerResolveLifetimeManager())
                .RegisterType<IBuildingService, BuildingService>(new PerResolveLifetimeManager())
                .RegisterType<IFloorService, FloorService>(new PerResolveLifetimeManager())
                .RegisterType<IFeaturesBackgroundService, FeaturesBackgroundService>(new PerResolveLifetimeManager())
                //.RegisterType<IControlService, ControlService>(new PerResolveLifetimeManager())
                .RegisterType<IFeatureControlService, FeatureControlService>(new PerResolveLifetimeManager())
                .RegisterType<IAvailableService, AvailableService>(new PerResolveLifetimeManager());
        }
    }
}
