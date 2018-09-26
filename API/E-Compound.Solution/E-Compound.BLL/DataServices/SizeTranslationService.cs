using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Compound.BLL.DataServices.Interfaces;
using E_Compound.BLL.DTOs;
using E_Compound.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace E_Compound.BLL.DataServices
{
    public class SizeTranslationService:Service<SizeTranslation>,ISizeTranslationService
    {
        public SizeTranslationService(IRepositoryAsync<SizeTranslation> repository):base(repository)
        {
            
        }
        public PagedResultsDto GetAllSizes(string language,long userId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Size.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Size.Restaurant.RestaurantAdminId == userId ).Select(x => x.Size).Count(x => !x.IsDeleted);
            List<Size> sizes;
            if (pageSize > 0)
                sizes = _repository.Query(x => !x.Size.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Size.Restaurant.RestaurantAdminId == userId).Select(x => x.Size)
                    .OrderBy(x => x.SizeId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                sizes = _repository.Query(x => !x.Size.IsDeleted && x.Language.ToLower() == language.ToLower() && x.Size.Restaurant.RestaurantAdminId == userId).Select(x => x.Size)
                    .OrderBy(x => x.SizeId).ToList();
            results.Data = Mapper.Map<List<Size>, List<SizeDto>>(sizes);
            //results.Data = Mapper.Map<List<Size>, List<SizeDto>>(sizes, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            foreach (Size size in src)
            //            {
            //                size.SizeTranslations = size.SizeTranslations
            //                    .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            }

            //        }
            //    );
            //});
            return results;
        }

        public bool CheckSizeNameExist(string sizeName, string language, long sizeId,long restaurantAdminId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.SizeName.ToLower() == sizeName.ToLower() &&
                          !x.Size.IsDeleted && x.SizeId != sizeId && x.Size.Restaurant.RestaurantAdminId == restaurantAdminId);
        }

        public bool CheckSizeNameTranslated(string language, long sizeId)
        {
            return !Queryable().Any(x => x.Language.ToLower() == language.ToLower() && !x.Size.IsDeleted && x.SizeId == sizeId);
        }
    }
}
