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
    public class TemplateFacade:BaseFacade, ITemplateFacade
    {
        private IPageService _pageService;
        private ITemplateService _templateService;
        private ICategoryService _categoryService;

        public TemplateFacade(IPageService pageService,ITemplateService templateService, ICategoryService categoryService, IUnitOfWorkAsync unitOfWork):base(unitOfWork)
        {
            _categoryService = categoryService;
            _pageService = pageService;
            _templateService = templateService;
        }

        public List<TemplateDTO> GetAllTemplates()
        {
            return Mapper.Map<List<TemplateDTO>>(_templateService.Queryable().ToList());
        }

        public void AddTemplateForCategory(long categoryId,List<PageDTO> pageDtos)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new NotFoundException(ErrorCodes.CategoryDeleted);
            var pages = _pageService.Query(x => x.CategoryId == categoryId).Select().ToList();
            foreach (var page in pages)
            {
                _pageService.Delete(page);
            }
            foreach (var pageDto in pageDtos)
            {
                var template = _templateService.Find(pageDto.TemplateId);
                if (template == null) throw new NotFoundException(ErrorCodes.TemplateNotFound);

                var page = Mapper.Map<Page>(pageDto);
                _pageService.Insert(page);
            }
            SaveChanges();
        }
    }
}
