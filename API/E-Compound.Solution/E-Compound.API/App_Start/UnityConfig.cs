using E_Compound.BLL.Services;
using E_Compound.BLL.Services.Interfaces;
using System.Web.Http;
using E_Compound.BLL;
using Unity;
using Unity.WebApi;
using Microsoft.Practices.Unity;
using Unity.Lifetime;
using System;

namespace E_Compound.API
{
    public static class UnityConfig
    {

        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            ApplyMapping(container, true);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
        public static void RegisterTypes(HttpConfiguration config)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            var container = new UnityContainer();

            // TODO: Register your types here

            ApplyMapping(container, false);


            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver =
                config.DependencyResolver = new UnityDependencyResolver(container);


        }

        public static void ApplyMapping(IUnityContainer container, bool applyDependencyResolver)
        {
            container.RegisterType<IUserFacade, UserFacade>(new PerResolveLifetimeManager())
                .RegisterType<IRefreshTokenFacade, RefreshTokenFacade>(new PerResolveLifetimeManager())
                .RegisterType<IFeatureFacade, FeatureFacade>(new PerResolveLifetimeManager())
                .RegisterType<IRoomFacade, RoomFacade>(new PerResolveLifetimeManager())
                .RegisterType<IRequestFacade, RequestFacade>(new PerResolveLifetimeManager())

                .RegisterType<IRestaurantFacade, RestaurantFacade>(new PerResolveLifetimeManager())
                .RegisterType<IMenuFacade, MenuFacade>(new PerResolveLifetimeManager())
                .RegisterType<ICategoryFacade, CategoryFacade>(new PerResolveLifetimeManager())
                .RegisterType<IitemFacade, ItemFacade>(new PerResolveLifetimeManager())
                .RegisterType<ISizeFacade, SizeFacade>(new PerResolveLifetimeManager())
                .RegisterType<ISideItemFacade, SideItemFacade>(new PerResolveLifetimeManager())
                .RegisterType<IBackgroundFacade, BackgroundFacade>(new PerResolveLifetimeManager())
                .RegisterType<ITemplateFacade, TemplateFacade>(new PerResolveLifetimeManager())
                .RegisterType<IBranchFacade, BranchFacade>(new PerResolveLifetimeManager())
                .RegisterType<IFeedBackFacade, FeedBackFacade>(new PerResolveLifetimeManager())
                .RegisterType<IBuildingFacade, BuildingFacade>(new PerResolveLifetimeManager())
                .RegisterType<IFloorFacade, FloorFacade>(new PerResolveLifetimeManager())
                .RegisterType<IUnitTypeFacade, UnitTypeFacade>(new PerResolveLifetimeManager())
                .RegisterType<IUserCategoryFacade, UserCategoryFacade>(new PerResolveLifetimeManager())
                .RegisterType<IUnitFacade, UnitFacade>(new PerResolveLifetimeManager())
                .RegisterType<IFeaturesBackgroundFacade, FeaturesBackgroundFacade>(new PerResolveLifetimeManager());
             

            ECompoundBLLConfig.RegisterTypes(container);
            if (applyDependencyResolver)
                GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);


        }
    }
}