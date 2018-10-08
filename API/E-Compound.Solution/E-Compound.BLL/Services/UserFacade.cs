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
using E_Compound.Common;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
using Newtonsoft.Json;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class UserFacade:BaseFacade,IUserFacade
    {
        private IUserService _UserService;
        private ISupervisorService _supervisorService;
        private IReceptionistService _receptionistService;
        private ISupervisorFeatureService _supervisorFeatureService;
        private IAdminService _adminService;
        private IPackageService _packageService;
        private IRestaurantService _restaurantService;
        private IRestaurantWaiterService _restaurantWaiterService;
        private IRoomService _roomService;
        private IUserCategoryService _userCategoryService;
        private ITechnicianService _technicianService;
        private ITechnicianCategoryService _technicianCategoryService;


        public UserFacade(IUnitOfWorkAsync unitOFWork, ITechnicianCategoryService technicianCategoryService, ITechnicianService technicianService, IUserCategoryService userCategoryService, IUserService userService, ISupervisorService supervisorService,
            IReceptionistService receptionistService, ISupervisorFeatureService supervisorFeatureService, IAdminService adminService, IPackageService packageService, IRestaurantService restaurantService, IRestaurantWaiterService restaurantWaiterService, IRoomService roomService) : base(unitOFWork)
        {
            _UserService = userService;
            _supervisorService = supervisorService;
            _receptionistService = receptionistService;
            _supervisorFeatureService = supervisorFeatureService;
            _adminService = adminService;
            _packageService = packageService;
            _restaurantService = restaurantService;
            _restaurantWaiterService = restaurantWaiterService;
            _roomService = roomService;
            _userCategoryService = userCategoryService;
            _technicianService = technicianService;
            _technicianCategoryService = technicianCategoryService;
        }

        public UserDto ValidateUser(string email, string password)
        {
            string encryptedPassword = PasswordHelper.Encrypt(password);
            var dd = _UserService.ValidateUser(email, encryptedPassword);
            var user = Mapper.Map<UserDto>(_UserService.ValidateUser(email, encryptedPassword));
            var user1 =   Mapper.Map<UserDto>(_UserService.CheckUserIsDeleted(email, encryptedPassword));
           // var user = Mapper.Map<UserDto>(_UserService.ValidateUser(email, encryptedPassword)) ?? Mapper.Map<UserDto>(_UserService.CheckUserIsDeleted(email, encryptedPassword));
            //var user = Mapper.Map<UserDto>(_UserService.ValidateUser(email, encryptedPassword));
            if (user == null) throw new ValidationException(ErrorCodes.UserNotFound);

            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            if (user.Role == Enums.RoleType.Room)
            {
                var room = _roomService.Find(user.UserId);
                if (DateTime.Now.Date > room.Package.End.Date) throw new ValidationException(ErrorCodes.PackageExpired);
                if (DateTime.Now.Date < room.Package.Start.Date) throw new ValidationException(ErrorCodes.PackageNotActivated);
            }
            return user;

        }
        public UserDto GetUser(long UserId)
        {
            return Mapper.Map<UserDto>(_UserService.Find(UserId));
        }

        public PagedResultsDto GetAllUsers(long adminId, int page, int pageSize,Enums.RoleType role)
        {
            var user = _UserService.Find(adminId);
            if(user == null) throw new ValidationException(ErrorCodes.UserNotFound);
            switch (role)
            {
                case Enums.RoleType.Supervisor:
                    return GetAllSupervisor(adminId, page, pageSize);
                case Enums.RoleType.Receptionist:
                    return GetAllReceptionist(adminId, page, pageSize);
            }
            return null;
        }


        #region Manage Supervisor
        private PagedResultsDto GetAllSupervisor(long adminId, int page, int pageSize)
        {
            var supervisorCount = _supervisorService.Query(x => !x.IsDeleted && x.AdminId == adminId).Select().Count();
            var supervisors = Mapper.Map<List<SupervisorDto>>(_supervisorService.GetAllSupervisors(adminId, page, pageSize));
            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = supervisorCount,
                Data = supervisors
            };
            return results;
        }

        public SupervisorDto GetSupervisor(long supervisorId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            if (supervisor.IsDeleted) throw new NotFoundException(ErrorCodes.UserNotFound);
            return Mapper.Map<SupervisorDto>(supervisor);

        }
        public void AddSupervisor(SupervisorDto supervisorDto, long adminId)
        {
            ValidateSupervisor(supervisorDto, adminId);
            var user = _UserService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            Supervisor supervisor = Mapper.Map<Supervisor>(supervisorDto);
            supervisor.AdminId = adminId;
            supervisor.Password = PasswordHelper.Encrypt(supervisorDto.Password);
            supervisor.Role = Enums.RoleType.Supervisor;
            supervisor.IsActive = true;

            foreach (var feature in supervisorDto.Features)
            {
                supervisor.SupervisorFeatures.Add(new SupervisorFeature
                {
                    FeatureId = feature.FeatureId
                });
            }

            _supervisorFeatureService.InsertRange(supervisor.SupervisorFeatures);
            _supervisorService.Insert(supervisor);
            SaveChanges();

        }
        public void UpdateSupervisor(SupervisorDto supervisorDto, long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorDto.UserId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            ValidateSupervisor(supervisorDto, adminId);
            supervisor.UserName = supervisorDto.UserName;
            supervisor.Password = PasswordHelper.Encrypt(supervisorDto.Password);

            SupervisorFeature[] features =new SupervisorFeature[supervisor.SupervisorFeatures.Count] ;
            supervisor.SupervisorFeatures.CopyTo(features,0);
            _supervisorFeatureService.DeleteRange(features.ToList());

            foreach (var feature in supervisorDto.Features)
            {
                supervisor.SupervisorFeatures.Add(new SupervisorFeature
                {
                    FeatureId = feature.FeatureId
                });
            }
            _supervisorService.Update(supervisor);
            SaveChanges();
        }
        public void ActivateSupervisor(long supervisorId, long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            supervisor.IsActive = true;
            _supervisorService.Update(supervisor);
            SaveChanges();
        }

        public void DeActivateSupervisor(long supervisorId, long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            supervisor.IsActive = false;
            _supervisorService.Update(supervisor);
            SaveChanges();
        }
        public void DeleteSupervisor(long supervisorId,  long adminId)
        {
            var supervisor = _supervisorService.Find(supervisorId);
            if (supervisor == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            supervisor.IsDeleted = true;
            supervisor.IsActive = false;
            _supervisorService.Update(supervisor);
            SaveChanges();
        }


        private void ValidateSupervisor(SupervisorDto supervisorDto, long adminId)
        {
            if (string.IsNullOrEmpty(supervisorDto.UserName)) throw new ValidationException(ErrorCodes.EmptyUserName);
            if (supervisorDto.UserName.Length > 100) throw new ValidationException(ErrorCodes.NameExceedLength);
            if (string.IsNullOrEmpty(supervisorDto.Password)) throw new ValidationException(ErrorCodes.EmptyPassword);
            if (supervisorDto.Password.Length < 8 || supervisorDto.Password.Length > 25) throw new ValidationException(ErrorCodes.PasswordLengthNotMatched);

            if (_supervisorService.CheckUserNameDuplicated(supervisorDto.UserName, supervisorDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
            if (_receptionistService.CheckUserNameDuplicated(supervisorDto.UserName, supervisorDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
            if (_roomService.CheckUserNameDuplicated(supervisorDto.UserName, supervisorDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
        }


        #endregion

        #region Manage Technician

        private PagedResultsDto GetAllTechnician(long adminId, int page, int pageSize)
        {
            var technicianCount = _technicianService.Query(x => !x.IsDeleted && x.AdminId == adminId).Select().Count();
            var technicians = Mapper.Map<List<TechnicianDto>>(_technicianService.GetAllTechnicians(adminId, page, pageSize));
            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = technicianCount,
                Data = technicians
            };
            return results;
        }

        public TechnicianDto GetTechnician(long technicianId)
        {
            var technician = _technicianService.Find(technicianId);
            if (technician == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            if (technician.IsDeleted) throw new NotFoundException(ErrorCodes.UserNotFound);
            return Mapper.Map<TechnicianDto>(technician);

        }
        public void AddTechnician(TechnicianDto technicianDto, long adminId)
        {
            ValidateTechnician(technicianDto, adminId);
            var user = _UserService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            Technician technician = Mapper.Map<Technician>(technicianDto);
            technician.AdminId = adminId;
            technician.Password = PasswordHelper.Encrypt(technicianDto.Password);
            technician.Role = Enums.RoleType.Technician;
            technician.IsActive = true;

            foreach (var category in technicianDto.UserCategories)
            {
                technician.TechnicianCategories.Add(new TechnicianCategory
                {
                    UserCategoryId = category.UserCategoryId
                });
            }

            _technicianCategoryService.InsertRange(technician.TechnicianCategories);
            _technicianService.Insert(technician);
            SaveChanges();
        }

        public void UpdateTechnician(TechnicianDto technicianDto, long adminId)
        {
            var technician = _technicianService.Find(technicianDto.UserId);
            if (technician == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            ValidateTechnician(technicianDto, adminId);
            technician.UserName = technicianDto.UserName;
            technician.Password = PasswordHelper.Encrypt(technicianDto.Password);

            TechnicianCategory[] categories = new TechnicianCategory[technician.TechnicianCategories.Count];
            technician.TechnicianCategories.CopyTo(categories, 0);
            _technicianCategoryService.DeleteRange(categories.ToList());

            foreach (var feature in technicianDto.UserCategories)
            {
                technician.TechnicianCategories.Add(new TechnicianCategory
                {
                    UserCategoryId = feature.UserCategoryId
                });
            }
            _technicianService.Update(technician);
            SaveChanges();
        }
        public void ActivateTechnician(long technicianId, long adminId)
        {
            var technician = _technicianService.Find(technicianId);
            if (technician == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            technician.IsActive = true;
            _technicianService.Update(technician);
            SaveChanges();
        }

        public void DeActivateTechnician(long technicianId, long adminId)
        {
            var technician = _technicianService.Find(technicianId);
            if (technician == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            technician.IsActive = false;
            _technicianService.Update(technician);
            SaveChanges();
        }
        public void DeleteTechnician(long technicianId, long adminId)
        {
            var technician = _technicianService.Find(technicianId);
            if (technician == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            technician.IsDeleted = true;
            technician.IsActive = false;
            _technicianService.Update(technician);
            SaveChanges();
        }

        private void ValidateTechnician(TechnicianDto technicianDto, long adminId)
        {
            if (string.IsNullOrEmpty(technicianDto.UserName)) throw new ValidationException(ErrorCodes.EmptyUserName);
            if (technicianDto.UserName.Length > 100) throw new ValidationException(ErrorCodes.NameExceedLength);
            if (string.IsNullOrEmpty(technicianDto.Password)) throw new ValidationException(ErrorCodes.EmptyPassword);
            if (technicianDto.Password.Length < 8 || technicianDto.Password.Length > 25) throw new ValidationException(ErrorCodes.PasswordLengthNotMatched);

            if (_technicianService.CheckUserNameDuplicated(technicianDto.UserName, technicianDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
            if (_receptionistService.CheckUserNameDuplicated(technicianDto.UserName, technicianDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
            if (_roomService.CheckUserNameDuplicated(technicianDto.UserName, technicianDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
        }
        #endregion

        #region Manage receptionist
        private PagedResultsDto GetAllReceptionist(long adminId, int page, int pageSize)
        {
            var receptionistCount = _receptionistService.Query(x => !x.IsDeleted && x.AdminId == adminId).Select().Count();
            var receptionists = Mapper.Map<List<ReceptionistDto>>(_receptionistService.GetAllReceptionists(adminId, page, pageSize));
            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = receptionistCount,
                Data = receptionists
            };
            return results;
        }

        public ReceptionistDto GetReceptionist(long receptionistId)
        {
            var receptionist = _receptionistService.Find(receptionistId);
            if (receptionist == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            if (receptionist.IsDeleted) throw new NotFoundException(ErrorCodes.UserNotFound);
            return Mapper.Map<ReceptionistDto>(receptionist);
           
        }
        public void AddReceptionist(ReceptionistDto receptionistDto, long adminId)
        {
            ValidateReceptionist(receptionistDto, adminId);
            var user = _UserService.Find(adminId);
            if (user == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            
            Receptionist receptionist = Mapper.Map<Receptionist>(receptionistDto);
            receptionist.AdminId = adminId;
            receptionist.Password = PasswordHelper.Encrypt(receptionistDto.Password);
            receptionist.Role = Enums.RoleType.Receptionist;
            receptionist.IsActive = true;

            _receptionistService.Insert(receptionist);
            SaveChanges();
            
        }
        public void UpdateReceptionist(ReceptionistDto receptionistDto, long adminId)
        {
            var receptionist = _receptionistService.Find(receptionistDto.UserId);
            if (receptionist == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            ValidateReceptionist(receptionistDto, adminId);
            receptionist.UserName = receptionistDto.UserName;
            receptionist.Password = PasswordHelper.Encrypt(receptionistDto.Password);
            _receptionistService.Update(receptionist);
            SaveChanges();
        }
        public void ActivateReceptionist(long receptionistId, long adminId)
        {
            var receptionist = _receptionistService.Find(receptionistId);
            if (receptionist == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            receptionist.IsActive = true;
            _receptionistService.Update(receptionist);
            SaveChanges();
        }

        public void DeActivateReceptionist(long receptionistId, long adminId)
        {
            var receptionist = _receptionistService.Find(receptionistId);
            if (receptionist == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            receptionist.IsActive = false;
            _receptionistService.Update(receptionist);
            SaveChanges();
        }
        public void DeleteReceptionist(long receptionistId, long adminId)
        {
            var receptionist = _receptionistService.Find(receptionistId);
            if (receptionist == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            receptionist.IsDeleted = true;
            receptionist.IsActive = false;
            _receptionistService.Update(receptionist);
            SaveChanges();
        }


        private void ValidateReceptionist(ReceptionistDto receptionistDto, long adminId)
        {
            if (string.IsNullOrEmpty(receptionistDto.UserName)) throw new ValidationException(ErrorCodes.EmptyUserName);
            if (receptionistDto.UserName.Length > 100) throw new ValidationException(ErrorCodes.NameExceedLength);
            if (string.IsNullOrEmpty(receptionistDto.Password)) throw new ValidationException(ErrorCodes.EmptyPassword);
            if (receptionistDto.Password.Length < 8 || receptionistDto.Password.Length > 25) throw new ValidationException(ErrorCodes.PasswordLengthNotMatched);

            if (_supervisorService.CheckUserNameDuplicated(receptionistDto.UserName, receptionistDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
            if (_receptionistService.CheckUserNameDuplicated(receptionistDto.UserName, receptionistDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
            if (_roomService.CheckUserNameDuplicated(receptionistDto.UserName, receptionistDto.UserId, adminId)) throw new ValidationException(ErrorCodes.UserNameAlreadyExist);
        }

        #endregion

        #region integration with subscription module
        public void AddNewGlobalUser(AdminDto adminDto)
        {
            var admin = _adminService.GetAdminByAccountId(adminDto.UserAccountId);
            if (admin == null)
            {
                Admin newAdmin = new Admin();
                newAdmin.UserName = adminDto.UserName;
                newAdmin.UserAccountId = adminDto.UserAccountId;
                newAdmin.Role = Enums.RoleType.Admin;
                newAdmin.Password = adminDto.Password;
                newAdmin.IsActive = adminDto.IsActive;
                newAdmin.FeaturesBackgroundId = Strings.FeaturesBackgroundId;
                //newAdmin.ProductId = adminDto.ProductId;
                _adminService.Insert(newAdmin);
                admin = newAdmin;
            }
            else
            {
                admin.UserName = adminDto.UserName;
                admin.Password = adminDto.Password;
                admin.IsActive = adminDto.IsActive;
                //admin.ProductId = adminDto.ProductId;

                _adminService.Update(admin);
            }
            SaveChanges();
            var package = admin.Packages.FirstOrDefault(x => x.PackageGuid == adminDto.PackageGuid);
            if (package == null)
            {
                admin.Packages.Add(new Package
                {
                    End = adminDto.End,
                    Start = adminDto.Start,
                    MaxNumberOfRooms = adminDto.Limit,
                    PackageGuid = adminDto.PackageGuid,
                    AdminId = admin.UserId
                });
                _packageService.InsertRange(admin.Packages);
            }
            else
            {
                package.End = adminDto.End;
                package.Start = adminDto.Start;
                _packageService.Update(package);
            }

            _adminService.Update(admin);
            SaveChanges();
        }

        public void UpdateGlobalUser(AdminDto adminDto)
        {
            var admin = _adminService.GetAdminByAccountId(adminDto.UserAccountId);
            admin.UserName = adminDto.UserName;
            admin.Password = adminDto.Password;
            admin.IsActive = adminDto.IsActive;

            _adminService.Update(admin);
            SaveChanges();
        }

        public void UpdateAdminPackage(AdminDto adminDto)
        {
            var admin = _adminService.GetAdminByAccountId(adminDto.UserAccountId);
            var package = admin.Packages.FirstOrDefault(x => x.PackageGuid == adminDto.PackageGuid);
            if (package == null)
            {
                admin.Packages.Add(new Package
                {
                    End = adminDto.End,
                    Start = adminDto.Start,
                    MaxNumberOfRooms= adminDto.Limit,
                    PackageGuid = adminDto.PackageGuid,
                    AdminId = admin.UserId
                });
                _packageService.InsertRange(admin.Packages);
            }
            else
            {
                package.End = adminDto.End;
                package.Start = adminDto.Start;
                _packageService.Update(package);
            }
            //admin.ProductId = adminDto.ProductId;

            _adminService.Update(admin);
            SaveChanges();
        }


        public UserConsumed GetMaxAndConsumedUsers(long userId)
        {
            var maxNum = _packageService.GetRoomsCountByAdminId(userId);

            var consumedUsers = _adminService.Find(userId).Rooms.Count(x=>!x.IsDeleted);

            UserConsumed MaxCon = new UserConsumed();
            MaxCon.MaxNumUsers = maxNum;
            MaxCon.ConsumedUsers = consumedUsers;


            return MaxCon;
        }



        #endregion

        #region Restaurant waiter
        public void AddRestaurantWaiter(RestaurantWaiterDTO restaurantWaiterDto, long restaurantAdminId)
        {
            ValidateRestaurantWaiter(restaurantWaiterDto, 0);
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            //var consumedWaiters = restaurant.GlobalAdmin.Restaurants.Where(x => !x.IsDeleted).Select(x => x.WaitersLimit).Sum();
            // var consumedWaiters = _packageService.Query(x => x.GlobalAdminId == restaurant.GlobalAdminId).Select(x => x.Waiters.Count(w=>!w.IsDeleted)).Sum();
            //Package package;

            //var packages = _packageService.Query(x => x.AdminId == restaurant.GlobalAdminId).Include(x => x.Waiters).Select().ToList();
            //package = packages.OrderBy(x => x.Start).FirstOrDefault();
            //int count = 1;
            //while (true)
            //{
            //    if (package.MaxNumberOfWaiters > package.Waiters.Count(x => !x.IsDeleted))
            //    {
            //        break;
            //    }
            //    //else
            //    //{
            //    //    consumedWaiters = consumedWaiters - package.MaxNumberOfWaiters;
            //    //}

            //    package = packages.OrderBy(x => x.Start).Skip(count).FirstOrDefault();
            //    count++;
            //}
            ///var packages = restaurant.GlobalAdmin.Packages;
            RestaurantWaiter restaurantWaiter = Mapper.Map<RestaurantWaiter>(restaurantWaiterDto);
            restaurantWaiter.RestaurantId = restaurant.RestaurantId;
            restaurantWaiter.Password = PasswordHelper.Encrypt(restaurantWaiterDto.Password);
            restaurantWaiter.Role = Enums.RoleType.Waiter;
            restaurantWaiter.IsActive = true;
            //restaurantWaiter.PackageId = package.PackageId;
            _restaurantWaiterService.Insert(restaurantWaiter);
            SaveChanges();
            //UpdateSubscription(restaurant.GlobalAdminId, package.PackageGuid, package.Waiters.Count(x => !x.IsDeleted));
        }

        public RestaurantWaiterDTO GetRestaurantWaiter(long waiterId)
        {
            var waiter = _restaurantWaiterService.Find(waiterId);
            if (waiter == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            return Mapper.Map<RestaurantWaiterDTO>(waiter);
        }

        public PagedResultsDto GetAllRestaurantWaiters(long restaurantAdminId, int page, int pageSize, string language)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            return _restaurantWaiterService.GetAllRestaurantWaiters(restaurant.RestaurantId, page, pageSize, language);
        }
        public void UpdateRestaurantWaiter(RestaurantWaiterDTO restaurantWaiterDto)
        {
            var restaurantWaiter = _restaurantWaiterService.Find(restaurantWaiterDto.UserId);
            if (restaurantWaiter == null) throw new NotFoundException(ErrorCodes.UserNotFound);

            ValidateRestaurantWaiter(restaurantWaiterDto, restaurantWaiter.RestaurantId);
            restaurantWaiter.Name = restaurantWaiterDto.Name;
            restaurantWaiter.UserName = restaurantWaiterDto.UserName;
            restaurantWaiter.Password = PasswordHelper.Encrypt(restaurantWaiterDto.Password);
            restaurantWaiter.BranchId = restaurantWaiterDto.BranchId;
            _restaurantWaiterService.Update(restaurantWaiter);
            SaveChanges();
        }
        private void ValidateRestaurantWaiter(RestaurantWaiterDTO restaurantWaiterDto, long restaurantId)
        {
            if (string.IsNullOrEmpty(restaurantWaiterDto.Name)) throw new ValidationException(ErrorCodes.EmptyRestaurantWaiterUserName);
            if (restaurantWaiterDto.Name.Length > 100) throw new ValidationException(ErrorCodes.RestaurantWaiterNameExceedLength);
            if (string.IsNullOrEmpty(restaurantWaiterDto.UserName)) throw new ValidationException(ErrorCodes.EmptyRestaurantWaiterUserName);
            if (restaurantWaiterDto.UserName.Length > 100) throw new ValidationException(ErrorCodes.RestaurantWaiterNameExceedLength);
            if (string.IsNullOrEmpty(restaurantWaiterDto.Password)) throw new ValidationException(ErrorCodes.EmptyRestaurantAdminPassword);
            if (restaurantWaiterDto.Password.Length < 8 || restaurantWaiterDto.Password.Length > 25) throw new ValidationException(ErrorCodes.RestaurantAdminPasswordLengthNotMatched);
            if (_restaurantWaiterService.CheckUserNameDuplicated(restaurantWaiterDto.UserName, restaurantId)) throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
            if (_UserService.CheckUserNameDuplicated(restaurantWaiterDto.UserName,restaurantWaiterDto.UserId)) throw new ValidationException(ErrorCodes.RestaurantAdminUserNameAlreadyExist);
        }
        public void DeleteRestaurantWaiter(long restaurantWaiterId)
        {
            var restaurantWaiter = _restaurantWaiterService.Find(restaurantWaiterId);
            if (restaurantWaiter == null) throw new NotFoundException(ErrorCodes.UserNotFound);
            restaurantWaiter.IsDeleted = true;
            _restaurantWaiterService.Update(restaurantWaiter);
            //var package = _packageService.Query(x => x.PackageId == restaurantWaiter.PackageId).Include(x => x.Waiters)
            //    .Select().FirstOrDefault();
            SaveChanges();
            //UpdateSubscription(package.GlobalAdminId, package.PackageGuid, package.Waiters.Count(x => !x.IsDeleted));
        }


        #endregion
    }
}
