(function () {
    'use strict';

    angular
        .module('home')
        .controller('cartController', ['$rootScope', '$translate', '$scope', 'appCONSTANTS', '$stateParams', '$state', '_', 'authenticationService', 'authorizationService', '$localStorage', 'ToastService',  'totalCartService','ResturantPrepService','RequestResource', cartController])

    function cartController($rootScope, $translate, $scope, appCONSTANTS, $stateParams, $state, _, authenticationService, authorizationService, $localStorage, ToastService, totalCartService,ResturantPrepService,RequestResource) {

       
        var vm = this;
        
  $scope.choicesA = [{
    id: 'choice1',
    choicesB:[]
  } ];

        $scope.addNewChoice = function() {
            var newItemNo = $scope.choicesA.length + 1;
            $scope.choicesA.push({
              'id': 'choice' + newItemNo 
            });
          };
        
          $scope.removeChoice = function(ind) {
            $scope.choicesA.splice(ind,1);
          };
        
        
        $scope.createInvetation = function (index) {
            
        }
     
         




    }


}());
