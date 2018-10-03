(function () {
    'use strict';

    angular
        .module('home')
        .controller('addUnitTypeController', ['$rootScope', '$scope', '$filter', 'AddUnitTypeResource','UnitTypePagingResource', 'callBackFunction', '$translate', '$uibModal','$uibModalInstance', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', addUnitTypeController]);


    function addUnitTypeController($rootScope, $scope, $filter, AddUnitTypeResource, UnitTypePagingResource, callBackFunction, $translate, $uibModal, $uibModalInstance, $state, $localStorage, authorizationService, appCONSTANTS, ToastService) {
        var vm = this;
        $scope.unitTypeList;
        vm.titleDictionary;
        $scope.getId="0";
        $scope.language = appCONSTANTS.supportedLanguage;
       // console.log(appCONSTANTS.supportedLanguage);
        $scope.changeId=function (val){
            $scope.getId=val;
        }
        
        function refreshUnitTypes(){
			var k = UnitTypePagingResource.getAllPagingUnitTypes({page:vm.currentPage}).$promise.then(function(results) {
				//vm.Now = $scope.getCurrentTime();	
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

        vm.close = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
		}
    
        $scope.AddNewUnitType = function () {
            var newUnitType = new AddUnitTypeResource();
            newUnitType.TitleDictionary = vm.titleDictionary;
            newUnitType.Limit = $scope.Limit;

            newUnitType.$addUnitType().then(
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