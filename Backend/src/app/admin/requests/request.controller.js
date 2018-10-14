(function () {
    'use strict';

    angular
        .module('home')
        .controller('adminRequestController', ['$scope', 'UserCategoryPrepService', 'TechnicanResource', 'appCONSTANTS', '$uibModal', 'RequestResource'
            , 'requestsPrepService', '$filter', 'ToastService', 'authorizationService', 'FeatureResource', 'roomsNamePrepService', 'featuresNamePrepService', adminRequestController])

    function adminRequestController($scope, UserCategoryPrepService, TechnicanResource, appCONSTANTS, $uibModal, RequestResource
        , requestsPrepService, $filter, ToastService, authorizationService, FeatureResource, roomsNamePrepService, featuresNamePrepService) {

        var vm = this;
        vm.requests = requestsPrepService;
        vm.rooms = [{ roomId: 0, roomName: "All rooms" }];
        vm.selectedRoom = vm.rooms[0];
        vm.rooms = vm.rooms.concat(roomsNamePrepService);
        console.log(vm.requests)
        $scope.userCategoryList = UserCategoryPrepService;
        console.log($scope.userCategoryList)
        vm.features = [{ featureId: 0, featureNameDictionary: { 'en-us': "All features", 'ar-eg': "كل الميزات" } }];
        vm.selectedFeature = vm.features[0];
        vm.features = vm.features.concat(featuresNamePrepService);



        _.forEach(vm.requests.results, function (request) {
            var indexCategory = $scope.userCategoryList.indexOf($filter('filter')($scope.userCategoryList, { 'userCategoryId': request.userCategory }, true)[0]);
            vm.selectedCategory = $scope.userCategoryList[indexCategory];
            if (indexCategory > -1) { request.category = vm.selectedCategory.titleDictionary['en-us']; }

            request.createTime = request.createTime + "Z";
            request.createTime = $filter('date')(new Date(request.createTime), "dd/MM/yyyy hh:mm a");
            request.modifyTime = request.modifyTime + "Z";
            request.modifyTime = $filter('date')(new Date(request.modifyTime), "dd/MM/yyyy hh:mm a");
            if (request.requestTime != null) {
                request.requestTime = request.requestTime + "Z";
                request.requestTime = $filter('date')(new Date(request.requestTime), "dd/MM/yyyy hh:mm a");
            }
            request.requestDetails.forEach(function (element) {
                if (element.from != null) {
                    element.from = element.from + "Z";
                    element.from = $filter('date')(new Date(element.from), "dd/MM/yyyy hh:mm a");
                }
                if (element.to != null) {
                    element.to = element.to + "Z";
                    element.to = $filter('date')(new Date(element.to), "dd/MM/yyyy hh:mm a");
                }
            }, this);
        });
        $('.pmd-sidebar-nav>li>a').removeClass("active")
        var user = authorizationService.getUser();

        if (user.role === 'Admin')
            $($('.pmd-sidebar-nav').children()[3].children[0]).addClass("active")
        else if (user.role === 'Supervisor')
            $($('.pmd-sidebar-nav').children()[0].children[0]).addClass("active")
        else if (user.role === 'Technician')
            $($('.pmd-sidebar-nav').children()[0].children[0]).addClass("active")
        else
            $($('.pmd-sidebar-nav').children()[1].children[0]).addClass("active")

        function refreshRequests() {
            vm.isLoading = true;
            var from = ""
            if ($('#datepicker-start').val() != "")
                from = (new Date($('#datepicker-start').val())).toISOString().replace('Z', '');

            var to = ""
            if ($('#datepicker-end').val() != "")
                to = (new Date($('#datepicker-end').val())).toISOString().replace('Z', '');
            var k = RequestResource.getAllRequest({
                page: vm.currentPage, roomId: vm.selectedRoom.roomId
                , featureId: vm.selectedFeature.featureId, from: from, to: to
            }).$promise.then(function (results) {

                vm.requests = results;
                _.forEach(vm.requests.results, function (request) {
                    request.createTime = request.createTime + "Z";
                    request.createTime = $filter('date')(new Date(request.createTime), "dd/MM/yyyy hh:mm a");
                    request.modifyTime = request.modifyTime + "Z";
                    request.modifyTime = $filter('date')(new Date(request.modifyTime), "dd/MM/yyyy hh:mm a");
                    if (request.requestTime != null) {
                        request.requestTime = request.requestTime + "Z";
                        request.requestTime = $filter('date')(new Date(request.requestTime), "dd/MM/yyyy hh:mm a");
                    }
                    request.requestDetails.forEach(function (element) {
                        if (element.from != null) {
                            element.from = element.from + "Z";
                            element.from = $filter('date')(new Date(element.from), "dd/MM/yyyy hh:mm a");
                        }
                        if (element.to != null) {
                            element.to = element.to + "Z";
                            element.to = $filter('date')(new Date(element.to), "dd/MM/yyyy hh:mm a");
                        }
                    }, this);

                });
                vm.isLoading = false;
            },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                    vm.isLoading = false;
                });
        }

        vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshRequests();

        }
        vm.showMore = function (element) {
            $(element.currentTarget).toggleClass("child-table-collapse");
        }
        function ApproveRequest(requestId, requestDetail) {
            var requestApproval = new RequestResource();
            requestApproval.requestDetails = requestDetail;
            requestApproval.status = "Approved";

            requestApproval.$Approve({ requestId: requestId }).then(
                function (data, status) {
                    refreshRequests()
                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }

        vm.Approve = function (featureId, requestId) {
            ApproveRequest(requestId);
        }
        vm.Reject = function (requestId) {
           // rejectRequest(requestId); 
        }
        // function rejectRequest(requestId, requestDetail) {
        //     var requestreject = new RequestResource();
        //     requestreject.technicianComment = "hamdad";
        //     requestreject.status = "Rejectd";

        //     requestreject.$reject({ requestId: requestId }).then(
        //         function (data, status) {
        //             refreshRequests()
        //         },
        //         function (data, status) {
        //             ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
        //         }
        //     );
        // }

        vm.openDialog = function (categoryId, requestId) {
            TechnicanResource.GetTechnicans({ categoryId: categoryId }).$promise.then(function (results) {
                var modalContent = $uibModal.open({
                    templateUrl: './app/admin/requests/templates/assignTicket.html',
                    controller: 'assignDialogController',
                    controllerAs: 'assignDlCtrl',
                    resolve: {
                        Technicans: function () { return results },
                        selectedLanguage: function () { return $scope.selectedLanguage; },
                        requestId: function () { return requestId; }
                    }

                });
            },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });

        }
    }

}());
