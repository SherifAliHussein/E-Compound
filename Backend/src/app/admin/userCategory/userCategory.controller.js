(function () {
    'use strict';

    angular
        .module('home')
        .controller('userCategoryController', ['$rootScope', '$http', '$scope', '$filter', 'UserCategoryPagingResource', 'DeleteUserCategoryResource', 'userCategoryPagingPrepService', '$translate', '$uibModal', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', userCategoryController]);


    function userCategoryController($rootScope, $http, $scope, $filter, UserCategoryPagingResource, DeleteUserCategoryResource, userCategoryPagingPrepService, $translate, $uibModal, $state, $localStorage, authorizationService, appCONSTANTS, ToastService) {
        var vm = this;
     
        $scope.userCategoryList = userCategoryPagingPrepService;
         
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
        
        function confirmationDelete(userCategoryId){
			DeleteUserCategoryResource.deleteUserCategory({userCategoryId:userCategoryId}).$promise.then(function(results) {
                ToastService.show("right","bottom","fadeInUp",$translate.instant('DeleteSuccess'),"success");
                vm.currentPage = 1;
				refreshUserCategories();
            },
            
            function(data, status) {
                ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
        }

        vm.UpdateUserCategory = function (userCategory) {
            $uibModal.open({
                templateUrl: './app/admin/userCategory/templates/editUserCategory.html',
                controller: 'editUserCategoryController',
                controllerAs: 'editUserCategoryCtrl',
                resolve:{
                    UserCategory: function(){return userCategory},
                    callBackFunction:function(){return refreshUserCategories;}
                }
            });
        }
        
        vm.AddUserCategory = function () {
            $uibModal.open({
                templateUrl: './app/admin/userCategory/templates/addUserCategory.html',
                controller: 'addUserCategoryController',
                controllerAs: 'addUserCategoryCtrl',
                resolve:{
                    callBackFunction: function(){return refreshUserCategories;}
                }

            });
          
        }

        vm.DeleteUserCategory = function(userCategoryId){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
                    itemId: function() { return userCategoryId },
                    itemName: function() { return null },
                    message: function() { return null }, 
					callBackFunction:function() { return confirmationDelete; }
				}
				
			});
        }
    }
    

}());