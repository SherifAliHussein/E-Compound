using E_Compound.Common;
using E_Compound.DAL.Entities.Model;

namespace E_Compound.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<E_Compound.DAL.Entities.ECompoundContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(E_Compound.DAL.Entities.ECompoundContext context)
        {

            var invetationFeature = context.Features.Add(new Feature
            {
                IsDeleted = false,
                IsActive = true,
                CreateTime = DateTime.Now,
                Type = Enums.FeatureType.Invetation,
                Creater = new Admin()

            });
            context.FeatureTranslations.Add(new FeatureTranslation
            {
                Feature = invetationFeature,
                FeatureName = "Invetation",
                Language = "en"

            });

            context.FeatureTranslations.Add(new FeatureTranslation
            {
                Feature = invetationFeature,
                FeatureName = "الدعوات",
                Language = "ar"

            });
            //});//context.Users.Add(new Admin
            //{
            //    IsDeleted = false,
            //    Password = "wArilz/QIT55GuLgpRQlCHX0lir/WTXM8yc33MPiN3Bl26dnvS752gHPadYZoL20",
            //    UserName = "admin",
            //    Role = Enums.RoleType.Admin
            //});
            //  This method will be called after migrating to the latest version.

            //context.FeaturesBackgrounds.Add(new FeaturesBackground
            //{
            //    FeaturesBackgroundId = 1
            //});
            //context.FeaturesBackgrounds.Add(new FeaturesBackground
            //{
            //    FeaturesBackgroundId = 2
            //});
            //context.FeaturesBackgrounds.Add(new FeaturesBackground
            //{
            //    FeaturesBackgroundId = 3
            //});
            //context.FeaturesBackgrounds.Add(new FeaturesBackground
            //{
            //    FeaturesBackgroundId = 4
            //});
            //context.FeaturesBackgrounds.Add(new FeaturesBackground
            //{
            //    FeaturesBackgroundId = 5
            //});


            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 1
            //});
            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 2
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 3
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 4
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 5
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 6
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 7
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 8
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 9
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 10
            //});
            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 11
            //});
            //context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 12
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 13
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 14
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 15
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 16
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 17
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 18
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 19
            //}); context.Backgrounds.Add(new Background
            //{
            //    IsDeleted = false,
            //    IsActive = true,
            //    BackgroundId = 20
            //});

            //context.Templates.Add(new Template
            //{
            //    Id = 1,
            //    ItemCount = 3
            //});
            //context.Templates.Add(new Template
            //{
            //    Id = 2,
            //    ItemCount = 4
            //});
            //context.Templates.Add(new Template
            //{
            //    Id = 3,
            //    ItemCount = 2
            //});
            //context.Templates.Add(new Template
            //{
            //    Id = 4,
            //    ItemCount = 5
            //});

            //context.Templates.Add(new Template
            //{
            //    Id = 5,
            //    ItemCount = 1
            //});
            //context.Templates.Add(new Template
            //{
            //    Id = 6,
            //    ItemCount = 10
            //});

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
