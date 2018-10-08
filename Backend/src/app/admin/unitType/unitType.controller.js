(function () {
    'use strict';

    angular
        .module('home')
        .controller('unitTypeController', ['$rootScope', '$http', '$scope', '$filter', 'UnitTypePagingResource', 'DeleteUnitTypeResource', 'unitTypePagingPrepService', '$translate', '$uibModal', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', unitTypeController]);


    function unitTypeController($rootScope, $http, $scope, $filter, UnitTypePagingResource, DeleteUnitTypeResource, unitTypePagingPrepService, $translate, $uibModal, $state, $localStorage, authorizationService, appCONSTANTS, ToastService) {
        var vm = this;
     
        $scope.unitTypeList = unitTypePagingPrepService;
         
        function refreshUnitTypes(){
			var k = UnitTypePagingResource.getAllPagingUnitTypes({page:vm.currentPage}).$promise.then(function(results) {
				$scope.unitTypeList = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
        }

        vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshUnitTypes();
		}
        
        function confirmationDelete(unitTypeId){
			DeleteUnitTypeResource.deleteUnitType({unitTypeId:unitTypeId}).$promise.then(function(results) {
                ToastService.show("right","bottom","fadeInUp",$translate.instant('DeleteSuccess'),"success");
                vm.currentPage = 1;
				refreshUnitTypes();
            },
            
            function(data, status) {
                ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
        }

        vm.UpdateUnitType = function (unitType) {
            $uibModal.open({
                templateUrl: './app/admin/unitType/templates/editUnitType.html',
                controller: 'editUnitTypeController',
                controllerAs: 'editUnitTypeCtrl',
                resolve:{
                    UnitType: function(){return unitType},
                    callBackFunction:function(){return refreshUnitTypes;}
                }
            });
        }
        
        vm.AddUnitType = function () {
            $uibModal.open({
                templateUrl: './app/admin/unitType/templates/addUnitType.html',
                controller: 'addUnitTypeController',
                controllerAs: 'addUnitTypeCtrl',
                resolve:{
                    callBackFunction: function(){return refreshUnitTypes;}
                }

            });
          
        }

        vm.DeleteUnitType = function(unitTypeId){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
                    itemId: function() { return unitTypeId },
                    itemName: function() { return null },
                    message: function() { return null }, 
					callBackFunction:function() { return confirmationDelete; }
				}
				
			});
        }
    }
    

}());