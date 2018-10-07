(function () {
    'use strict';

    angular
        .module('home')
        .controller('editUserCategoryController', ['$rootScope', '$scope','$uibModalInstance','callBackFunction', '$filter', '$translate', '$uibModal', '$state', 'UpdateUserCategoryResource', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService','UserCategory', editUserCategoryController]);


    function editUserCategoryController($rootScope, $scope, $uibModalInstance,callBackFunction, $filter, $translate, $uibModal, $state, UpdateUserCategoryResource, $localStorage, authorizationService, appCONSTANTS,ToastService, UserCategory) {
        var vm = this;
        $scope.language = appCONSTANTS.supportedLanguage;

        vm.userCategoryObj = UserCategory;

        vm.close = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
        }

        vm.backdrop = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
        }
        
        $scope.UpdateContact = function () {
            var newUserCategory = new UpdateUserCategoryResource();
            newUserCategory.TitleDictionary = vm.userCategoryObj.titleDictionary;
            newUserCategory.UserCategoryId = vm.userCategoryObj.userCategoryId;           
           
            newUserCategory.$updateUserCategory().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('EditSuccess'), "success");
                    $uibModalInstance.dismiss('cancel');
                    callBackFunction();

                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
 
    }

})();


