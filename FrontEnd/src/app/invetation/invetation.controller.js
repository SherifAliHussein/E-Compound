(function () {
    'use strict';

    angular
        .module('home')
        .controller('invetationController', ['$rootScope','featureDetailPrepService','invetationResource', '$translate', '$scope', 'appCONSTANTS', '$stateParams', '$state', '_', 'authenticationService', 'authorizationService', '$localStorage', 'ToastService', 'RequestResource', invetationController])

    function invetationController($rootScope,featureDetailPrepService,invetationResource, $translate, $scope, appCONSTANTS, $stateParams, $state, _, authenticationService, authorizationService, $localStorage, ToastService, RequestResource) {
    
        var vm = this;
        
        vm.featureMode = true;
        $scope.$parent.globalInfo= {};
        $scope.$parent.globalInfo.featureMode = false;
        $scope.feature=featureDetailPrepService;
        $scope.dateinvetation=new Date();
        $scope.countList = [];
        $scope.showCount=false;
        $scope.type="Single";
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
            newObj.featureId =$scope.feature.featureId; 
            newObj.count = $scope.count; 
            newObj.comment =$scope.comment; 
            newObj.requestTime = $scope.dateinvetation; 
            newObj.InvetationType = $scope.type; 
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
         if(date != null) 
               $scope.showCount=true;
             else
                $scope.showCount=false;

                if($scope.count ==null)
                { 
                    ToastService.show("right", "bottom", "fadeInUp", "please type the count", "error");
                    return;
                }
            $scope.dateinvetation=date;
              checkAvailable();
        } 

        function checkAvailable() {
            var k = invetationResource.CheckUnitInvetationLimit({ featureId: 1,
                datetime: $scope.dateinvetation.toISOString(),
                limit: $scope.count
             }).$promise.then(function (results) {
              console.log(results);  
              if  (results.isLimit==false)
                ToastService.show("right", "bottom", "fadeInUp", "you exceed the limit", "error");
            },
            function (data, status) {
                ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
            });
        }


    }


}());
