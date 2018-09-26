using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.BLL.Services.Interfaces;
using E_Compound.Common.CustomException;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace E_Compound.BLL.Services
{
    public class BranchFacade:BaseFacade,IBranchFacade
    {
        private IBranchService _branchService;
        private IBranchTranslationService _branchTranslationService;
        private IRestaurantService _restaurantService;
        public BranchFacade(IBranchService branchService, IBranchTranslationService branchTranslationService, IRestaurantService restaurantService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _branchService = branchService;
            _branchTranslationService = branchTranslationService;
            _restaurantService = restaurantService;
        }

        public void AddBranch(BranchDto branchDto, long restaurantAdminId)
        {
            var restaurant = _restaurantService.GetRestaurantByAdminId(restaurantAdminId);
            if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
            if (restaurant.IsDeleted) throw new ValidationException(ErrorCodes.RestaurantDeleted);
            ValidateBranch(branchDto, restaurantAdminId);

            var branch = Mapper.Map<Branch>(branchDto);
            branch.RestaurantId = restaurant.RestaurantId;
            foreach (var branchTitle in branchDto.BranchTitleDictionary)
            {
                branch.BranchTranslations.Add(new BranchTranslation
                {
                    BranchTitle = branchTitle.Value,
                    BranchAddress = branchDto.BranchAddressDictionary[branchTitle.Key],
                    Language = branchTitle.Key.ToLower()
                });
            }
            _branchTranslationService.InsertRange(branch.BranchTranslations);
            _branchService.Insert(branch);
            SaveChanges();
            
        }

        private void ValidateBranch(BranchDto branchDto, long restaurantAdminId)
        {
            foreach (var branchTitle in branchDto.BranchTitleDictionary)
            {
                if (string.IsNullOrEmpty(branchTitle.Value))
                    throw new ValidationException(ErrorCodes.EmptyBranchTitle);
                if (branchTitle.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.BranchTiteExceedLength);
                if (_branchTranslationService.CheckBranchTitleExist(branchTitle.Value, branchTitle.Key, branchDto.BranchId, restaurantAdminId)
                ) throw new ValidationException(ErrorCodes.BranchTitleAlreadyExist);
            }
            foreach (var branchAddress in branchDto.BranchAddressDictionary)
            {
                if (string.IsNullOrEmpty(branchAddress.Value))
                    throw new ValidationException(ErrorCodes.EmptyBranchAddress);
                if (branchAddress.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.BranchAddressExceedLength);
            }
        }
        public BranchDto GetBranch(long branchId)
        {
            var branch = _branchService.Find(branchId);
            if (branch == null) throw new NotFoundException(ErrorCodes.BranchNotFound);
            if (branch.IsDeleted) throw new NotFoundException(ErrorCodes.BranchDeleted);
            //return Mapper.Map<Branch,BranchDto>(branch, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.BranchTranslations = src.BranchTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower())
            //                .ToList();
            //        }
            //    );
            //});
            return Mapper.Map<BranchDto>(branch);
        }

        public void ActivateBranch(long branchId)
        {
            var branch = _branchService.Find(branchId);
            if (branch == null) throw new NotFoundException(ErrorCodes.BranchNotFound);
            branch.IsActive = true;
            _branchService.Update(branch);
            SaveChanges();
        }

        public void DeActivateBranch(long branchId)
        {
            var branch = _branchService.Find(branchId);
            if (branch == null) throw new NotFoundException(ErrorCodes.BranchNotFound);
            branch.IsActive = false;
            _branchService.Update(branch);
            SaveChanges();
        }
        public void DeleteBranch(long branchId)
        {
            var branch = _branchService.Find(branchId);
            if (branch == null) throw new NotFoundException(ErrorCodes.BranchNotFound);
            branch.IsDeleted = true;
            branch.IsActive = false;
            _branchService.Update(branch);
            SaveChanges();
        }
        public void UpdateBranch(BranchDto branchDto,long restaurantAdminId)
        {
            ValidateBranch(branchDto, restaurantAdminId);
            var branch = _branchService.Find(branchDto.BranchId);
            if (branch == null) throw new NotFoundException(ErrorCodes.BranchNotFound);
            foreach (var branchTitle in branchDto.BranchTitleDictionary)
            {
                var branchTranslation =
                    branch.BranchTranslations.FirstOrDefault(x => x.Language.ToLower() == branchTitle.Key.ToLower());
                if (branchTranslation == null)
                {
                    branch.BranchTranslations.Add(new BranchTranslation
                    {
                        Language = branchTitle.Key.ToLower(),
                        BranchTitle = branchTitle.Value,
                        BranchAddress = branchDto.BranchAddressDictionary[branchTitle.Key]
                    });
                }
                else
                {
                    branchTranslation.BranchTitle = branchTitle.Value;
                    branchTranslation.BranchAddress = branchDto.BranchAddressDictionary[branchTitle.Key];
                }
            }
            _branchService.Update(branch);
            SaveChanges();
        }
        public PagedResultsDto GetAllBranches(string language, long restaurantAdminId, int page, int pageSize)
        {
            var results = _branchTranslationService.GetAllBranchesByRestaurantAdminId(language, restaurantAdminId, page, pageSize);
            return results;
        }
    }
}
