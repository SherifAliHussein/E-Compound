using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.BLL.Services.ManageStorage;
using E_Compound.Common;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
//using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class RestaurantFacade : BaseFacade, IRestaurantFacade
    {
        private IRestaurantTypeService _restaurantTypeService;
        private IRestaurantTypeTranslationService _restaurantTypeTranslationService;
        private IRestaurantService _restaurantService;
        private IRestaurantTranslationService _restaurantTranslationService;
        private IUserService _userService;
        private IRestaurantAdminService _restaurantAdminService;
        private IManageStorage _manageStorage;
        private IRestaurantWaiterService _restaurantWaiterService;
        private IAdminService _globalAdminService;
        private IPackageService _packageService;
        private IMenuService _menuService;
        private IFeedBackService _feedBackService;

        public RestaurantFacade(IRestaurantTypeService restaurantTypeService,
            IRestaurantTypeTranslationService restaurantTypeTranslationService
            , IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService,
            IUserService userService, IRestaurantAdminService restaurantAdminService
            , IManageStorage manageStorage,IRestaurantWaiterService restaurantWaiterService, IAdminService globalAdminService, IUnitOfWorkAsync unitOfWork, IPackageService packageService, IMenuService menuService, IFeedBackService feedBackService) : base(unitOfWork)
        {
            _restaurantTypeService = restaurantTypeService;
            _restaurantTypeTranslationService = restaurantTypeTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _userService = userService;
            _restaurantAdminService = restaurantAdminService;
            _manageStorage = manageStorage;
            _restaurantWaiterService = restaurantWaiterService;
            _globalAdminService = globalAdminService;
            _packageService = packageService;
            _menuService = menuService;
            _feedBackService = feedBackService;
        }

        public RestaurantFacade(IRestaurantTypeService restaurantTypeService,
            IRestaurantTypeTranslationService restaurantTypeTranslationService,
            IRestaurantService restaurantService, IRestaurantTranslationService restaurantTranslationService,
            IUserService userService, IRestaurantAdminService restaurantAdminService, IManageStorage manageStorage, IPackageService packageService, IMenuService menuService, IFeedBackService feedBackService)
        {
            _restaurantTypeService = restaurantTypeService;
            _restaurantTypeTranslationService = restaurantTypeTranslationService;
            _restaurantService = restaurantService;
            _restaurantTranslationService = restaurantTranslationService;
            _userService = userService;
            _restaurantAdminService = restaurantAdminService;
            _manageStorage = manageStorage;
            _packageService = packageService;
            _menuService = menuService;
            _feedBackService = feedBackService;
        }

        public List<RestaurantTypeDto> GetAllRestaurantType(string language, long userId)
        {
            var t = _restaurantTypeTranslationService.GeRestaurantTypeTranslation(language, userId);
            return Mapper.Map<List<RestaurantTypeDto>>(t);
        }

        public RestaurantTypeDto GetRestaurantTypeById(long restaurantTypeId)
        {
            return Mapper.Map<RestaurantTypeDto>(
                _restaurantTypeService.Find(restaurantTypeId));
        }

        public bool AddRestaurantType(RestaurantTypeDto restaurantTypeDto,long userId)
        {
            ValidateRestaurantType(restaurantTypeDto);
            RestaurantType restaurantType = new RestaurantType();
            restaurantType.RestaurantTypeId = _restaurantTypeService.GetLastRecordId() + 1;
            restaurantType.RestaurantTypeTranslations = new List<RestaurantTypeTranslation>();
            //foreach (var type in restaurantTypeDto.TypeName)
            //{

            foreach (var typeName in restaurantTypeDto.TypeNameDictionary)
            {
                if (_restaurantTypeTranslationService.CheckRepeatedType(typeName.Value, typeName.Key,
                    restaurantType.RestaurantTypeId, userId))
                {
                    throw new ValidationException(ErrorCodes.RestaurantTypeAlreadyExist);
                }

                restaurantType.RestaurantTypeTranslations.Add(new RestaurantTypeTranslation
                {
                    Language = typeName.Key.ToLower(),
                    TypeName = typeName.Value,
                    RestaurantTypeId = restaurantType.RestaurantTypeId

                });
            }

            //}
            restaurantType.AdminId = userId;
            _restaurantTypeTranslationService.InsertRange(restaurantType.RestaurantTypeTranslations);
            _restaurantTypeService.Insert(restaurantType);
            SaveChanges();
            return true;
        }

        public void UpdateRestaurantType(RestaurantTypeDto restaurantTypeDto, long userId)
        {
            ValidateRestaurantType(restaurantTypeDto);
            var restaurantType = _restaurantTypeService.Find(restaurantTypeDto.RestaurantTypeId);
            if (restaurantType == null) throw new NotFoundException(ErrorCodes.RestaurantTypeNotFound);
            foreach (var typeName in restaurantTypeDto.TypeNameDictionary)
            {
                if (_restaurantTypeTranslationService.CheckRepeatedType(typeName.Value, typeName.Key,
                    restaurantType.RestaurantTypeId, userId))
                {
                    throw new ValidationException(ErrorCodes.RestaurantTypeAlreadyExist);
                }
                var restaurantTypeTranslation =
                    restaurantType.RestaurantTypeTranslations.FirstOrDefault(
                        x => x.Language.ToLower() == typeName.Key.ToLower());
                if (restaurantTypeTranslation != null) restaurantTypeTranslation.TypeName = typeName.Value;
                else
                    restaurantType.RestaurantTypeTranslations.Add(new RestaurantTypeTranslation
                    {
                        Language = typeName.Key.ToLower(),
                        TypeName = typeName.Value,
                        RestaurantTypeId = restaurantType.RestaurantTypeId
                    });
            }
            _restaurantTypeService.Update(restaurantType);
            SaveChanges();
        }

        private void ValidateRestaurantType(RestaurantTypeDto restaurantTypeDto)
        {
            foreach (var typeName in restaurantTypeDto.TypeNameDictionary)
            {
                if (string.IsNullOrEmpty(typeName.Value))
                    throw new ValidationException(ErrorCodes.EmptyRestaurantType);
                if (typeName.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.RestaurantTypeExceedLength);
                if (Strings.SupportedLanguages.All(x => x.ToLower() != typeName.Key.ToLower()))
                    throw new ValidationException(ErrorCodes.UnSupportedLanguage);
            }
        }

        public void DeleteRestaurantType(long restaurantTypeId)
        {
            var restaurantType = _restaurantTypeService.Find(restaurantTypeId);
            if (restaurantType == null) throw new NotFoundException(ErrorCodes.RestaurantTypeNotFound);
            restaurantType.IsDeleted = true;
            foreach (var restaurant in restaurantType.Restaurants)
            {
                restaurant.IsDeleted = true;
                restaurant.RestaurantAdmin.IsDeleted = true;
                
                _restaurantService.Update(restaurant);
                var waiters = _restaurantWaiterService.GetAlRestaurantWaitersByRestaurantId(restaurant.RestaurantId);
                waiters.ForEach(x => { x.IsDeleted = true; _restaurantWaiterService.Update(x); });
                
                var globalAdmin = _globalAdminService.Find(restaurant.AdminId);
                //var packages = _packageService.GetAllPackagesByAdminId(globalAdmin.UserId);
                //Parallel.ForEach(packages, (package) =>
                //{
                //    UpdateSubscription(globalAdmin, package.PackageGuid, package.Waiters.Count(x => !x.IsDeleted));
                //});
            }
            _restaurantTypeService.Update(restaurantType);

           SaveChanges();
        }

        public void AddRestaurant(RestaurantDTO restaurantDto, string path, long userId)
        {
            ValidateRestaurant(restaurantDto,userId);

            var restaurantType = _restaurantTypeService.Find(restaurantDto.RestaurantTypeId);


            Restaurant restaurant = new Restaurant
            {
                RestaurantTypeId = restaurantDto.RestaurantTypeId,
                IsActive = false,
                RestaurantTranslations = new List<RestaurantTranslation>(),
                AdminId = userId,
                //WaitersLimit = restaurantDto.WaitersLimit

            };
            foreach (var restaurantName in restaurantDto.RestaurantNameDictionary)
            {
                restaurant.RestaurantTranslations.Add(new RestaurantTranslation
                {
                    Language = restaurantName.Key.ToLower(),
                    RestaurantName = restaurantName.Value,
                    RestaurantDescription = restaurantDto.RestaurantDescriptionDictionary[restaurantName.Key]
                });
            }
            restaurant.RestaurantAdmin = new RestaurantAdmin
            {
                UserName = restaurantDto.RestaurantAdminUserName,
                Password = PasswordHelper.Encrypt(restaurantDto.RestaurantAdminPassword),
                Role = Enums.RoleType.RestaurantAdmin,
                IsDeleted = false,
                IsActive = true
            };
            restaurant.BackgroundId = Strings.BackgroundId;
            //_userService.Insert(restaurant.RestaurantAdmin);
            _restaurantAdminService.Insert(restaurant.RestaurantAdmin);
            _restaurantTranslationService.InsertRange(restaurant.RestaurantTranslations);
            _restaurantService.Insert(restaurant);
            SaveChanges();
            restaurant.RestaurantAdmin.RestaurantId = restaurant.RestaurantId;
            _restaurantAdminService.Update(restaurant.RestaurantAdmin);
            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + restaurant.RestaurantId, restaurantDto.Image,
                restaurant.RestaurantId.ToString());
        }

        public RestaurantDTO GetRestaurant(long restaurantId, string language)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new NotFoundException(ErrorCodes.RestaurantDeleted);
            //var restaurantAdmin = _restaurantAdminService.Find(restaurant.RestaurantAdminId);
            //if(restaurantAdmin == null) throw new NotFoundException(ErrorCodes.RestaurantAdminNotFound);

            //var restaurantdto = Mapper.Map<Restaurant, RestaurantDTO>(restaurant, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.RestaurantTranslations = src.RestaurantTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            src.RestaurantType.RestaurantTypeTranslations = src.RestaurantType.RestaurantTypeTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //        }
            //    );
            //});

            //restaurantdto.RestaurantAdminPassword = restaurantAdmin.UserName;
            //restaurantdto.RestaurantAdminPassword = restaurantAdmin.Password;
            var restaurantDto= Mapper.Map<RestaurantDTO>(restaurant);
            //restaurantDto.ConsumedWaiters = _restaurantWaiterService
            //    .GetAllRestaurantWaiters(restaurant.RestaurantId, 1, 0, Strings.DefaultLanguage).TotalCount;
            return restaurantDto;
        }

        private void ValidateRestaurant(RestaurantDTO restaurantDto,  long userId)
        {
            foreach (var restaurantName in restaurantDto.RestaurantNameDictionary)
            {
                if (string.IsNullOrEmpty(restaurantName.Value))
                    throw new ValidationException(ErrorCodes.EmptyRestaurantName);
                if (restaurantName.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.RestaurantNameExceedLength);
                if (_restaurantTranslationService.CheckRestaurantNameExist(restaurantName.Value, restaurantName.Key,restaurantDto.RestaurantId, userId))
                    throw new ValidationException(ErrorCodes.RestaurantNameAlreadyExist);
                if (Strings.SupportedLanguages.All(x => x.ToLower() != restaurantName.Key.ToLower()))
                    throw new ValidationException(ErrorCodes.UnSupportedLanguage);
            }
            foreach (var restaurantDescription in restaurantDto.RestaurantDescriptionDictionary)
            {
                if (string.IsNullOrEmpty(restaurantDescription.Value))
                    throw new ValidationException(ErrorCodes.EmptyRestaurantDescription);
                if (Strings.SupportedLanguages.All(x => x.ToLower() != restaurantDescription.Key.ToLower()))
                    throw new ValidationException(ErrorCodes.UnSupportedLanguage);
            }
            if (string.IsNullOrEmpty(restaurantDto.RestaurantAdminUserName))
                throw new ValidationException(ErrorCodes.EmptyRestaurantAdminUserName);
            if (string.IsNullOrEmpty(restaurantDto.RestaurantAdminPassword))
                throw new ValidationException(ErrorCodes.EmptyRestaurantAdminPassword);
            if (restaurantDto.RestaurantAdminPassword.Length < 8 ||
                restaurantDto.RestaurantAdminPassword.Length > 25)
                throw new ValidationException(ErrorCodes.RestaurantAdminPasswordLengthNotMatched);
            if (_restaurantAdminService.CheckUserNameDuplicated(restaurantDto.RestaurantAdminUserName,
                restaurantDto.RestaurantId))
                throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
            //if (_userService.CheckUserNameDuplicated(restaurantDto.RestaurantAdminUserName))
            //    throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
        }

        public PagedResultsDto GetAllRestaurant(string language, int page, int pageSize, long userId)
        {
            var allRestaurant = _restaurantTranslationService.GetAllRestaurant(language, page, pageSize,userId);
            //foreach (var restaurant in (List<RestaurantDTO>)allRestaurant.Data)
            //{
            //    restaurant.ConsumedWaiters = _restaurantWaiterService
            //        .GetAllRestaurantWaiters(restaurant.RestaurantId, 1, 0, Strings.DefaultLanguage).TotalCount;
            //}
            return allRestaurant;
        }
        public List<RestaurantNameDto> GetAllRestaurantsName(long adminId)
        {
            return Mapper.Map<List<RestaurantNameDto>>(_restaurantService.GetRestaurantsName(adminId));
        }

        public void ActivateRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (!restaurant.IsReady) throw new ValidationException(ErrorCodes.RestaurantIsNotReady);
            if (!_menuService.Query(x => x.RestaurantId == restaurant.RestaurantId && x.IsActive).Select().Any())
                throw new ValidationException(ErrorCodes.RestaurantMenuDoesNotActivated);
            //if(restaurant.RestaurantType.RestaurantTypeTranslations.Select(t=>t.Language))
            //if (Strings.SupportedLanguages.Any(x => !restaurant.RestaurantTranslations.Select(m => m.Language.ToLower())
            //    .Contains(x.ToLower())))
            //    throw new ValidationException(ErrorCodes.CategoryIsNotTranslated);
            restaurant.IsActive = true;
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public void DeActivateRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            restaurant.IsActive = false;
            if (restaurant.FeatureId.HasValue)
            {
                var count = restaurant.Feature.Restaurants.Count(x => x.IsActive && !x.IsDeleted);
                if (count <= 0 )
                {
                    restaurant.Feature.IsActive = false;
                }
            }
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public void DeleteRestaurant(long restaurantId)
        {
            var restaurant = _restaurantService.Find(restaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            restaurant.IsDeleted = true;
            restaurant.RestaurantAdmin.IsDeleted = true;
            var waiters = _restaurantWaiterService.GetAlRestaurantWaitersByRestaurantId(restaurant.RestaurantId);
            waiters.ForEach(x => { x.IsDeleted = true; _restaurantWaiterService.Update(x); });

            //var globalAdmin = _globalAdminService.Find(restaurant.AdminId);
            //var packages = _packageService.GetAllPackagesByAdminId(globalAdmin.UserId);
            //Parallel.ForEach(packages, (package) =>
            //{
            //    UpdateSubscription(globalAdmin, package.PackageGuid, package.Waiters.Count(x => !x.IsDeleted));
            //});
            if (restaurant.FeatureId.HasValue)
            {
                var count = restaurant.Feature.Restaurants.Count(x => x.IsActive && !x.IsDeleted);
                if (count < 0)
                {
                    restaurant.Feature.IsActive = false;
                }
            }
            _restaurantService.Update(restaurant);
            

            SaveChanges();
        }

        public void UpdateRestaurant(RestaurantDTO restaurantDto, string path, long userId)
        {
            Restaurant restaurant = _restaurantService.Find(restaurantDto.RestaurantId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            ValidateRestaurant(restaurantDto,userId);
            var restaurantType = _restaurantTypeService.Find(restaurantDto.RestaurantTypeId);
            if (Strings.SupportedLanguages.Any(x => !restaurantType.RestaurantTypeTranslations
                .Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.RestaurantTypeIsNotTranslated);
            restaurant.RestaurantTypeId = restaurantDto.RestaurantTypeId;
            restaurant.RestaurantAdmin.UserName = restaurantDto.RestaurantAdminUserName;
            restaurant.RestaurantAdmin.Password = PasswordHelper.Encrypt(restaurantDto.RestaurantAdminPassword);
            //restaurant.WaitersLimit = restaurantDto.WaitersLimit;

            foreach (var restaurantName in restaurantDto.RestaurantNameDictionary)
            {
                var restaurantTranslation =
                    restaurant.RestaurantTranslations.FirstOrDefault(x => x.Language.ToLower() == restaurantName.Key.ToLower());
                if (restaurantTranslation == null)
                {
                    restaurant.RestaurantTranslations.Add(new RestaurantTranslation
                    {
                        Language = restaurantName.Key.ToLower(),
                        RestaurantName = restaurantName.Value,
                        RestaurantDescription = restaurantDto.RestaurantDescriptionDictionary[restaurantName.Key]
                    });
                }
                else
                {
                    restaurantTranslation.RestaurantName = restaurantName.Value;
                    restaurantTranslation.RestaurantDescription = restaurantDto.RestaurantDescriptionDictionary[restaurantName.Key];
                }
            }

            _restaurantService.Update(restaurant);
            SaveChanges();
            if (restaurantDto.IsLogoChange)
                _manageStorage.UploadImage(path + "\\" + "Restaurant-" + restaurant.RestaurantId, restaurantDto.Image,
                    restaurant.RestaurantId.ToString());
        }

        public RestaurantDTO CheckRestaurantReady(long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            RestaurantDTO restaurantDto = new RestaurantDTO();
            restaurantDto.IsReady = restaurant.IsReady;
            return restaurantDto;
        }

        public void PublishRestaurant(long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (!_menuService.Query(x => x.RestaurantId == restaurant.RestaurantId && x.IsActive).Select().Any())
                throw new ValidationException(ErrorCodes.RestaurantMenuDoesNotActivated);
            //if (Strings.SupportedLanguages.Any(x => !restaurant.RestaurantTranslations.Select(m => m.Language.ToLower())
            //    .Contains(x.ToLower())))
            //    throw new ValidationException(ErrorCodes.CategoryIsNotTranslated);
            restaurant.IsReady = true;
            _restaurantService.Update(restaurant);
            SaveChanges();
        }

        public ResturantInfoDto GetGlobalRestaurantInfo(long restaurantId, string role,string language)
        {
            Restaurant restaurant;
            //if (role == Enums.RoleType.RestaurantAdmin.ToString())
            //{
            //    restaurant = _restaurantService.GetRestaurantByAdminId(userId);
            //    if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            //    if (restaurant.IsDeleted) throw new NotFoundException(ErrorCodes.RestaurantDeleted);
            //}
            //else
            //{
            //    var waiter = _restaurantWaiterService.Find(userId);
            //    restaurant = _restaurantService.Find(waiter.RestaurantId);
            //    if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            //    if (restaurant.IsDeleted) throw new NotFoundException(ErrorCodes.RestaurantDeleted);

            //}
            restaurant = _restaurantService.Find(restaurantId);
            var restaurantdto = new ResturantInfoDto
            {
                ResturentId = restaurant.RestaurantId,
                BackgroundId = restaurant.BackgroundId,
                RestaurantName = restaurant.RestaurantTranslations.FirstOrDefault(x=>x.Language.ToLower() == language.ToLower()).RestaurantName,
                Rate = _feedBackService.GetRestaurantRate(restaurantId)

            };
            return restaurantdto;
        }

        private void UpdateSubscription(Admin globalAdmin, Guid packageGuid, int consumed)
        {
            string url = ConfigurationManager.AppSettings["subscriptionURL"];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/Users/EditUserConsumer");
            //request.Headers.Add("X-Auth-Token:" + token);
            request.ContentType = "application/json";
            request.Method = "POST";
            var serializer = JsonConvert.SerializeObject(new
            {
                userConsumer = consumed,
                userAccountId = globalAdmin.UserAccountId,
                backageGuid = packageGuid
            });
            //request.ContentLength = serializer.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = serializer;

                streamWriter.Write(json);
            }
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var infoResponse = readStream.ReadToEnd();

                response.Close();
                receiveStream.Close();
                readStream.Close();
            }
        }

    }
}
