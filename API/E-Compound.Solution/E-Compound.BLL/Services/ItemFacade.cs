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

namespace E_Compound.BLL.Services
{
    public class ItemFacade:BaseFacade,IitemFacade
    {
        private ICategoryService _categoryService;
        private IitemService _itemService;
        private IitemTranslationService _itemTranslationService;
        private IItemSizeService _itemSizeService;
        private IItemSideItemService _itemSideItemService;
        private IManageStorage _manageStorage;
        private ICategoryTranslationService _categoryTranslationService;
        private ISizeTranslationService _sizeTranslationService;
        private ISideItemTranslationService _sideItemTranslationService;
        private IMenuService _menuService;
        private IRestaurantService _restaurantService;

        private IPageService _pageService;
        private ITemplateService _templateService;
        private IMenuTranslationService _menuTranslationService;

        public ItemFacade(ICategoryService categoryService,IitemService itemService,IitemTranslationService itemTranslationService, IItemSizeService itemSizeService, IItemSideItemService itemSideItemService
            , IManageStorage manageStorage, ISizeTranslationService sizeTranslationService, ISideItemTranslationService sideItemTranslationService, ICategoryTranslationService categoryTranslationService,
            IMenuService menuService , IRestaurantService restaurantService,IPageService pageService, ITemplateService templateService,IMenuTranslationService menuTranslationService, IUnitOfWorkAsync unitOfWork):base(unitOfWork)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _itemTranslationService = itemTranslationService;
            _itemSideItemService = itemSideItemService;
            _itemSizeService = itemSizeService;
            _manageStorage = manageStorage;
            _categoryTranslationService = categoryTranslationService;
            _sizeTranslationService = sizeTranslationService;
            _sideItemTranslationService = sideItemTranslationService;
            _menuService = menuService;
            _restaurantService = restaurantService;

            _pageService = pageService;
            _templateService = templateService;
            _menuTranslationService = menuTranslationService;
        }

        public ItemFacade(ICategoryService categoryService, IitemService itemService, IitemTranslationService itemTranslationService, IItemSizeService itemSizeService, 
            IItemSideItemService itemSideItemService, IManageStorage manageStorage, ISizeTranslationService sizeTranslationService, ISideItemTranslationService sideItemTranslationService, ICategoryTranslationService categoryTranslationService,
            IMenuService menuService, IRestaurantService restaurantService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _itemTranslationService = itemTranslationService;
            _itemSideItemService = itemSideItemService;
            _itemSizeService = itemSizeService;
            _manageStorage = manageStorage;
            _categoryTranslationService = categoryTranslationService;
            _sizeTranslationService = sizeTranslationService;
            _sideItemTranslationService = sideItemTranslationService;
            _menuService = menuService;
            _restaurantService = restaurantService;
        }


        public void AddItem(ItemDTO itemDto, string path)
        {
            ValidateItem(itemDto);
            var category = _categoryService.Find(itemDto.CategoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new ValidationException(ErrorCodes.CategoryDeleted);
            var item = Mapper.Map<Item>(itemDto);
            
            if (Strings.SupportedLanguages.Any(lang => itemDto.Sizes.Any(x => _sizeTranslationService.CheckSizeNameTranslated(lang.ToLower(), x.SizeId))))
                throw new ValidationException(ErrorCodes.SizeIsNotTranslated);
            if (Strings.SupportedLanguages.Any(lang => itemDto.SideItems.Any(x => _sideItemTranslationService.CheckSideItemNameTranslated(lang.ToLower(), x.SideItemId))))
                throw new ValidationException(ErrorCodes.SideItemIsNotTranslated);

            foreach (var itemName in itemDto.ItemNameDictionary)
            {
                item.ItemTranslations.Add(new ItemTranslation
                {
                    ItemName = itemName.Value,
                    ItemDescription = itemDto.ItemDescriptionDictionary[itemName.Key],
                    Language = itemName.Key.ToLower()
                });
            }
            foreach (var sizeDto in itemDto.Sizes)
            {
                item.ItemSizes.Add(new ItemSize
                {
                    SizeId = sizeDto.SizeId,
                    ItemId = item.ItemId,
                    Price = sizeDto.Price
                });
            }
            foreach (var sideItemDto in itemDto.SideItems)
            {
                item.ItemSideItems.Add(new ItemSideItem
                {
                    SideItemId = sideItemDto.SideItemId,
                    ItemId = item.ItemId
                });
            }
            item.OrderNumber = 100000;
            //  item.OrderNumber = category.Items.Select(x => x.OrderNumber).Max();
            //item.CategoryId = categoryId;
            _itemSideItemService.InsertRange(item.ItemSideItems);
            _itemTranslationService.InsertRange(item.ItemTranslations);
            _itemSizeService.InsertRange(item.ItemSizes);
            _itemService.Insert(item);

            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + + category.Menu.RestaurantId + "\\" + "Menu-" + category.MenuId + "\\"+ "Category-"+ item.CategoryId + "\\Items", itemDto.Image, item.ItemId+"-1");
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + +category.Menu.RestaurantId + "\\" + "Menu-" + category.MenuId + "\\" + "Category-" + item.CategoryId + "\\Items", itemDto.Image2, item.ItemId+"-2");
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + +category.Menu.RestaurantId + "\\" + "Menu-" + category.MenuId + "\\" + "Category-" + item.CategoryId + "\\Items", itemDto.Image3, item.ItemId + "-3");
        }

        public ItemDTO GetItem(long itemId, string language)
        {
            var item = _itemService.Find(itemId);
            if (item == null) throw new NotFoundException(ErrorCodes.ItemNotFound);
            if (item.IsDeleted) throw new NotFoundException(ErrorCodes.ItemDeleted);
            //return Mapper.Map<Item, ItemDTO>(item, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.ItemTranslations = src.ItemTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower())
            //                .ToList();
            //        }
            //    );
            //});
            var itemDto = Mapper.Map<ItemDTO>(item);
            var itemTranslation = item.ItemTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
            itemDto.ItemName = itemTranslation.ItemName;
            itemDto.ItemDescription = itemTranslation.ItemDescription;
            return itemDto;
        }

        private void ValidateItem(ItemDTO itemDto)
        {
            foreach (var itemName in itemDto.ItemNameDictionary)
            {
                if (string.IsNullOrEmpty(itemName.Value))
                    throw new ValidationException(ErrorCodes.EmptyItemName);
                if (string.IsNullOrEmpty(itemDto.ItemDescriptionDictionary[itemName.Key]))
                    throw new ValidationException(ErrorCodes.EmptyItemDescription);
                if (itemName.Value.Length > 100)
                    throw new ValidationException(ErrorCodes.ItemNameExceedLength);
                if (_itemTranslationService.CheckItemNameExistForCategory(itemName.Value, itemName.Key, itemDto.ItemID,
                    itemDto.CategoryId)) throw new ValidationException(ErrorCodes.ItemNameAlreadyExist);
            }
            if (itemDto.Sizes.Any(x => double.IsNaN(x.Price) || x.Price <= 0))
                throw new ValidationException(ErrorCodes.InvalidItemPrice);
        }

        public PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new ValidationException(ErrorCodes.CategoryDeleted);
            var results = _itemTranslationService.GetAllItemsByCategoryId(language, categoryId, page, pageSize);
            //results.IsParentTranslated = language == Strings.DefaultLanguage || _categoryTranslationService.CheckCategoryByLanguage(categoryId, language);
            return results;
        }

        public PagedResultsDto GetActivatedItemsByCategoryId(string language, long categoryId, int page, int pageSize)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new ValidationException(ErrorCodes.CategoryDeleted);
            var results = _itemTranslationService.GetActivatedItemsByCategoryId(language, categoryId, page, pageSize);
            return results;
        }

        public void ActivateItem(long itemId)
        {
            var item = _itemService.Find(itemId);
            if (item == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            //if (item.Items.Count <= 0)
            //    throw new ValidationException(ErrorCodes.CategoryItemsDoesNotActivated);
            if (Strings.SupportedLanguages.Any(x => !item.ItemTranslations.Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.ItemIsNotTranslated);
            item.IsActive = true;
            _itemService.Update(item);

            var itemCountForAllTemplates = item.Category.Pages.Select(x => x.Template.ItemCount).Sum();
            var totalItemCount = item.Category.Items.Count(x => x.IsActive);
            if (totalItemCount > itemCountForAllTemplates)
            {
                var template = _templateService.Find(Strings.DefaultTemplateId);
                var pageNumber = _pageService.GetLastPageNumberForCategory(item.CategoryId) +1;
                while (totalItemCount > itemCountForAllTemplates)
                {
                    var page = new Page();
                    page.TemplateId = template.Id;
                    page.CategoryId = item.CategoryId;
                    page.PageNumber = pageNumber;
                    _pageService.Insert(page);
                    itemCountForAllTemplates += template.ItemCount;
                }
            }

            SaveChanges();
        }

        public void DeActivateItem(long itemId)
        {
            var item = _itemService.Find(itemId);
            if (item == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            item.IsActive = false;
            _itemService.Update(item);
            var categoryHasItemActivated = item.Category.Items.Any(x => x.IsActive);
            if (!categoryHasItemActivated)
            {
                item.Category.IsActive = false;
                _categoryService.Update(item.Category);
                CheckMenuHasCategoryActivated(item);
            }
            SaveChanges();
        }

        public void DeleteItem(long itemId)
        {
            var item = _itemService.Find(itemId);
            if (item == null) throw new NotFoundException(ErrorCodes.ItemNotFound);
            item.IsDeleted = true;
            item.IsActive = false;
            _itemService.Update(item);
            var categoryHasItemActivated = item.Category.Items.Any(x => x.IsActive);
            if (!categoryHasItemActivated)
            {
                item.Category.IsActive = false;
                _categoryService.Update(item.Category);
                CheckMenuHasCategoryActivated(item);
            }
            SaveChanges();
        }

        public void LikeItem(long itemId)
        {
            var item = _itemService.Find(itemId);
            if (item == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            item.Like = item.Like + 1;
            _itemService.Update(item);
            SaveChanges();
        }

        public void DislikeItem(long itemId)
        {
            var item = _itemService.Find(itemId);
            if (item == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            item.Dislike = item.Dislike + 1;
            _itemService.Update(item);
            SaveChanges();
        }
        private void CheckMenuHasCategoryActivated(Item item)
        {
            var menuHasCategoryActivated = item.Category.Menu.Categories.Any(x => x.IsActive);
            if (!menuHasCategoryActivated)
            {
                item.Category.Menu.IsActive = false;
                _menuService.Update(item.Category.Menu);
                CheckRestaurantHasMenuActivated(item);
            }
        }

        private void CheckRestaurantHasMenuActivated(Item item)
        {
            var restaurantHasMenuActivated = item.Category.Menu.Restaurant.Menus.Any(x => x.IsActive);
            if (!restaurantHasMenuActivated)
            {
                item.Category.Menu.Restaurant.IsActive = false;
                _restaurantService.Update(item.Category.Menu.Restaurant);
            }
        }
        public void UpdateItem(ItemDTO itemDto, string path)
        {
            ValidateItem(itemDto);
            var item = _itemService.Find(itemDto.ItemID);
            if (item == null) throw new NotFoundException(ErrorCodes.ItemNotFound);

            if (Strings.SupportedLanguages.Any(lang => itemDto.Sizes.Any( x => _sizeTranslationService.CheckSizeNameTranslated(lang.ToLower(), x.SizeId))))
                throw new ValidationException(ErrorCodes.SizeIsNotTranslated);
            if (Strings.SupportedLanguages.Any(lang => itemDto.SideItems.Any(x => _sideItemTranslationService.CheckSideItemNameTranslated(lang.ToLower(), x.SideItemId))))
                throw new ValidationException(ErrorCodes.SideItemIsNotTranslated);

            foreach (var itemName in itemDto.ItemNameDictionary)
            {
                var itemTranslation =
                    item.ItemTranslations.FirstOrDefault(x => x.Language.ToLower() == itemName.Key.ToLower());
                if (itemTranslation == null)
                {
                    item.ItemTranslations.Add(new ItemTranslation
                    {
                        Language = itemName.Key.ToLower(),
                        ItemName = itemName.Value,
                        ItemDescription = itemDto.ItemDescriptionDictionary[itemName.Key]
                    });
                }
                else
                {
                    itemTranslation.ItemName = itemName.Value;
                    itemTranslation.ItemDescription = itemDto.ItemDescriptionDictionary[itemName.Key];
                }
            }
            foreach (var sizeDto in itemDto.Sizes)
            {
                var size = item.ItemSizes.FirstOrDefault(x => x.SizeId == sizeDto.SizeId);
                if ( size == null)
                    item.ItemSizes.Add(new ItemSize
                    {
                        SizeId = sizeDto.SizeId,
                        ItemId = item.ItemId,
                        Price =  sizeDto.Price
                    });
                else
                {
                    size.Price = sizeDto.Price;
                    _itemSizeService.Update(size);
                }
            }
            var deleteItemSizes = item.ItemSizes.Where(x => !itemDto.Sizes.Select(s => s.SizeId).Contains(x.SizeId)).ToList();
            foreach (var itemSize in deleteItemSizes)
            {
                _itemSizeService.Delete(itemSize.ItemSizeId);
            }
            foreach (var sideItemDto in itemDto.SideItems)
            {
                if (item.ItemSideItems.FirstOrDefault(x => x.SideItemId == sideItemDto.SideItemId) == null)
                    item.ItemSideItems.Add(new ItemSideItem
                    {
                        SideItemId = sideItemDto.SideItemId,
                        ItemId = item.ItemId
                    });
            }
            var deItemSideItems = item.ItemSideItems.Where(x => !itemDto.SideItems.Select(s => s.SideItemId).Contains(x.SideItemId)).ToList();
            foreach (var itemSideItem in deItemSideItems)
            {
                _itemSideItemService.Delete(itemSideItem.ItemSideItemId);
            }
            item.MaxSideItemValue = itemDto.MaxSideItemValue;
            //item.Price = itemDto.Price;
            _itemService.Update(item);
            SaveChanges();
            if (itemDto.IsImageChange)
                _manageStorage.UploadImage( path + "\\" + "Restaurant-"  + item.Category.Menu.RestaurantId + "\\" + "Menu-" + item.Category.MenuId + "\\"+ "Category-" + item.CategoryId + "\\Items" , itemDto.Image, item.ItemId+"-1");
            if (itemDto.IsImage2Change)
                _manageStorage.UploadImage(path + "\\" + "Restaurant-" + item.Category.Menu.RestaurantId + "\\" + "Menu-" + item.Category.MenuId + "\\" + "Category-" + item.CategoryId + "\\Items", itemDto.Image2, item.ItemId+"-2");
            if (itemDto.IsImage3Change)
                _manageStorage.UploadImage(path + "\\" + "Restaurant-" + item.Category.Menu.RestaurantId + "\\" + "Menu-" + item.Category.MenuId + "\\" + "Category-" + item.CategoryId + "\\Items", itemDto.Image3, item.ItemId + "-3");
        }
        
        public void TranslateItem(ItemDTO itemDto, string language)
        {
            ValidateTranslateItem(itemDto, language);
            var item = _itemService.Find(itemDto.ItemID);
            if (item == null) throw new NotFoundException(ErrorCodes.ItemNotFound);
            if (item.ItemSizes.Any(x => _sizeTranslationService.CheckSizeNameTranslated(language, x.SizeId))) throw new ValidationException(ErrorCodes.SizeIsNotTranslated);
            if (item.ItemSideItems.Any(x => _sideItemTranslationService.CheckSideItemNameTranslated(language, x.SideItemId))) throw new ValidationException(ErrorCodes.SideItemIsNotTranslated);
            var itemTranslation = item.ItemTranslations.FirstOrDefault(x => x.Language.ToLower() == language.ToLower());
            if (itemTranslation == null)
            {
                item.ItemTranslations.Add(new ItemTranslation
                {
                    Language = language,
                    ItemName = itemDto.ItemName,
                    ItemDescription = itemDto.ItemDescription
                });
            }
            else
            {
                itemTranslation.ItemName = itemDto.ItemName;
                itemTranslation.ItemDescription = itemDto.ItemDescription;
            }

            _itemService.Update(item);
            SaveChanges();
        }
        private void ValidateTranslateItem(ItemDTO itemDto, string language)
        {
            if (string.IsNullOrEmpty(itemDto.ItemName))
                throw new ValidationException(ErrorCodes.EmptyItemName);
            if (string.IsNullOrEmpty(itemDto.ItemDescription))
                throw new ValidationException(ErrorCodes.EmptyItemDescription);
            if (itemDto.ItemName.Length > 100)
                throw new ValidationException(ErrorCodes.ItemNameExceedLength);
            if (_itemTranslationService.CheckItemNameExistForCategory(itemDto.ItemName, language, itemDto.ItemID, itemDto.CategoryId)) throw new ValidationException(ErrorCodes.ItemNameAlreadyExist);
        }
        public List<ItemNamesDto> GetAllItemNamesByCategoryId(string language, long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new ValidationException(ErrorCodes.CategoryDeleted);
            return  _itemTranslationService.GetAllItemNamesByCategoryId(language, categoryId);
        }

        public CategoryPageTemplateDTO GetActivatedItemsWithTemplatesByCategoryId(string language, long categoryId)
        {
            var category = _categoryService.Query(x => x.CategoryId == categoryId).Include(x => x.CategoryTranslations)
                .Select().FirstOrDefault();
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new ValidationException(ErrorCodes.CategoryDeleted);

            var pages = _pageService.Query(x => x.CategoryId == categoryId).Include(x => x.Template).Select().ToList();
            var items = _itemTranslationService
                .Query(x => x.Language.ToLower() == language.ToLower() && x.Item.CategoryId == categoryId &&
                            x.Item.IsActive && !x.Item.IsDeleted).Select(x => x.Item).OrderBy(x => x.OrderNumber)
                .ToList();
            var itemsDtos = Mapper.Map<List<Item>, List<ItemDTO>>(items);
            //var itemsDtos = Mapper.Map<List<Item>, List<ItemDTO>>(items, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            foreach (Item item in src)
            //            {
            //                item.ItemTranslations = item.ItemTranslations
            //                    .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            }

            //        }
            //    );
            //});
            int itemCount = 0;
            List<PageTemplateDTO> pageTemplateDtos = new List<PageTemplateDTO>();
            foreach (var pageTemplate in pages)
            {
                var pageItems = itemsDtos.Skip(itemCount).Take(pageTemplate.Template.ItemCount).ToList();
                if (pageItems.Count <= 0) continue;
                pageTemplateDtos.Add(new PageTemplateDTO
                {
                    PageNumber = pageTemplate.PageNumber,
                    TemplateId = pageTemplate.TemplateId,
                    ItemDto = pageItems,
                    ItemCount = pageTemplate.Template.ItemCount

                });
                itemCount += pageTemplate.Template.ItemCount;
            }
            var categoryTemplates = new CategoryPageTemplateDTO
            {
                Templates = pageTemplateDtos,
                //MenuName = category.Menu.MenuTranslations
                //    .FirstOrDefault(x => x.Language.ToLower() == language.ToLower()).MenuName,
                MenuNameDictionary = _menuService.Find(category.MenuId).MenuTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.MenuName),
                //CategoryName = category.CategoryTranslations
                //    .FirstOrDefault(x => x.Language.ToLower() == language.ToLower()).CategoryName,
                CategoryNameDictionary = category.CategoryTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.CategoryName),
                CategoryId = categoryId,
                MenuId = category.MenuId,
                RestaurantId = category.Menu.RestaurantId
            };
            return categoryTemplates;
        }

        public void UpdateItemOrder(List<ItemNamesDto> itemNamesDto)
        {
            foreach (var itemDto in itemNamesDto)
            {
                var item = _itemService.Find(itemDto.ItemId);
                item.OrderNumber = itemDto.OrderNumber;
                _itemService.Update(item);
            }
            SaveChanges();
        }
    }
}
