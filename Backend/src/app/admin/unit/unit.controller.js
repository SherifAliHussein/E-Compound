(function () {
    'use strict';

    angular
        .module('home')
        .controller('unitController', ['$rootScope', '$http', '$scope', '$filter', 'UnitPagingResource', 'DeleteUnitResource', 'unitPagingPrepService', 'unitTypesPagingPrepService', '$translate', '$uibModal', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', unitController]);


    function unitController($rootScope, $http, $scope, $filter, UnitPagingResource, DeleteUnitResource, unitPagingPrepService, unitTypesPagingPrepService, $translate, $uibModal, $state, $localStorage, authorizationService, appCONSTANTS, ToastService) {
        var vm = this;
     
        $scope.unitList = unitPagingPrepService;
        $scope.unitTypeList = unitTypesPagingPrepService;
         
        function refreshUnits(){
			var k = UnitPagingResource.getAllPagingUnits({page:vm.currentPage}).$promise.then(function(results) {
				//vm.Now = $scope.getCurrentTime();	
				$scope.unitList = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
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
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
        }

        vm.UpdateUnit = function (unit) {
            // var currencyId = currency.currencyId;
            $uibModal.open({
                templateUrl: './app/admin/unit/templates/editUnit.html',
                controller: 'editUnitController',
                controllerAs: 'editUnitCtrl',
                resolve:{
                    Unit: function(){return unit},
                    callBackFunction:function(){return refreshUnits;}
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
                    //selectedLanguage : function(){return selectedLanguage;}
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