(function () {
    'use strict';

    angular
        .module('home')
        .controller('addnotificationController', ['$scope', 'AddnotificationResource', '$translate', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', 'ToastService', addnotificationController]);


    function addnotificationController($scope, AddnotificationResource, $translate, $state, $localStorage, authorizationService, appCONSTANTS, ToastService) {
        var vm = this;
        $scope.notificationList;
        vm.titleDictionary;
        $scope.getId = "0";
        $scope.language = appCONSTANTS.supportedLanguage;
        $scope.changeId = function (val) {
            $scope.getId = val;
        }

        function refreshnotifications() {
            var k = notificationPagingResource.getAllPagingnotifications({ page: vm.currentPage }).$promise.then(function (results) {
                $scope.notificationList = results;
            },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
        }

        vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshnotifications();
        }

        $scope.AddNewnotification = function () {
            var newnotification = new AddnotificationResource();
            newnotification.title = $scope.title;
            newnotification.message = $scope.message;

            newnotification.$send().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddSuccess'), "success");
                    $scope.title="";
                    $scope.message="";
                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
    }

}());