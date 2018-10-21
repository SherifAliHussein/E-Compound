﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.Common.CustomException
{
    public enum ErrorCodes
    {
        UnitNameAlreadyExist,
        UserNotFound,
        UserCategoryNotFound,
        UnitHasRelation,
        UnitTypeHasRelation,
        EmptyFeatureName,
        FeatureNameExceedLength,
        FeatureNameAlreadyExist,
        FeatureNotFound,
        EmptyUserName,
        NameExceedLength,
        EmptyPassword,
        PasswordLengthNotMatched,
        UserNameAlreadyExist,
        EmptyImage,
        ImageExceedSize,
        InvalidImageType,
        RequestStatusChanged,
        UserDeleted,
        UserDeactivated,
        UnitTypeNotFound,
        UnitNotFound,
        ApartmentDeactivated,


        UnSupportedLanguage,
        EmptyRestaurantType,
        RestaurantTypeExceedLength,
        RestaurantTypeAlreadyExist,
        EmptyRestaurantName,
        RestaurantTypeNotFound,
        RestaurantNameAlreadyExist,
        RestaurantNameExceedLength,
        EmptyRestaurantDescription,
        EmptyRestaurantAdminUserName,
        EmptyRestaurantAdminPassword,
        RestaurantAdminUserNameAlreadyExist,
        RestaurantAdminPasswordLengthNotMatched,
        RestaurantNotFound,
        RestaurantMenuDoesNotActivated,
        RestaurantDeleted,
        EmptyMenuName,
        MenuNameExceedLength,
        MenuNameAlreadyExist,
        MenuNotFound,
        MenuDeleted,
        MenuCategoriesDoesNotActivated,
        EmptyCategoryName,
        CategoryNameExceedLength,
        CategoryNameAlreadyExist,
        CategoryNotFound,
        CategoryDeleted,
        CategoryItemsDoesNotActivated,
        EmptyItemName,
        EmptyItemImage,
        ItemNameExceedLength,
        ItemNameAlreadyExist,
        ItemNotFound,
        ItemDeleted,
        EmptyItemDescription,
        EmptyItemPrice,
        InvalidItemPrice,
        RestaurantAdminNotFound,
        EmptyRestaurantLogo,
        EmptyCategoryImage,
        EmptySizeName,
        SizeNameExceedLength,
        SizeNameMinimumLength,
        SizeNameAlreadyExist,
        SizeDeleted,
        SizeNotFound,
        SizeHasItems,
        EmptySideItemName,
        SideItemNameExceedLength,
        SideItemNameMinimumLength,
        SideItemNameAlreadyExist,
        SideItemDeleted,
        SideItemNotFound,
        SideHasItems,
        InvalidSideItemValue,
        SizeIsNotTranslated,
        SideItemIsNotTranslated,
        MenuIsNotTranslated,
        CategoryIsNotTranslated,
        RestaurantIsNotTranslated,
        ItemIsNotTranslated,
        RestaurantIsNotReady,
        RestaurantTypeIsNotTranslated,
        EmptyRestaurantWaiterUserName,
        RestaurantWaiterNameExceedLength,
        BackgroundNotFound,
        BackgroundDeleted,
        BackgroundItemsDoesNotActivated,
        EmptyBackgroundImage,
        TemplateNotFound,
        CategoryTemplatesInvalid,
        RestaurantIsNotActivated,
        EmptyBranchTitle,
        EmptyBranchAddress,
        BranchTiteExceedLength,
        BranchAddressExceedLength,
        BranchTitleAlreadyExist,
        BranchNotFound,
        BranchDeleted,
        PackageExpired,
        PackageNotActivated,
        GlobalAdminInactive,
        BuildingNameRepeated,
        FloorNameRepeated
    }
}
