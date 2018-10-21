(function() {
  'use strict';

  angular
  .module('core').config(["$translateProvider","appCONSTANTS",function($translateProvider,appCONSTANTS){
        
        var en_translations = {
            "Assinglbl" : "Assing",
            "Featureslbl" : "Features",
            "Name" : "Name",
            "englishName" : "English Name",
            "arabicName" : "Arabic Name",
            'NoFeaturesAvailable':'there is no features.',
            "saveChangesBtn":"save changes",
            "DiscardBtn":"Discard",
            "requiredErr":"required",
            "deleteConfirmationLbl":" Are you sure you want delete ",
            "deleteBtn":"Delete",
            "cancelBtn":"cancel",
            "DescriptionLbl":"Description",
            "englishDescriptionLbl":"English Description",
            "arabicDescriptionLbl":"Arabic Description",
            "LogoLbl":"logo",
            "TypeLbl":"Type",
            "AdminUserLbl":"Admin email",
            "AdminUserPasswordLbl":"Admin password",
            "ActivateBtn":"Activate",
            "DeActivateBtn":"DeActivate",
            "UploadBtn":"select logo",
            "Imagelbl":"Image",
            "UploadImageBtn":"Select Image",
            "NameLengthError":"Name length must be 3-40 characters.",
            "SizeLengthError":"Name length must be 1-10 characters.",
            "DescLengthError":"Description length must be 3-300 characters.",
            "english":"english",
            "arabic":"arabic",
            "logoutBtn":"Logout",
            "status":"Status",
            "ReadyLbl":"Ready",
            "UserName":"User Name",
            "Title":"Title",
            "OkLbl":"Ok",
            "invalidEmail":"Invalid Email",
            "ConsumedMsg":"Consumed",
            "maximumMsg":"Maximum",
            "MinimumMsg":"Minimum",
            "ConfirmPasswordLbl":"Confirm password",            
            "AddFeatureBtn":"Add feature",
            "AddRestaurantFeatureBtn":"Add restaurant feature",
            "newFeatureLbl":"New Feature",
            "UpdateFeatureLbl":"Update Feature",
            "hasDetailsLbl":"Has details?",
            "FeatureAddSuccess":"Feature added successfuly",
            "FeatureDeleteSuccess":"Feature deleted successfuly",
            "FeatureUpdateSuccess":"Feature updated successfuly",
            "Users":"Users",
            "ReceptionistLbl":"Receptionist",
            "AddReceptionistBtn":"Add Receptionist",
            "ReceptionistAddSuccess":"Receptionist added successfuly",
            "NewReceptionistLbl":"New Receptionist",
            "ReceptionistUserPasswordLbl":"Receptionist password",
            "ReceptionistDeleteSuccess":"Receptionist deleted successfuly",
            "UpdateReceptionistLbl":"Update Receptionist",
            "ReceptionistUpdateSuccess":"Receptionist updated successfuly",
            "NoReceptionistLbl": "there is no receptionists",

            "SupervisorLbl":"Supervisor",
            "AddSupervisorBtn":"Add Supervisor",
            "SupervisorAddSuccess":"Supervisor added successfuly",
            "NewSupervisorLbl":"New Supervisor",
            "SupervisorUserPasswordLbl":"Supervisor password",
            "SupervisorDeleteSuccess":"Supervisor deleted successfuly",
            "UpdateSupervisorLbl":"Update Supervisor",
            "SupervisorUpdateSuccess":"Supervisor updated successfuly",
            "NoSupervisorLbl": "there is no supervisors",
            "selectFeatureLbl":"Select features",

            "englishDescriptionLbl":"English Description",
            "arabicDescriptionLbl":"Arabic Description",
            "DescriptionLbl":"Description",
            "IsFreeLbl":"Is free?",
            "priceLbl":"Price",
            "totalpriceLbl":"total price",
            "freelbl":"Free",
            "AddMoreBtn":"Add more",
            "AddMoreDetailBtn":"Add more detail",
            "FeatureDetailExist":"feature detail is already exist",
            "UploadBtn":"select image",
            "imgaeSizeError":"Maximum size allowed is 2MB",
            "imageTypeError":"Only (.jpeg, .bmp, .png) extensions are allowed.",
            "Imagelbl":"Image",

            "Rooms":"Apartments",
            "RoomLbl":"Apartment",
            "AddRoomBtn":"Add Apartment",
            "RoomAddSuccess":"Apartment added successfuly",
            "NewRoomLbl":"New Apartment",
            "RoomUserPasswordLbl":"Apartment password",
            "RoomDeleteSuccess":"Apartment deleted successfuly",
            "UpdateRoomLbl":"Update Apartment",
            "RoomUpdateSuccess":"Apartment updated successfuly",
            "NoroomsAvailable": "there is no Apartments ",

            "RequestSuccess":"Request successfuly",
            "Requests":"Requests",
            "NorequestsAvailable":"No request available",
            "Featurelbl":"Feature",
            "CreateTime":"Creation date",
            "StatusLbl":"Status",
            "ApproveBtn":"Approve",
            "RejectBtn":"Reject",
            "RequestDetailLbl":"Request detail",
            "NumberLbl":"Count",
            "roomName":"Apartment name",
            "RoomPasswordLbl":"Apartment Password",
            "Pending":"Pending",
            "Approved":"Approved by",
            "Rejected":"Rejected by",
            "supervisorhasnofeature":"feature deleted or deactivated",
            "requestConfirmationLbl":"Are you sure you want request ",
            "roomsLimitConsumedLbl":"consumed / limit of units",
            "unitTypes":"Unit Types",
            "UnitType":"Unit Type",

            "restaurantType" : "Restaurant types",
            "restaurant" : "Restaurants",
            "Name" : "Name",
            "englishName" : "English Name",
            "arabicName" : "Arabic Name",
            "AddType": "Add new type",
            "NoRestaurantTypesAvailable":"There is no restaurant types.",
            "saveChangesBtn":"save changes",
            "DiscardBtn":"Discard",
            "NewRestaurantTypeLbl":"New restaurant type",
            "requiredErr":"required",
            "UpdateRestaurantTypeLbl":"Update restaurant type",
            "deleteConfirmationLbl":" Are you sure you want delete ",
            "deleteBtn":"Delete",
            "cancelBtn":"cancel",
            "DescriptionLbl":"Description",
            "englishDescriptionLbl":"English Description",
            "arabicDescriptionLbl":"Arabic Description",
            "AddRestaurantBtn":"Add new restaurant",
            "NoRestaurantAvailable":"There is no restaurants.",
            "LogoLbl":"logo",
            "TypeLbl":"Type",
            "AdminUserLbl":"Admin email",
            "AdminUserPasswordLbl":"Admin password",
            "WaiterUserPasswordLbl":"Waiter password",
            "ActivateBtn":"Activate",
            "DeActivateBtn":"DeActivate",
            "NewRestaurantLbl":"New restaurant",
            "UpdateRestaurantLbl":"Update restaurant",
            "restaurantTypesLbl" : "Restaurant Type",
            "restaurantDeleteSuccess":"restaurant deleted successfully.",
            "restaurantUpdateSuccess":"restaurant updated successfully.",
            "restaurantAddSuccess":"restaurant added successfully.",
            "UploadBtn":"select logo",
            "imgaeSizeError":"Maximum size allowed is 2MB",
            "imageTypeError":"Only (.jpeg, .bmp, .png) extensions are allowed.",
            "AddMenuBtn": "Add new menu",
            "NoMenusAvailable":"There is no menus.",
            "UpdateMenuLbl":"Update Menu",
            "NewMenuLbl":"New menu",
            "Menu":"Menu",
            "menuAddSuccess":"Menu added successfully.",
            "menuDeleteSuccess":"Menu deleted successfully.",
            "menuUpdateSucess":"Menu updated successfully.",
            "CategoriesBtn":"View Categories",
            "AddCategoryBtn":"Add new category",
            "NoCategoryAvailable":"There is no categories.",
            "Imagelbl":"Image",
            "newCategoryLbl":"New Category",
            "CategoryAddSuccess":"Category added successfully.",
            "UpdateCategoryLbl":"Update Category",
            "UploadImageBtn":"Select Image",
            "CategoryupdateSuccess":"Category updated successfully.",
            "CategoryDeleteSuccess":"Category deleted successfully.",
            "size":"Sizes",
            "AddSizeBtn":"Add new size",
            "NoSizesAvailable":"there is no sizes.",
            "sizeAddSuccess":"Size added successfully.",
            "NewSizeLbl":"New size",
            "UpdateSizeLbl":"Update size",
            "UpdateSizeSuccess":"Size updated successfully.",
            "SizeDeleteSuccess":"Size deleted successfully.",
            "NameLengthError":"Name length must be 3-40 characters.",
            "SizeLengthError":"Name length must be 1-10 characters.",
            "DescLengthError":"Description length must be 3-300 characters.",
            "sideItem":"Side items",
            "value":"Value",
            "AddSideItemBtn":"Add new side item",
            "NoSideItemsAvailable":"there is no side items.",
            "SideItemAddSuccess":"Side item added successfully.",
            "NewSideItemLbl":"New side item",
            "UpdateSideItemLbl":"Update side item",
            "UpdateSideItemSuccess":"Side item updated successfully.",
            "SideItemDeleteSuccess":"Side item deleted successfully.",
            "english":"english",
            "arabic":"arabic",
            "logoutBtn":"Logout",
            "NoRestaurantDefault":"You must add restaurant in english first.",
            "status":"Status",
            "ItemsBtn":"View items",
            "NoItemAvailable":"there is no items.",
            "AddItemBtn":"Add new Item",
            "itemAddSuccess":"item added successfully.",
            "NewItemtLbl":"New item",
            "UpdateItemLbl":"Update item",
            "Pricelbl":"Price",
            "itemDeleteSuccess":"item deleted successfully.",
            "hasSizeLbl":"has Size?",
            "selectSizeLbl":"Select Size",
            "hasSideItemLbl":"has Side Item?",
            "selectSideItemLbl":"Select Side Item",
            "ItemUpdateSuccess":"item updated successfully.",
            "MaxValueLbl":"Maximum value for side items",
            "ReadyLbl":"Ready",
            "RestaurantTypeDeleteMessage":"all restaurants under this type will be deleted",
            "RestaurantTypeAddSuccess":"restaurant type added successfully.",
            "RestaurantTypeUpdateSuccess":"restaurant type updated successfully.",
            "ConfirmPasswordLbl":"Confirm password",
            "RestaurantTypeDeleteSuccess":"restaurant type deleted successfully.",
            "MaxSideItemValueError":"Must be in range for the minimum value and total value of side items",
            "waiter":"Waiter",
            "AddWaiterBtn":"Add new Waiter",
            "NoWaiterAvailable":"There is no waiters",
            "UserName":"User Name",
            "NewWaiterLbl":"New waiter",
            "UpdateWaiterLbl":"Update waiter",
            "WaiterDeleteSuccess":"Waiter deleted successfully.",
            "WaiterUpdateSuccess":"Waiter updated successfully.",
            "WaiterAddSuccess":"Waiter added successfully.",

            "background":"background",
            "AddbackgroundBtn":"Add new background",
            "NobackgroundAvailable":"There is no backgrounds",
            "UserName":"User Name",
            "NewbackgroundLbl":"New background",
            "UpdatebackgroundLbl":"Update background",
            "backgroundDeleteSuccess":"background deleted successfully.",
            "backgroundUpdateSuccess":"background updated successfully.",
            "backgroundAddSuccess":"background added successfully.",
            "CurrentBtn":"Current Background",
            "NotCurrentBtn":"Select as background", 

            "Template":"Template",
            "RecommendedMenuImage":"Recommended dimension 477 x 286",
            "RecommendedCategoryImage":"Recommended dimension 210 x 137",
            "RecommendedItemImage1":"Recommended dimension 423 x 139",
            "RecommendedItemImage2":"Recommended dimension 112 x 69", 
            "RecommendedItemImage3":"Recommended dimension 527 x 735", 
            "PriceLengthError":"Price length must be 1-5 Digts.",
            "RecommendedBackgroundImage":"Recommended dimension 1920 x 1200", 

            "Branch":"Branch",
            "AddBranchBtn":"Add Branch",
            "NoBranchAvailable":"There is no branches.",
            "newBranchLbl":"New Branch",
            "Title":"Title",
            "Address":"Address",
            "englishAddress":"English Address",
            "arabicAddress":"Arabic Address",
            "BranchAddSuccess":"Branch added successfuly.",
            "UpdateBranchLbl":"Update Branch",
            "BranchUpdateSuccess":"Branch Updated successfuly.",
            "NumOfUsersLbl":"number of waiters",
            "waitersLimitConsumedLbl":"consumed / limit waiters",
            "consumedAndTotal":"consumed / total",
            "TotalRemaining":"total / remaining",
            "OkLbl":"Ok",
            "pageLbl":"Page",
            "selectTemplate":"Select template for page",
            "selectedTemplates":"Selected templates",
            "startDatelbl":"Start date",
            "endDatelbl":"End date",
            "invalidEmail":"Invalid Email",
            "ConsumedMsg":"Consumed",
            "maximumMsg":"Maximum",
            "OrderItemUpdateSuccess":"Items sorted",
            "TemplateUpdateSuccessfuly":"Templates updated successfuly.",
            "orderItem":"order items",
            "MinimumMsg":"Minimum",
            "TelmplateErrorCount":"Template should have",


            "selectRestaurantLbl":"Select restaurants",
            "itemlbl":"item",
            "likelbl":"Like",
            "dislike":"Dislike",
            "feedBacksBtn":"feedBacks",
            "Comment":"Comment",
            "createdate":"date",
            "Rate":"Rate",
            "Guest":"Guest",
            "NoFeedBacksAvailable":"there is no feedback",
            "BuildingLbl":"Building",
            "AddBuildingBtn":"Add building",
            "NoBuildingLbl":"there is no buildings",
            "NewBuildingLbl":"New building",
            "BuildingAddSuccess":"Building added successfuly",
            "BuildingUpdateSuccess":"Building updated successfuly",
            "FloorLbl":"Floor",
            "AddFloorBtn":"Add floor",
            "NoFloorLbl":"there is no floors",
            "NewFloorLbl":"New floor",
            "FloorAddSuccess":"Floor added successfuly",
            "FloorUpdateSuccess":"Floor updated successfuly",
            "buildingName":"building name",
            "floorName":"floor name",
            "selectControlLbl":"Select control",
            "orderControlLbl":"Order control",
            "detailLbl":"Details",
            "ListOfText":"List of text",
            "ListOfImage":"List of image",
            "ListOfTextAndImage":"List of text and image",
            "Videos":"Videos",
            "Available":"Available",
            "Time":"Time",
            "ListOfAvailable":"List Of availability",
            "ManageFeatureLbl":"Manage feature",
            "Add":"Add",
            "NoData":"No data",
            "singleSelect":"Single select",
            "multiSelect":"Multi select",
            "NoActionForFeature":"No action for this feature",

            "Sunday":"Sunday",
            "Monday":"Monday",
            "Tuesday":"Tuesday",
            "Wednesday":"Wednesday",
            "Thursday":"Thursday",
            "Friday":"Friday",
            "Saturday":"Saturday",
            "From":"From",
            "To":"To",
            "EditBtn":"Edit",
            "applyFilterBtn":"apply filter",
            "Reports":"Reports",
            "ExportCsvBtn":"Export to csv",
            "downloadBtn":"download",
            "NoUnitTypeAvailable":"No Unit Type Available",
            "AddUnitTypeLbl":"Add Unit Type",
            "Title":"Title",
            "UpdateUnitTypeLbl":"Update Unit Type",
            "DeleteSuccess": "Deleted Successfully",
            "EditSuccess":"Edited Successfully",
            "units":"Units",
            "NoUnitAvailable":"No Unit Available",
            "AddUnitLbl":"Add Unit",
            "UnitNameLbl":"Unit Name",
            "Unit":"Unit",
            "UpdateUnitLbl":"Update Unit",
            "Limit":"Invitees' Limit",
            "TextOnly":"Text Only",
            "NumbersOnly":"Numbers Only",
            "LimitReqError":"Invitees' Limit Required",
            "LimitLengthError":"Limit should be from 1:2 numbers",
            "PhoneLengthError":"Limit should be from 10:50 numbers",
            "NameLengthError":"number of characters should be between 2:10",
            "userCategories":"User Categories",
            "AddUserCategoryLbl":"Add User Category",
            "NoUserCategoryAvailable":"No Category Available",
            "UpdateUserCategoryLbl":"Update User Category",
            "TechnicianLbl":"Technician",
            "Categorieslbl":"Categories",
            "AddTechnicianBtn":"Add Technician",
            "NoTechnicianLbl":"No technician available",
            "TechnicianAddSuccess":"Technician Added Successfully",
            "selectCategoryLbl":"Select Category",
            "TechnicianUserPasswordLbl":"Technician Password",
            "technicianhasnofeature":"Technician has no feature",
            "TechnicianDeleteSuccess":"Technician Deleted Successfully",
            "TechnicianUpdateSuccess":"Technician Updated Successfully",
            "Action":"Action",
            "technicianComment":"Technician Comment",
            "supervisorComment":"Supervisor Comment",
            "Appartment":"Apartment",
            "PhoneReqError":"Numbers Only",
            "phoneLbl":"Phone",
            "AddnotificationLbl":"Add notification",
            "notifications":"Notification",
            "firstName":"first Name",
            "lastName":"last Name",
        }
        
        var ar_translations = {
            "AddnotificationLbl":"اضافه اشعارات",
            "firstName":"الاسم الاول",
            "firstName":"الاسم الاخير",
           
            "notifications":"اشعارات",
            "PhoneReqError":"رقم التليفون مطلوب",
            "phoneLbl":"التليفون",
            "Appartment":"عقار",
            "technicianComment":"تعليق الفني",
            "supervisorComment":"تعليق المراقب",
             "TechnicianUpdateSuccess":"تم تعديل الفني بنجاح",
            "TechnicianDeleteSuccess" : "تم حذف الفني بنجاح",
            "technicianhasnofeature":"لا يوجد قسم للفني",
            "TechnicianUserPasswordLbl":"كلمة مرور الفني",
            "selectCategoryLbl":"اختار القسم",
            "TechnicianAddSuccess":"تم اضافة الفني بنجاح",
            "NoTechnicianLbl":"لا يوجد فني متاح",
            "AddTechnicianBtn":"اضافة فني",
            "TechnicianLbl":"فني",
            "Categorieslbl":"الاقسام",
            "UpdateUserCategoryLbl":"تعديل اقسام المستخدمين",
            "NoUserCategoryAvailable":"لا يوجد اقسام المسخدمين",
            "AddUserCategoryLbl":"اضافة اقسام المسخدمين",
            "userCategories":"اقسام المسخدمين",
            "NameLengthError":"عدد الحروف يجب ان يكون بين 2:10 حروف",
            "LimitLengthError":"حد المدعويين يجب ان يكون من 1:2 ارقام",
            "PhoneLengthError":"حد رقم الهاتف يجب ان يكون من 10:50 ارقام",
            "LimitReqError":"الحد الأقصي للمدعويين مطلوب",
            "NumbersOnly":"ارقام فقط",
            "TextOnly":"حروف فقط",
            "Limit":"الحد الأقصي للمدعويين",
            "UpdateUnitLbl":"تعديل الوحدة",
            "Unit":"الوحده",
            "UnitNameLbl":"اسم الوحده",
            "AddUnitLbl":"اضافة وحده",
            "NoUnitAvailable":"لا يوجد وحدات متاحه",
            "units":"الوحدات",
            "EditSuccess" : "تم التعديل بنجاح",
            "DeleteSuccess":"تم المسح بنجاح",
            "UpdateUnitTypeLbl":"تعديل نوع الوحدة",
            "Title":"عنوان",
            "AddUnitTypeLbl":"اضافة نوع وحدة",
            "NoUnitTypeAvailable":"لا يوجد نوع وحدة متاح",
            "UnitType":"نوع الوحدة",
            "unitTypes": "أنواع الوحدات",
            "Featureslbl" : "خدمات",
            "Name" : " الاسم",
            "englishName" : "الاسم انجليزي",
            "arabicName" : "الاسم عربي",
            "saveChangesBtn":"حفظ",
            "DiscardBtn":"تجاهل",
            'NoFeaturesAvailable':'.لا يوجد خدمات',
            "requiredErr":"مطلوب",
            "deleteConfirmationLbl":" هل انت متأكد انك تريد حذف ",
            "deleteBtn":"حذف",
            "cancelBtn":"إلغاء",
            "DescriptionLbl":"وصف",
            "englishDescriptionLbl":"وصف انجليزي",
            "arabicDescriptionLbl":"وصف عربي",
            "LogoLbl":"شعار",
            "TypeLbl":"نوع",            
            "AdminUserLbl":"بريد الاكتروني لالمشرف",
            "AdminUserPasswordLbl":"كلمة مرور المشرف",
            "ActivateBtn":"تفعيل",
            "DeActivateBtn":"عطل",
            "UploadBtn":"اختار شعار",
            "Imagelbl":"صوره",
            "UploadImageBtn":"اختار الصوره",
            "NameLengthError":".طول الاسم يجب من 3-40 حرف",
            "SizeLengthError":".طول المقاس يجب من 1-10 حرف",
            "DescLengthError":".طول الوصف يجب من 3-300 حرف",
            "english":"انجليزي",
            "arabic":"عربي",
            "logoutBtn":"خروج",
            "status":"الحالة",
            "ConfirmPasswordLbl":"تأكيد كلمه المرور",
            "UserName":"اسم المستخدم",
            "NewWaiterLbl":"نادل جديد",
            "Title":"لقب",
            "Address":"عنوان",
            "OkLbl":"تم",
            "pageLbl":"صفحه",
            "invalidEmail":"البريد الاكتروني غير صحيح",
            "ConsumedMsg":"المستخدمين",
            "maximumMsg":"الحد الاقصي",
            "MinimumMsg":"علي الاقل",
            "AddFeatureBtn":"اضف خدمة",
            "AddRestaurantFeatureBtn":"اضف خدمة المطعم",
            "newFeatureLbl":"خدمة جديده",
            "UpdateFeatureLbl":"تحديث خدمة",
            "hasDetailsLbl":"هل لديه تفاصيل؟",
            "FeatureAddSuccess":"تم ادخال الخدمة بنجاح",
            "FeatureDeleteSuccess":"تم حذف الخدمة بنجاح",
            "FeatureUpdateSuccess":"تم تحديث الخدمة بنجاح",
            "Users":"المستخدمين",

            "ReceptionistLbl":"موظف الإستقبال",
            "AddReceptionistBtn":"اضف موظف الإستقبال",
            "ReceptionistAddSuccess":"تم ادخال موظف الإستقبال بنجاح",
            "NewReceptionistLbl":"موظف الإستقبال جديد",
            "ReceptionistUserPasswordLbl":"كلمة المرور موظف الإستقبال",
            "ReceptionistDeleteSuccess":"تم حذف موظف الإستقبال بنجاح",
            "UpdateReceptionistLbl":"تحديث موظف الإستقبال",
            "ReceptionistUpdateSuccess":"تم تحديث موظف الإستقبال بنجاح",
            "NoReceptionistLbl": "لا يوجد موظفين الإستقبال",

            "SupervisorLbl":"مشرف",
            "AddSupervisorBtn":"اضف مشرف",
            "SupervisorAddSuccess":"تم ادخال المشرف بنجاح",
            "NewSupervisorLbl":"مشرف جديد",
            "SupervisorUserPasswordLbl":"كلمة مرور مشرف",
            "SupervisorDeleteSuccess":"تم حذف مشرف بنجاح",
            "UpdateSupervisorLbl":"تحديث مشرف",
            "SupervisorUpdateSuccess":"تم تحديث مشرف بنجاح",
            "NoSupervisorLbl": "لا يوجد مشرفين",
            "selectFeatureLbl":"اختار خدمات",

            "DescriptionLbl":"وصف",            
            "englishDescriptionLbl":"وصف انجليزي",
            "arabicDescriptionLbl":"وصف عربي",
            "IsFreeLbl":"مجانا؟",
            "priceLbl":"سعر",
            "totalpriceLbl":"اجمالي السعر",
            "freelbl":"مجانا",
            "AddMoreBtn":"أضف المزيد",
            "AddMoreDetailBtn":"أضف مزيد من التفاصيل",
            "FeatureDetailExist":"التفاصيل موجود بالفعل",
            "UploadBtn":"اختار صورة",
            "Imagelbl":"صوره",
            
            "Rooms":"عقار",
            "RoomLbl":"عقار",
            "AddRoomBtn":"اضف عقار",
            "RoomAddSuccess":"تم ادخال عقار بنجاح",
            "NewRoomLbl":"عقار جديد",
            "RoomUserPasswordLbl":"كلمة المرور عقار",
            "RoomDeleteSuccess":"تم حذف عقار بنجاح",
            "UpdateRoomLbl":"تحديث عقار",
            "RoomUpdateSuccess":"تم تحديث عقار بنجاح",
            "NoroomsAvailable": "لا يوجد عقار",

            "RequestSuccess":"تم الطلب بنجاح",
            "Requests":"طلبات",
            "NorequestsAvailable":"لايوجد طلبات",
            "Featurelbl":" خدمة",
            "CreateTime":"تاريخ الطلب",
            "StatusLbl":"الحاله",
            "ApproveBtn":"وافق",
            "RejectBtn":"رفض",
            "RequestDetailLbl":"تفاصيل الطلب",
            "NumberLbl":"عدد",
            "roomName":"اسم العقار",
            "RoomPasswordLbl":"كلمة مرور العقار",
            "Pending":"قيد الانتظار",
            "Approved":"وافق",
            "Rejected":"رفض",
            "supervisorhasnofeature":"الخدمة غير متاحه او تم حذفها",
            "requestConfirmationLbl":"هل انت متأكد انك تريد طلب ",
            "roomsLimitConsumedLbl":"المجموع العقارات / المستخدم",

            "restaurantType" : "أنواع المطاعم",
            "restaurant" : "مطاعم",
            "Name" : " الاسم",
            "englishName" : "الاسم انجليزي",
            "arabicName" : "الاسم عربي",
            "AddType": "اضف نوع جديد",
            "NoRestaurantTypesAvailable":".لا يوجد انواع مطعم",
            "saveChangesBtn":"حفظ",
            "DiscardBtn":"تجاهل",
            "NewRestaurantTypeLbl":"نوع مطعم جديد",
            "requiredErr":"مطلوب",
            "UpdateRestaurantTypeLbl":"تحديث نوع المطعم",
            "deleteConfirmationLbl":" هل انت متأكد انك تريد حذف ",
            "deleteBtn":"حذف",
            "cancelBtn":"إلغاء",
            "DescriptionLbl":"وصف",
            "englishDescriptionLbl":"وصف انجليزي",
            "arabicDescriptionLbl":"وصف عربي",
            "AddRestaurantBtn":"اضف مطعم جديد",
            "NoRestaurantAvailable":".لا يوجد مطاعم",
            "LogoLbl":"شعار",
            "TypeLbl":"نوع",            
            "AdminUserLbl":"بريد الاكتروني لالمشرف",
            "AdminUserPasswordLbl":"كلمة مرور المشرف",
            "WaiterUserPasswordLbl":"كلمة مرور النادل",
            "ActivateBtn":"تفعيل",
            "DeActivateBtn":"عطل",
            "NewRestaurantLbl":"مطعم جديد",
            "UpdateRestaurantLbl":"تحديث مطعم",
            "restaurantTypesLbl" : "نوع المطاعم",
            "restaurantDeleteSuccess":".تم حذف المطعم بنجاح",
            "restaurantUpdateSuccess":".تم تحديث المطعم بنجاح",
            "restaurantAddSuccess":".تم ادخال المطعم بنجاح",
            "UploadBtn":"اختار شعار",
            "AddMenuBtn": "اضف قائمه جديد",
            "NoMenusAvailable":".لا يوجد قائمه",
            "UpdateMenuLbl":"تحديث القائمه",
            "NewMenuLbl":"قائمه جديده",
            "Menu":"قائمه",
            "menuAddSuccess":".تم ادخال القائمه بنجاح",
            "menuDeleteSuccess":".تم حذف القائمه بنجاح",
            "menuUpdateSucess":".تم تحديث القاءمه بنجاح",
            "CategoriesBtn":"عرض الاقسام",
            "AddCategoryBtn":"اضف قسم جديد",
            "NoCategoryAvailable":".لا يوجد اقسام",
            "Imagelbl":"صوره",
            "newCategoryLbl":"قسم جديد",
            "CategoryAddSuccess":".تم ادخال القسم بنجاح",
            "UpdateCategoryLbl":"تحديث القسم",
            "UploadImageBtn":"اختار الصوره",
            "CategoryupdateSuccess":".تم تحديث القسم بنجاح",
            "CategoryDeleteSuccess":".تم حذف القسم بنجاح",
            "size":"احجام",
            "AddSizeBtn":"اضف حجم جديد",
            "NoSizesAvailable":".لا يوجد احجام",
            "sizeAddSuccess":".تم ادخال الحجم لنجاح",
            "NewSizeLbl":"حجم جديد",
            "UpdateSizeLbl":"تحديث الحجم",
            "UpdateSizeSuccess":".تم تحديث الحجم بنجاح",
            "SizeDeleteSuccess":".تم حذف الحجم بنجاح",
            "NameLengthError":".طول الاسم يجب من 3-40 حرف",
            "SizeLengthError":".طول المقاس يجب من 1-10 حرف",
            "DescLengthError":".طول الوصف يجب من 3-300 حرف",
            "sideItem":"طبق جانبى",
            "value":"قيمه",
            "AddSideItemBtn":"اضف طبق جانبي",
            "NoSideItemsAvailable":".لا يوجد اطباق جانبيه",
            "SideItemAddSuccess":".تم ادخال طبق جانبي بنجاح",
            "NewSideItemLbl":"طبق جانبي جديد",
            "UpdateSideItemLbl":"تحديث طبق جانبي",
            "UpdateSideItemSuccess":".تم تحديث الطبق الجانبيي بنجاح",
            "SideItemDeleteSuccess":".تم حذف طبق الجانبي بنجاح",
            "english":"انجليزي",
            "arabic":"عربي",
            "logoutBtn":"خروج",
            "NoRestaurantDefault":".يجب ادخال مطعم بالانجليزي",
            "NoSizeDefault":".يجب ادخال حجم بالانجليزي",
            "NoMenuDefault":".يجب ادخال قائمة بالانجليزي",
            "NoCategoryDefault":".يجب ادخال قسم بالانجليزي",
            "NoItemDefault":".يجب ادخال منتج بالانجليزي",
            "NoSideItemDefault":".يجب ادخال طبق جانبي بالانجليزي",
            "NoRestaurantTypeDefault":".يجب ادخال نوع مطعم بالانجليزي",
            "RestaurantNotTranslated":".مطعم ليس مترجم بالعربيه",
            "MenuNotTranslated":".قائمة ليس مترجم بالعربيه",
            "status":"الحالة",
            "ItemsBtn":"عرض منتجات",
            "NoItemAvailable":".لا يوجد منتاجات",
            "AddItemBtn":"اضف منتج جديد",
            "CategoryNotTranslated":".قسم ليس مترجم بالعربيه",
            "itemAddSuccess":".تم ادخال منتج بنجاح",
            "NewItemtLbl":"منتج جديد",
            "UpdateItemLbl":"تحديث المنتج",
            "Pricelbl":"السعر",
            "itemDeleteSuccess":".تم حذف المنتج بنجاح",
            "hasSizeLbl":"يوجد احجام؟",
            "selectSizeLbl":"اختار الحجم",
            "hasSideItemLbl":"يوجد اطباق جانبيه؟",
            "selectSideItemLbl":"اختار طبق جتنبي",
            "ItemUpdateSuccess":".تم تحديث المنتج بنجاح",
            "MaxValueLbl":"أقصى قيمه للأطباق الجانبيه",
            "ReadyLbl":"جاهز",
            "RestaurantTypeDeleteMessage":"سيتم حذف كل المطعام لهذا النوع",
            "RestaurantTypeAddSuccess":".تم ادخال نوع المطعم بنجاح",
            "RestaurantTypeUpdateSuccess":".تم تحديث نوع المطعم بنجاح",
            "ConfirmPasswordLbl":"تأكيد كلمه المرور",
            "RestaurantTypeDeleteSuccess":".تم حذف نوع المطعم بنجاح",
            "MaxSideItemValueError":"يجب ان تكون في نطاق اقل ومجموع قيمة الاطباق الجانبيه",
            "waiter":"نادل",
            "AddWaiterBtn":"اضف نادل جديد",
            "NoWaiterAvailable":".لا يوجد نوادل",
            "UserName":"اسم المستخدم",
            "NewWaiterLbl":"نادل جديد",
            "UpdateWaiterLbl":"تحديث نادل",
            "WaiterDeleteSuccess":".تم حذف نوع نادل بنجاح.",
            "WaiterUpdateSuccess":".تم تحديث النادل بنجاح.",
            "WaiterAddSuccess":".تم ادخال نادل بنجاح",

            "background":"خلفيه المنيو",
            "AddbackgroundBtn":"اضافه خلفيه المنيو",
            "NobackgroundAvailable":"لا يوجد خلفيه المنيو", 
            "NewbackgroundLbl":"خلفيه المنيو جديده",
            "UpdatebackgroundLbl":"تعديل خلفيه المنيو",
            "backgroundDeleteSuccess":"خلفيه المنيو.تم حذف",
            "backgroundUpdateSuccess":"خلفيه المنيو تم تحديث ",
            "backgroundAddSuccess":"خلفيه المنيو تم ادخال",
            "CurrentBtn":"الخلفيه الحاليه",
            "NotCurrentBtn":" اختار كخلفيه", 

            "Template":"نموذج",
            "RecommendedMenuImage":"477 x 286 البعد الموصى به",
            "RecommendedCategoryImage":"210 x 137 البعد الموصى به",            
            "RecommendedItemImage1":"423 x 139 البعد الموصى به",
            "RecommendedItemImage2":"112 x 69 البعد الموصى به",  
            "RecommendedItemImage3":"527 x 735 البعد الموصى به",  
            "PriceLengthError":".طول السعر يجب من 1-5ارقام ",
                     
            "RecommendedBackgroundImage":"1920 x 1200 البعد الموصى به",

            "Branch":"فرع",
            "AddBranchBtn":"اضافه فرع",
            "NoBranchAvailable":".لا يوجد فروع",
            "newBranchLbl":"فرع جديد",
            "Title":"لقب",
            "Address":"عنوان",
            "englishAddress":"عنوان انجليزي",
            "arabicAddress":"عنوان عربي",
            "BranchAddSuccess":".تم ادخال الفرع بنجاح",
            "UpdateBranchLbl":"تحديث الفرع",
            "NoBranchDefault":".يجب ادخال فرع بالانجليزي",
            "BranchUpdateSuccess":".تم تحديث الفرع بنجاح",
            "NumOfUsersLbl":"عدد النوادل",
            "waitersLimitConsumedLbl":"المجموع النوادل / المستخدم",
            "consumedAndTotal":"المجموع / المستخدم",
            "TotalRemaining":"المتبقي / المجموع",
            "OkLbl":"تم",
            "pageLbl":"صفحه",
            "selectTemplate":"اختار نموذج لصفحه",
            "selectedTemplates":"النماذج المختاره",
            "startDatelbl":"Start date",
            "endDatelbl":"End date",
            "invalidEmail":"البريد الاكتروني غير صحيح",
            "ConsumedMsg":"المستخدمين",
            "maximumMsg":"الحد الاقصي",
            "OrderItemUpdateSuccess":"تم ترتيب المنتاجات",
            "TemplateUpdateSuccessfuly":".تم تحديث النموذج بنجاح",
            "orderItem":"ترتيب المنتجات",
            "MinimumMsg":"علي الاقل",
            "TelmplateErrorCount":"النموذج يجب يحتوي علي ",

            "selectRestaurantLbl":"اخنار مطاعم",
            "itemlbl":"منتج",
            "likelbl":"Like",
            "dislike":"لم يعجبنى",
            "feedBacksBtn":"تقييمات",
            "Comment":"تعليق",
            "createdate":"تاريخ",
            "Rate":"تقيم",
            "Guest":"زائر",
            "NoFeedBacksAvailable":"لا يوجد تقييمات",
            "BuildingLbl":"مبنى",
            "AddBuildingBtn":"اضف مبنى",
            "NoBuildingLbl":"لا يوجد مباني",
            "NewBuildingLbl":"مبنى جديد",
            "BuildingAddSuccess":"تم اضافة المبنى بنجاح",
            "BuildingUpdateSuccess":"تم تحديث المبنى بنجاح",
            "FloorLbl":"دور",
            "AddFloorBtn":"اضف دور",
            "NoFloorLbl":"لا يوجد ادوار",
            "NewFloorLbl":"دور جديد",
            "FloorAddSuccess":"تم اضافة الدور بنجاح",
            "FloorUpdateSuccess":"تم تحديث الدور بنجاح",
            "buildingName":"اسم المبنى",
            "floorName":"اسم الدور",
            "selectControlLbl":"اختار التحكم",
            "orderControlLbl":"ترتيب التحكم",
            "detailLbl":"تفاصيل",
            "ListOfText":"قائمة النص",
            "ListOfImage":"قائمة الصورة",
            "ListOfTextAndImage":"قائمة النص والصورة",
            "Videos":"فيديو",
            "Available":"متاح",
            "Time":"وقت",
            "ListOfAvailable":"قائمة التوافر",
            "ManageFeatureLbl":"إدارة الخدمة",
            "Add":"أضف",
            "NoData":"لا يوجد بيانات",
            "singleSelect":"اختيار واحد",
            "multiSelect":"اختيار متعدد",
            "NoActionForFeature":"لا يوجد نشاط لهذة الخدمة ",

            "Sunday":"الأحد",
            "Monday":"الإثنين",
            "Tuesday":"الثلاثاء",
            "Wednesday":"الأربعاء",
            "Thursday":"الخميس",
            "Friday":"الجمعة",
            "Saturday":"السبت",
            "From":"من",
            "To":"الي",
            "EditBtn":"تحديث",
            "applyFilterBtn":"تطبيق التصفية",
            "Reports":"تقارير",
            "ExportCsvBtn":"csv تصدير الى",
            "downloadBtn":"تحميل",
            "Assinglbl" : "تخصيص",
            "Action" : "فعل",
        }
        
        $translateProvider.translations('en-us',en_translations);
        
        $translateProvider.translations('ar-eg',ar_translations);
        
        $translateProvider.preferredLanguage(appCONSTANTS.defaultLanguage);
        
        }]);

}());
