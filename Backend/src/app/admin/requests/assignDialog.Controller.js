(function () {
    'use strict';

    angular
        .module('home')
        .controller('assignDialogController', ['$scope', '$uibModalInstance', 'AssignResource','$translate', 'ToastService', 'Technicans', 'selectedLanguage','requestId', assignDialogController])

    function assignDialogController($scope, $uibModalInstance,AssignResource, $translate, ToastService, Technicans, selectedLanguage,requestId) {
        var vm = this;
        vm.Technicans = Technicans.results;
        vm.SelectedTechnican ="";
        console.log(vm.Technicans)
        vm.close = function () {
            $uibModalInstance.dismiss('cancel');
        }
        vm.requestId = requestId
        vm.selectedLanguage = selectedLanguage
        vm.AddNewassign = function () {
            debugger; 
            var newassign = new AssignResource();
            newassign.requestId = vm.requestId;
            newassign.technician = vm.SelectedTechnican;
            newassign.supervisorComment = vm.SupervisorComment; 
            newassign.$create().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('assignAddSuccess'), "success");
                    refreshRequests();
                    $uibModalInstance.dismiss('cancel');
                    //callBackFunction();
                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
    }
}());
