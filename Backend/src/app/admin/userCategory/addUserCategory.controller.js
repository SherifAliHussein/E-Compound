(function () {
    'use strict';

    angular
        .module('home')
        .controller('addUserCategoryController', ['$rootScope', '$scope', '$filter', 'AddUserCategoryResource','UserCategoryPagingResource', 'callBackFunction', '$translate', '$uibModal','$uibModalInstance', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', addUserCategoryController]);


    function addUserCategoryController($rootScope, $scope, $filter, AddUserCategoryResource, UserCategoryPagingResource, callBackFunction, $translate, $uibModal, $uibModalInstance, $state, $localStorage, authorizationService, appCONSTANTS, ToastService) {
        var vm = this;
        $scope.userCategoryList;
        vm.titleDictionary;
        $scope.getId="0";
        $scope.language = appCONSTANTS.supportedLanguage;
        $scope.changeId=function (val){
            $scope.getId=val;
        }
        
        function refreshUserCategories(){
			var k = UserCategoryPagingResource.getAllPagingUserCategories({page:vm.currentPage}).$promise.then(function(results) {
				$scope.userCategoryList = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
        }

		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshUserCategories();
        }

        vm.close = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
		}
    
        $scope.AddNewUserCategory = function () {
            var newUserCategory = new AddUserCategoryResource();
            newUserCategory.TitleDictionary = vm.titleDictionary;

            newUserCategory.$addUserCategory().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddSuccess'), "success");
                    callBackFunction();
                    $uibModalInstance.dismiss('cancel');

                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
    }

}());