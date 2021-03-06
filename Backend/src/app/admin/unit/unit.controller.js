(function () {
    'use strict';

    angular
        .module('home')
        .controller('unitController', ['$rootScope', '$http', '$scope', '$filter', 'UnitPagingResource', 'DeleteUnitResource', 'unitPagingPrepService', 'unitTypesPagingPrepService', '$translate', '$uibModal', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', 'unitLimitPrepService', 'AdminUnitsLimitResource', unitController]);


    function unitController($rootScope, $http, $scope, $filter, UnitPagingResource, DeleteUnitResource, unitPagingPrepService, unitTypesPagingPrepService, $translate, $uibModal, $state, $localStorage, authorizationService, appCONSTANTS, ToastService, unitLimitPrepService, AdminUnitsLimitResource) {
        var vm = this;
     
        $scope.unitList = unitPagingPrepService;
        vm.unitsLimit = unitLimitPrepService;
         
        function refreshUnits(){
			var k = UnitPagingResource.getAllPagingUnits({page:vm.currentPage}).$promise.then(function(results) {
				$scope.unitList = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });

            AdminUnitsLimitResource.getUnitsLimitAndConsumed().$promise.then(function (results) {
				vm.unitsLimit = results;
			},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
				});
        }

        vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshUnits();
		}
        
        function confirmationDelete(unitId){
			DeleteUnitResource.deleteUnit({unitId:unitId}).$promise.then(function(results) {
                ToastService.show("right","bottom","fadeInUp",$translate.instant('DeleteSuccess'),"success");
                vm.currentPage = 1;
				refreshUnits();
            },
            
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
        }

        vm.UpdateUnit = function (unit) {
            $uibModal.open({
                templateUrl: './app/admin/unit/templates/editUnit.html',
                controller: 'editUnitController',
                controllerAs: 'editUnitCtrl',
                resolve:{
                    Unit: function(){return unit},
                    callBackFunction:function(){return refreshUnits;},
                    selectedLanguage : function(){return $scope.selectedLanguage;},
                    unitTypesPagingPrepService : function(){return unitTypesPagingPrepService;}
                }
            });
        }
        
        vm.AddUnit = function () {
            $uibModal.open({
                templateUrl: './app/admin/unit/templates/addUnit.html',
                controller: 'addUnitController',
                controllerAs: 'addUnitCtrl',
                resolve:{
                    callBackFunction: function(){return refreshUnits;},
                    unitTypesPagingPrepService : function(){return unitTypesPagingPrepService;},
                    selectedLanguage : function(){return $scope.selectedLanguage;}
                }

            });
          
        }

        vm.DeleteUnit = function(unitId){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
                    itemId: function() { return unitId },
                    itemName: function() { return null },
                    message: function() { return null }, 
					callBackFunction:function() { return confirmationDelete; }
				}
				
			});
        }
    }
    

}());