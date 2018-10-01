(function () {
    'use strict';

    angular
        .module('home')
        .controller('addUnitController', ['$rootScope', '$scope', '$filter', 'unitTypesPagingPrepService', 'AddUnitResource','UnitPagingResource', 'callBackFunction', '$translate', '$uibModal','$uibModalInstance', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', addUnitController]);


    function addUnitController($rootScope, $scope, $filter, unitTypesPagingPrepService, AddUnitResource, UnitPagingResource, callBackFunction, $translate, $uibModal, $uibModalInstance, $state, $localStorage, authorizationService, appCONSTANTS, ToastService) {
        var vm = this;
        $scope.unitList;
      //  $scope.selectedLanguage = selectedLanguage;
        $scope.unitTypeList = unitTypesPagingPrepService;
        $scope.language = appCONSTANTS.supportedLanguage;

        $scope.changeId=function (val){
            $scope.getId=val;
        }
        
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

        vm.close = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
		}
    
        $scope.AddNewUnit = function () {
            var newUnit = new AddUnitResource();
            newUnit.Name = $scope.Name;

            newUnit.$addUnit().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddSuccess'), "success");
                    callBackFunction();
                    //close popup after save
                    $uibModalInstance.dismiss('cancel');

                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
    }

}());