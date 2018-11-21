(function () {
    'use strict';

    angular
        .module('home')
        .controller('TicketController', ['$http', 'CategoryResource', 'featureDetailPrepService', 'TicketResource', '$translate', '$scope', 'appCONSTANTS', '$stateParams', '$state', '_', 'authenticationService', 'authorizationService', '$localStorage', 'ToastService', 'RequestResource', TicketController])

    function TicketController($http, CategoryResource, featureDetailPrepService, TicketResource, $translate, $scope, appCONSTANTS, $stateParams, $state, _, authenticationService, authorizationService, $localStorage, ToastService, RequestResource) {

        var vm = this;
        vm.featureMode = true;
        $scope.$parent.globalInfo = {};
        $scope.$parent.globalInfo.featureMode = false;
        /*$scope.userCategoryList = CategoryResource; */
        $scope.selectedCategory = "";
        $scope.feature = featureDetailPrepService;
        console.log($scope.feature)
        refreshFeatures()
        function refreshFeatures() {
            var k = CategoryResource.UserCategories().$promise.then(function (results) {

                debugger
                $scope.userCategoryList = results;
            },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
        }

        $scope.createTicket = function (index) {

            debugger;
            var newTicket = new Object(); 
            newTicket.featureId = $scope.feature.featureId;
            newTicket.title = $scope.title;
            newTicket.userCategory = $scope.selectedCategory.userCategoryId;
            newTicket.comment = $scope.comment;

            var model = new FormData();
            model.append('data', JSON.stringify(newTicket));
            vm.files.forEach(function (element) {
                model.append('file', element);
            }, this);

            $http({
                method: 'POST',
                url: appCONSTANTS.API_URL + 'TicketRequests/',
                useToken: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity,
                data: model
            }).then(
                function (data, status) {
                    vm.isChanged = false;
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddedSuccessfully'), "success");
                    $state.go('Features')

                },
                function (data, status) {
                    vm.isChanged = false;
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }

        vm.LoadUploadImages = function () {
            $("#file").click();
            vm.fileExist = false;

        }
        vm.files = [];
        $scope.AddFile = function (element) {
            var imageFile = element[0];

            var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

            vm.files.forEach(function (file) {
                if (file.name === imageFile.name) {
                    vm.fileExist = true;
                    ToastService.show("right", "bottom", "fadeInUp", "File is already exist", "error");
                    return
                }
            }, this);
            if (imageFile && imageFile.size >= 0 && ((imageFile.size / (1024 * 1000)) < 2)) {

                if (allowedImageTypes.indexOf(imageFile.type) !== -1) {
                    if (!vm.fileExist) {
                        $scope.TicketForm.$dirty = true;
                        $scope.$apply(function () {

                            vm.files.push(imageFile);
                            var reader = new FileReader();

                            reader.onloadend = function () {
                                $scope.$apply();
                            };
                            if (imageFile) {
                                reader.readAsDataURL(imageFile);
                            }
                        })
                    }
                    else {
                        $("#file").val('');
                        $scope.$apply()
                    }
                } else {
                    $("#file").val('');
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('imageTypeError'), "error");
                }

            } else {
                if (imageFile) {
                    $("#file").val('');
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('imgaeSizeError'), "error");
                }

            }


        }

        vm.removeFile = function (index) {
            vm.files.splice(index, 1);
        }
    }


}());
