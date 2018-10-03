using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Compound.DAL.Entities;
using E_Compound.DAL.Entities.Model;
using Microsoft.Build.Framework.XamlTypes;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace E_Compound.DAL
{
    public static class ECompoundDALConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IDataContextAsync, ECompoundContext>(new PerResolveLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryProvider, RepositoryProvider>(
                    new PerResolveLifetimeManager(),
                    new InjectionConstructor(new object[] {new RepositoryFactories()})
                )
                .RegisterType<IRepositoryAsync<User>, Repository<User>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RefreshToken>, Repository<RefreshToken>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Supervisor>, Repository<Supervisor>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Receptionist>, Repository<Receptionist>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Feature>, Repository<Feature>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<FeatureTranslation>, Repository<FeatureTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SupervisorFeature>, Repository<SupervisorFeature>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<FeatureDetail>, Repository<FeatureDetail>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<FeatureDetailTranslation>, Repository<FeatureDetailTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Room>, Repository<Room>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<UnitType>, Repository<UnitType>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<UnitTypeTranslation>, Repository<UnitTypeTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<UserCategory>, Repository<UserCategory>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<UserCategoryTranslation>, Repository<UserCategoryTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Unit>, Repository<Unit>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Request>, Repository<Request>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RequestDetail>, Repository<RequestDetail>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Package>, Repository<Package>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Admin>, Repository<Admin>>(new PerResolveLifetimeManager())


                .RegisterType<IRepositoryAsync<RestaurantAdmin>, Repository<RestaurantAdmin>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantWaiter>, Repository<RestaurantWaiter>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Entities.Model.Category>, Repository<Entities.Model.Category>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<CategoryTranslation>, Repository<CategoryTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Item>, Repository<Item>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemTranslation>, Repository<ItemTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Menu>, Repository<Menu>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<MenuTranslation>, Repository<MenuTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Restaurant>, Repository<Restaurant>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantTranslation>, Repository<RestaurantTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantType>, Repository<RestaurantType>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantTypeTranslation>, Repository<RestaurantTypeTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Size>, Repository<Size>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SizeTranslation>, Repository<SizeTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SideItem>, Repository<SideItem>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SideItemTranslation>, Repository<SideItemTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemSideItem>, Repository<ItemSideItem>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemSize>, Repository<ItemSize>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Background>, Repository<Background>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Template>, Repository<Template>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Page>, Repository<Page>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Branch>, Repository<Branch>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<BranchTranslation>, Repository<BranchTranslation>>(
                    new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<FeedBack>, Repository<FeedBack>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Building>, Repository<Building>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Floor>, Repository<Floor>>(new PerResolveLifetimeManager())
            .RegisterType<IRepositoryAsync<FeaturesBackground>, Repository<FeaturesBackground>>(new PerResolveLifetimeManager())
                //.RegisterType<IRepositoryAsync<Control>, Repository<Control>>(new PerResolveLifetimeManager())
                //.RegisterType<IRepositoryAsync<ControlTranslation>, Repository<ControlTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<FeatureControl>, Repository<FeatureControl>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Available>, Repository<Available>>(new PerResolveLifetimeManager());

        }

    }
}
