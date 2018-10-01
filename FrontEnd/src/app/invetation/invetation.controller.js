(function () {
    'use strict';

    angular
        .module('home')
        .controller('invetationController', ['$rootScope','invetationResource', '$translate', '$scope', 'appCONSTANTS', '$stateParams', '$state', '_', 'authenticationService', 'authorizationService', '$localStorage', 'ToastService', 'RequestResource', invetationController])

    function invetationController($rootScope,invetationResource, $translate, $scope, appCONSTANTS, $stateParams, $state, _, authenticationService, authorizationService, $localStorage, ToastService, RequestResource) {
    
        var vm = this;
        $scope.dateinvetation=new Date();
        $scope.countList = [];

        $scope.addNewChoice = function () {
            var newItemNo = $scope.countList.length + $scope.count;
            for (var i = 0; i < $scope.count; i++) {
                $scope.countList.push({
                    'name': newItemNo.id,
                    'idtype': newItemNo.id,
                    'id': newItemNo.id,
                });



            }
        };

        $scope.removeChoice = function (ind) {
            $scope.countList.splice(ind, 1);
        };


        $scope.createInvetation = function (index) {
           
           debugger;
            var newObj = new RequestResource();
            newObj.featureId = 8; 
            newObj.count = $scope.count; 
            newObj.comment =$scope.comment; 
            newObj.invetationType = $scope.dateinvetation; 
            newObj.requestDetails = []; 
            $scope.countList.forEach(element => {
                newObj.requestDetails.push(
                    {
                       idNumber:element.id,
                       idType:element.idtype,
                        name:element.name,
                    });
            });
            newObj.$create().then(
                function (data, status) { 
                 ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddedSuccessfully'), "success"); 
                      $state.go('Features'); 
                },
                function (data, status) {       

                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }

        $scope.DateChange = function (date) { 
           debugger; $scope.dateinvetation=date;
              checkAvailable();
        } 

        function checkAvailable() {
            var k = invetationResource.CheckUnitInvetationLimit({ featureId: 8,
                datetime: $scope.dateinvetation.toISOString(),
                limit: $scope.count
             }).$promise.then(function (results) {
              console.log(results);  
               ToastService.show("right", "bottom", "fadeInUp", results.message, "error");
            },
            function (data, status) {
                ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
            });
        }


    }


}());
