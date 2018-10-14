using System;
using System.Collections.Generic;
using System.Linq;
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
using Repository.Pattern.UnitOfWork;
using Unity.Interception.Utilities;

namespace E_Compound.BLL.Services
{
    public class FeatureFacade:BaseFacade,IFeatureFacade
    {
        private IFeatureService _featureService;
        private IFeatureTranslationService _featureTranslationService;
        private IUserService _userService;
        private IFeatureDetailService _featureDetailService;
        private IFeatureDetailTranslationService _featureDetailTranslationService;
        private IManageStorage _manageStorage;
        private IRoomService _roomService;
        private IRestaurantService _restaurantService;
        //private IControlService _controlService;
        private IFeatureControlService _featureControlService;

        private IAvailableService _availableService;

        private ISupervisorFeatureService _supervisorFeatureService;
        private ITechnicianService _technicianService;
        private IReceptionistService _receptionistService;
        public FeatureFacade(IUnitOfWorkAsync unitOFWork, IFeatureService featureService, IFeatureTranslationService featureTranslationService, IUserService userService, IFeatureDetailService featureDetailService, IFeatureDetailTranslationService featureDetailTranslationService, IManageStorage manageStorage, IRoomService roomService, IRestaurantService restaurantService, IFeatureControlService featureControlService, ISupervisorFeatureService supervisorFeatureService, IAvailableService availableService, IReceptionistService receptionistService, ITechnicianService technicianService) : base(unitOFWork)
        {
            _featureService = featureService;
            _featureTranslationService = featureTranslationService;
            _userService = userService;
            _featureDetailService = featureDetailService;
            _featureDetailTranslationService = featureDetailTranslationService;
            _manageStorage = manageStorage;
            _roomService = roomService;
            _restaurantService = restaurantService;
            //_controlService = controlService;
            _featureControlService = featureControlService;
            _supervisorFeatureService = supervisorFeatureService;
            _availableService = availableService;
            _receptionistService = receptionistService;
            _technicianService = technicianService;
        }

        public PagedResultsDto GetAllFeatures(long adminId, int page, int pageSize)
        {
            var user = _userService.Find(adminId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            var featuresCount = _featureService.Query(x => !x.IsDeleted && x.CreationBy == adminId).Select().Count();
            var features = Mapper.Map<List<FeatureDto>>(_featureService.GetAllFeaturesAdminId(adminId, page, pageSize));
            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = featuresCount,
                Data = features
            };
            return results;
        }
        public PagedResultsDto GetAllActiveFeatures(long adminId, int page, int pageSize,string role)
        {
            var user = _userService.Find(adminId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            int featuresCount = 0;
            List<FeatureDto> features = null;
            if (role == Enums.RoleType.Admin.ToString())
            {
                featuresCount = _featureService.Query(x => !x.IsDeleted && x.CreationBy == adminId).Select().Count();
                features = Mapper.Map<List<FeatureDto>>(_featureService.GetAllActiveFeaturesAdminId(adminId, page, pageSize));
                //PagedResultsDto results = new PagedResultsDto
                //{
                //    TotalCount = featuresCount,
                //    Data = features
                //};
                //return results;
            }
            else if (role == Enums.RoleType.Room.ToString())
            {
                var room = _roomService.Find(adminId);
                featuresCount = _featureService.Query(x => !x.IsDeleted && x.CreationBy == room.AdminId).Select().Count();
                //features = Mapper.Map<List<FeatureDto>>(_featureService.GetAllActiveFeaturesAdminId(room.AdminId, page, pageSize));
                features = Mapper.Map<List<Feature>, List<FeatureDto>>(_featureService.GetAllActiveFeaturesAdminId(room.AdminId, page, pageSize), opt =>
                {
                    opt.BeforeMap((src, dest) =>
                        {
                            foreach (Feature feature  in src)
                            {
                                feature.Restaurants = feature.Restaurants.Where(x => x.IsActive && !x.IsDeleted)
                                    .ToList();
                            }

                        }
                    );
                });
            }

            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = featuresCount,
                Data = features
            };
            return results;
        }
        public void AddFeature(FeatureDto featureDto, long adminId, string path)
        {
            var user = _userService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            ValidateFeature(featureDto, adminId);

            var feature = Mapper.Map<Feature>(featureDto);
            feature.CreationBy = adminId;
            feature.CreateTime = DateTime.Now;
            feature.IsActive = true;
            foreach (var featureControlDto in featureDto.FeatureControl)
            {
                feature.FeatureControls.Add(new FeatureControl
                {
                    Control = featureControlDto.Control,
                    ControlType =
                        featureControlDto.Control == Enums.Control.ListOfText ||
                        featureControlDto.Control == Enums.Control.ListOfTextAndImage ||
                        featureControlDto.Control == Enums.Control.ListOfAvailable
                            ? Enums.ControlType.Single
                            : Enums.ControlType.None,
                    Order = featureControlDto.Order,
                    IsActive = true
                     
                });
            }
            //foreach (var featureName in featureDto.FeatureNameDictionary)
            //{
            //    feature.FeatureTranslations.Add(new FeatureTranslation
            //    {
            //        FeatureName = featureName.Value,
            //        Language = featureName.Key.ToLower()
            //    });
            //}
            //feature.FeatureDetails.ForEach(x =>
            //{
            //    x.CreationBy = adminId;
            //    x.CreateTime = DateTime.Now;
            //});
            //feature.FeatureDetails.ForEach(x => _featureDetailTranslationService.InsertRange(x.FeatureDetailTranslations));
            //_featureDetailService.InsertRange(feature.FeatureDetails);
            _featureControlService.InsertRange(feature.FeatureControls);
            _featureTranslationService.InsertRange(feature.FeatureTranslations);
            _featureService.Insert(feature);

            foreach (var restaurantDto in featureDto.Restaurants)
            {
                var restaurant = _restaurantService.Find(restaurantDto.RestaurantId);
                restaurant.Feature = feature;
                _restaurantService.Update(restaurant);
            }

            SaveChanges();
           _manageStorage.UploadImage(path + "\\" + "Feature-" + feature.FeatureId, featureDto.Image, feature.FeatureId.ToString());

        }
        private void ValidateFeature(FeatureDto featureDto, long adminId)
        {
            foreach (var featureName in featureDto.FeatureNameDictionary)
            {
                if (string.IsNullOrEmpty(featureName.Value))
                    throw new ValidationException(ErrorCodes.EmptyFeatureName);
                if (featureName.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.FeatureNameExceedLength);
                if (_featureTranslationService.CheckFeatureNameExist(featureName.Value, featureName.Key, featureDto.FeatureId, adminId)
                ) throw new ValidationException(ErrorCodes.FeatureNameAlreadyExist);

            }
            //foreach (var featureDetail in featureDto.FeatureDetails)
            //{
            //    foreach (var description in featureDetail.DescriptionDictionary)
            //    {
            //        if (string.IsNullOrEmpty(description.Value))
            //            throw new ValidationException(ErrorCodes.EmptyFeatureName);
            //        if (description.Value.Length > 300)
            //            throw new ValidationException(ErrorCodes.FeatureNameExceedLength);
            //        if (_featureDetailTranslationService.CheckFeatureDetailDescriptionExist(description.Value, description.Key, featureDetail.FeatureDetailId, adminId)
            //        ) throw new ValidationException(ErrorCodes.FeatureNameAlreadyExist);

            //    }
            //}
        }

        private void ValidateFeatureDetail(FeatureDetailDto featureDetail, long userId)
        {
            foreach (var description in featureDetail.DescriptionDictionary)
            {
                if (string.IsNullOrEmpty(description.Value))
                    throw new ValidationException(ErrorCodes.EmptyFeatureName);
                if (description.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.FeatureNameExceedLength);
                if (_featureDetailTranslationService.CheckFeatureDetailDescriptionExist(description.Value, description.Key, featureDetail.FeatureDetailId, userId)
                ) throw new ValidationException(ErrorCodes.FeatureNameAlreadyExist);

            }
        }

        public void ActivateFeature(long featureId,long adminId)
        {
            var feature = _featureService.Find(featureId);
            if (feature == null) throw new NotFoundException(ErrorCodes.FeatureNotFound);
            if (feature.Type == Enums.FeatureType.Restaurant && feature.Restaurants.Count(x=>x.IsActive && !x.IsDeleted) <= 0)
            {
                throw new NotFoundException(ErrorCodes.RestaurantIsNotActivated);
            }
            feature.IsActive = true;
            feature.ModifiedBy = adminId;
            feature.ModifyTime = DateTime.Now;
            _featureService.Update(feature);
            SaveChanges();
        }

        public void DeActivateFeature(long featureId, long adminId)
        {
            var feature = _featureService.Find(featureId);
            if (feature == null) throw new NotFoundException(ErrorCodes.FeatureNotFound);
            feature.IsActive = false;
            feature.ModifiedBy = adminId;
            feature.ModifyTime = DateTime.Now;
            _featureService.Update(feature);
            SaveChanges();
        }
        public void DeleteFeature(long featureId, long adminId)
        {
            var feature = _featureService.Find(featureId);
            if (feature == null) throw new NotFoundException(ErrorCodes.FeatureNotFound);
            feature.IsDeleted = true;
            feature.IsActive = false;
            feature.DeletedBy = adminId;
            feature.DeleteTime = DateTime.Now;
            _featureService.Update(feature);
            SaveChanges();
        }

        public void UpdateFeature(FeatureDto featureDto, long adminId, string path)
        {
            ValidateFeature(featureDto, adminId);
            var feature = _featureService.Find(featureDto.FeatureId);
            if (feature == null) throw new NotFoundException(ErrorCodes.FeatureNotFound);
            foreach (var featureName in featureDto.FeatureNameDictionary)
            {
                var featureTranslation =
                    feature.FeatureTranslations.FirstOrDefault(x => x.Language.ToLower() == featureName.Key.ToLower());
                if (featureTranslation == null)
                {
                    feature.FeatureTranslations.Add(new FeatureTranslation
                    {
                        Language = featureName.Key.ToLower(),
                        FeatureName = featureName.Value,
                    });
                }
                else
                {
                    featureTranslation.FeatureName = featureName.Value;
                }
            }
            feature.FeatureControls.ForEach(x => x.IsActive = false);
            _featureControlService.UpdateRange(feature.FeatureControls);
            foreach (var featureControl in featureDto.FeatureControl)
            {
                var controlExist = feature.FeatureControls.FirstOrDefault(x => x.Control == featureControl.Control);
                if (controlExist != null)
                {
                    controlExist.IsActive = true;
                    controlExist.Order = featureControl.Order;
                    _featureControlService.Update(controlExist);
                }
                else
                {
                    var control = new FeatureControl
                    {
                        Control = featureControl.Control,
                        ControlType =
                            featureControl.Control == Enums.Control.ListOfText ||
                            featureControl.Control == Enums.Control.ListOfTextAndImage ||
                            featureControl.Control == Enums.Control.ListOfAvailable
                                ? Enums.ControlType.Single
                                : Enums.ControlType.None,
                        Order = featureControl.Order,
                        IsActive = true
                    };
                    feature.FeatureControls.Add(control);
                    _featureControlService.Insert(control);
                }
            }
            //if (featureDto.HasDetails)
            //{
            //    foreach (var item in featureDto.FeatureDetails)
            //    {
            //        var featureDetail =
            //            feature.FeatureDetails.FirstOrDefault(x => x.FeatureDetailId == item.FeatureDetailId && item.FeatureDetailId > 0);
            //        if (item.IsDeleted)
            //        {
            //            //_featureDetailService.Delete(featureDetail);
            //            if (featureDetail == null) continue;
            //            featureDetail.IsDeleted = true;
            //            featureDetail.DeletedBy = adminId;
            //            featureDetail.DeleteTime = DateTime.Now;
            //            _featureDetailService.Update(featureDetail);
            //        }
            //        else if (featureDetail != null)
            //        {
            //            foreach (var featureDetailName in item.DescriptionDictionary)
            //            {
            //                var featureDetailTranslation =
            //                    featureDetail.FeatureDetailTranslations.FirstOrDefault(
            //                        x => x.Language.ToLower() == featureDetailName.Key.ToLower());
            //                if (featureDetailTranslation == null)
            //                {
            //                    featureDetail.FeatureDetailTranslations.Add(new FeatureDetailTranslation
            //                    {
            //                        Language = featureDetailName.Key.ToLower(),
            //                        Description = featureDetailName.Value,
            //                    });
            //                }
            //                else
            //                {
            //                    featureDetailTranslation.Description = featureDetailName.Value;
            //                }
            //            }
            //            featureDetail.Price = item.Price;
            //            featureDetail.IsFree = item.IsFree;
            //            _featureDetailService.Update(featureDetail);
            //        }
            //        else
            //        {
            //            featureDetail = Mapper.Map<FeatureDetail>(item);
            //            featureDetail.CreationBy = adminId;
            //            featureDetail.CreateTime = DateTime.Now;
            //            featureDetail.FeatureId = feature.FeatureId;

            //            _featureDetailTranslationService.InsertRange(featureDetail.FeatureDetailTranslations);
            //            _featureDetailService.Insert(featureDetail);

            //        }
            //    }
            //}

            //feature.HasDetails = featureDto.HasDetails;
            
            foreach (var featureRestaurant in feature.Restaurants)
            {
                featureRestaurant.FeatureId = null;
                //_restaurantService.Update(featureRestaurant);
            }
            _featureService.Update(feature);
            foreach (var restaurantDto in featureDto.Restaurants)
            {
                var restaurant = _restaurantService.Find(restaurantDto.RestaurantId);
                restaurant.Feature = feature;
                _restaurantService.Update(restaurant);
            }
            SaveChanges();
            if (featureDto.IsImageChange)
                _manageStorage.UploadImage(path + "\\" + "Feature-" + feature.FeatureId, featureDto.Image, feature.FeatureId.ToString());
        }

        public FeatureDto GetFeature(long featureId)
        {
            var feature = _featureService.Find(featureId);
            if (feature == null) throw new NotFoundException(ErrorCodes.FeatureNotFound);
            if (feature.IsDeleted) throw new NotFoundException(ErrorCodes.FeatureNotFound);
            return Mapper.Map<FeatureDto>(feature);
        }

        public FeatureDto CheckFeatureAsRestaurant(long adminId)
        {
            return Mapper.Map<FeatureDto>(_featureService.CheckFeatureAsRestaurant(adminId));
        }
        public FeatureDto CheckFeatureAsInvitation(long adminId)
        {
            return Mapper.Map<FeatureDto>(_featureService.CheckFeatureAsInvitation(adminId));
        }

        public FeatureDto CheckFeatureAsTicket(long adminId)
        {
            return Mapper.Map<FeatureDto>(_featureService.CheckFeatureAsTicket(adminId));
        }
        //public List<ControlDto> GetAllControl()
        //{
        //    return Mapper.Map<List<ControlDto>>(_controlService.Queryable().Select(x=>x).ToList());
        //}

        public List<FeatureDto> GetAllFeatureForSupervisor(long userId)
        {
            var features = Mapper.Map<List<FeatureDto>>(_supervisorFeatureService.Query(x => x.SupervisorId == userId ).Select(x => x.Feature).ToList());

            return features;
        }
        public List<FeatureNameDto> GetAllFeatureName(long userId,string userRole)
        {
            List<FeatureNameDto> features = null;
            if (userRole == Enums.RoleType.Admin.ToString())
            {
                features = Mapper.Map<List<FeatureNameDto>>(_featureService.Query(x => x.CreationBy == userId && !x.IsDeleted && x.IsActive).Select().ToList());
            }
            else if (userRole == Enums.RoleType.Supervisor.ToString())
            {
                features = Mapper.Map<List<FeatureNameDto>>(_supervisorFeatureService.Query(x => x.SupervisorId == userId && !x.Feature.IsDeleted && x.Feature.IsActive).Select(x => x.Feature).ToList());
            } 
            else if (userRole == Enums.RoleType.Receptionist.ToString())
            {
                var adminId = _receptionistService.Find(userId).AdminId;
                features = Mapper.Map<List<FeatureNameDto>>(_featureService.Query(x => x.CreationBy == adminId && !x.IsDeleted && x.IsActive).Select().ToList());
            }

            return features;
        }
        public PagedResultsDto GeFeatureDetails(long featureControlId,int page,int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _featureDetailService.Query(x => !x.IsDeleted && x.FeatureControlId == featureControlId).Select().Count();
            results.Data = Mapper.Map<List<FeatureDetailDto>>(_featureDetailService
                .Query(x => !x.IsDeleted && x.FeatureControlId == featureControlId).Select()
                .OrderBy(x => x.FeatureControlId).Skip((page - 1) * pageSize).Take(pageSize).ToList());
            return results;
        }

        public void AddFeatureDetail(FeatureDetailDto featureDetailDto, long adminId, string path)
        {
            var user = _userService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            ValidateFeatureDetail(featureDetailDto, adminId);

            var featureDetail = Mapper.Map<FeatureDetail>(featureDetailDto);
            featureDetail.CreationBy = adminId;
            featureDetail.CreateTime = DateTime.Now;

            //foreach (var featureName in featureDetailDto.DescriptionDictionary)
            //{
            //    featureDetail.FeatureTranslations.Add(new FeatureTranslation
            //    {
            //        FeatureName = featureName.Value,
            //        Language = featureName.Key.ToLower()
            //    });
            //}
            //featureDetail.FeatureDetails.ForEach(x => _featureDetailTranslationService.InsertRange(x.FeatureDetailTranslations));
            _availableService.InsertRange(featureDetail.Availables);
            _featureDetailTranslationService.InsertRange(featureDetail.FeatureDetailTranslations);
            _featureDetailService.Insert(featureDetail);


            SaveChanges();
            if (featureDetailDto.IsImageChange)
            {
                var featureControl = _featureControlService.Find(featureDetail.FeatureControlId);
                _manageStorage.UploadImage(path + "\\" + "Feature-" + featureControl.FeatureId + "\\Detail-" + featureDetail.FeatureDetailId, featureDetailDto.Image, featureDetail.FeatureDetailId.ToString());
            }


        }
        public void UpdateFeatureDetail(FeatureDetailDto featureDetailDto, long adminId, string path)
        {
            var user = _userService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            var featureDetail = _featureDetailService.Find(featureDetailDto.FeatureDetailId);
            ValidateFeatureDetail(featureDetailDto, adminId);

            //var featureDetail = Mapper.Map<FeatureDetail>(featureDetailDto);
            featureDetail.ModifiedBy = adminId;
            featureDetail.ModifyTime = DateTime.Now;
            featureDetail.Link = featureDetailDto.Link;
            featureDetail.IsFree = featureDetailDto.IsFree;
            featureDetail.Price = featureDetailDto.Price;
            //featureDetail.Availables = Mapper.Map<List<Available>>(featureDetailDto.Availables);

            foreach (var featureName in featureDetailDto.DescriptionDictionary)
            {
                var featureDetailTranslation=
                    featureDetail.FeatureDetailTranslations.FirstOrDefault(x => x.Language.ToLower() == featureName.Key.ToLower());
                if (featureDetailTranslation == null)
                {
                    featureDetail.FeatureDetailTranslations.Add(new FeatureDetailTranslation
                    {
                        Language = featureName.Key.ToLower(),
                        Description = featureName.Value,
                    });
                }
                else
                {
                    featureDetailTranslation.Description = featureName.Value;
                }
            }
            featureDetail.Availables.ForEach(x=>x.IsDeleted = true);
            foreach (var availableDto in featureDetailDto.Availables)
            {
                var available =
                    featureDetail.Availables.FirstOrDefault(x => x.AvailableId == availableDto.AvailableId && availableDto.AvailableId > 0);
                if (available == null)
                {
                    available = new Available
                    {
                        From = availableDto.From,
                        To = availableDto.To,
                        Max = availableDto.Max,
                        Day = availableDto.Day,
                        IsDeleted = false
                    };
                    featureDetail.Availables.Add(available);
                    //_availableService.Insert(available);
                }
                else
                {
                    available.From = availableDto.From;
                    available.To = availableDto.To;
                    available.Max = availableDto.Max;
                    available.IsDeleted = false;
                    //_availableService.Update(available);
                }
            }
            //_featureDetailTranslationService.InsertRange(featureDetail.FeatureDetailTranslations);
            //_availableService.InsertOrUpdateRange(featureDetail.Availables);
            _featureDetailService.Update(featureDetail);


            SaveChanges();
            if (featureDetailDto.IsImageChange)
            {
                var featureControl = _featureControlService.Find(featureDetail.FeatureControlId);
                _manageStorage.UploadImage(path + "\\" + "Feature-" + featureControl.FeatureId + "\\Detail-" + featureDetail.FeatureDetailId, featureDetailDto.Image, featureDetail.FeatureDetailId.ToString());
            }
        }
        public void DeleteFeatureDetail(long featureDetailId, long userId)
        {
            var featureDetail =_featureDetailService.Find(featureDetailId);
            featureDetail.IsDeleted = true;
            featureDetail.DeletedBy = userId;
            featureDetail.DeleteTime = DateTime.Now;
            _featureDetailService.Update(featureDetail);
            SaveChanges();
        }

        public void SwitchFeatureControlType(long featureControlId)
        {
            var featureControl = _featureControlService.Find(featureControlId);
            featureControl.ControlType = featureControl.ControlType == Enums.ControlType.Single
                ? Enums.ControlType.Multiple
                : Enums.ControlType.Single;
            _featureControlService.Update(featureControl);
            SaveChanges();
        }

        public FeatureInfoDto GetAllFeatureControlDetailDtos(long featureId)
        {
            var featureControls = Mapper.Map<FeatureInfoDto>(_featureService.Find(featureId));
            return featureControls;
        }

        public void AddFeatureDetailAvailability(FeatureDetailDto featureDetailDto, long adminId)
        {
            var user = _userService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            ValidateFeatureDetail(featureDetailDto, adminId);
            var featureDetail = _featureDetailService.Find(featureDetailDto.FeatureDetailId);
            var available = Mapper.Map<Available>(featureDetailDto.Availables.FirstOrDefault());
            if (featureDetail != null)
            {
                featureDetail.ModifiedBy = adminId;
                featureDetail.ModifyTime = DateTime.Now;
                _availableService.Insert(available);
            }
            else
            {
                featureDetail = Mapper.Map<FeatureDetail>(featureDetailDto);
                featureDetail.CreationBy = adminId;
                featureDetail.CreateTime = DateTime.Now;
                
                _featureDetailTranslationService.InsertRange(featureDetail.FeatureDetailTranslations);
                _featureDetailService.Insert(featureDetail); 
            }
            

            SaveChanges();
        }

        public void UpdateFeatureDetailAvailability(FeatureDetailDto featureDetailDto, long adminId)
        {
            var user = _userService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            var featureDetail = _featureDetailService.Find(featureDetailDto.FeatureDetailId);
            ValidateFeatureDetail(featureDetailDto, adminId);

            //var featureDetail = Mapper.Map<FeatureDetail>(featureDetailDto);
            featureDetail.ModifiedBy = adminId;
            featureDetail.ModifyTime = DateTime.Now;

            foreach (var availableDto in featureDetailDto.Availables)
            {
                var available =
                    featureDetail.Availables.FirstOrDefault(x => x.AvailableId == availableDto.AvailableId);
                if (available == null)
                {
                    featureDetail.Availables.Add(new Available
                    {
                        Day = availableDto.Day,
                        From = availableDto.From,
                        To = availableDto.To,
                        Max = availableDto.Max
                });
                }
                else
                {
                    available.Day  = availableDto.Day;
                    available.From = availableDto.From;
                    available.To = availableDto.To;
                    available.Max = availableDto.Max;
                }
            }
            _featureDetailService.Update(featureDetail);


            SaveChanges();
           
        }
        public void DeleteAvailable(long avialableId, long userId)
        {
            var available = _availableService.Find(avialableId);
            available.IsDeleted = true;
            _availableService.Update(available);
            SaveChanges();
        }

        
    }
}
