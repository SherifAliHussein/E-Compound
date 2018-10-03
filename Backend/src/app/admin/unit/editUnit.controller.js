(function () {
    'use strict';

    angular
        .module('home')
        .controller('editUnitController', ['$rootScope', '$scope','$uibModalInstance','callBackFunction', 'unitTypesPagingPrepService', 'selectedLanguage', '$filter', '$translate', '$uibModal', '$state', 'UnitResource', 'AddUnitResource', 'UpdateUnitResource', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService','Unit', editUnitController]);


    function editUnitController($rootScope, $scope, $uibModalInstance,callBackFunction, unitTypesPagingPrepService, selectedLanguage, $filter, $translate, $uibModal, $state , UnitResource, AddUnitResource, UpdateUnitResource, $localStorage, authorizationService, appCONSTANTS,ToastService, Unit) {
        var vm = this;
        $scope.unitObj = "";
        vm.unitObj = Unit;
        $scope.selectedLanguage = selectedLanguage;
        $scope.unitTypeList = unitTypesPagingPrepService;

        vm.close = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
		}
     
        $scope.UpdateUnit = function () {
            var newUnit = new UpdateUnitResource();
            newUnit.UnitId =vm.unitObj.unitId;             
            newUnit.Name = vm.unitObj.name;

            if($scope.unitType == undefined){
                newUnit.UnitTypeId = vm.unitObj.unitTypeId;
            }
            else{
                newUnit.UnitTypeId = $scope.unitType.unitTypeId;
            }

            
          
            newUnit.$updateUnit().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('EditSuccess'), "success");
                    callBackFunction();
                    $uibModalInstance.dismiss('cancel');
                    //$state.go('product');

                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
 
    }

})();


