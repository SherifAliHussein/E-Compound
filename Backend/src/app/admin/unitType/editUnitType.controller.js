(function () {
    'use strict';

    angular
        .module('home')
        .controller('editUnitTypeController', ['$rootScope', '$scope','$uibModalInstance','callBackFunction', '$filter', '$translate', '$uibModal', '$state', 'UpdateUnitTypeResource', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService','UnitType', editUnitTypeController]);


    function editUnitTypeController($rootScope, $scope, $uibModalInstance,callBackFunction, $filter, $translate, $uibModal, $state, UpdateUnitTypeResource, $localStorage, authorizationService, appCONSTANTS,ToastService, UnitType) {
        var vm = this;
        $scope.language = appCONSTANTS.supportedLanguage; 

        vm.unitTypeObj = UnitType;

        vm.close = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
        }

        vm.backdrop = function(){
            $uibModalInstance.dismiss('cancel');
            callBackFunction();
        }
        
        $scope.UpdateContact = function () {
            var newUnitType = new UpdateUnitTypeResource();
            newUnitType.TitleDictionary = vm.unitTypeObj.titleDictionary;
            newUnitType.UnitTypeId = vm.unitTypeObj.unitTypeId;           
            newUnitType.Limit = vm.unitTypeObj.limit;           
           
            newUnitType.$updateUnitType().then(
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


