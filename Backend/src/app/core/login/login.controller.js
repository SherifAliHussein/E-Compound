(function() {
    'use strict';

    angular
        .module('home')
        .controller('loginController', ['$rootScope', '$scope','$state','$localStorage','authorizationService','appCONSTANTS',loginController]);
   
    function loginController($rootScope, $scope,$state, $localStorage,authorizationService,appCONSTANTS) {
    
		if ($localStorage.authInfo) {  
			if ($localStorage.authInfo.Role  == "Admin") {
				$state.go('features');

			// } else if ($localStorage.authInfo.Role == "Room") {
            //     $state.go('clientFeatures');

            } else if ($scope.user.role == "Supervisor") {
                $state.go('adminRequests');

            } 
            else if ($scope.user.role == "Receptionist") {
                $state.go('adminRequests');

            }
            else if ($scope.user.role == "Waiter") {
                $state.go('adminRequests');

            } else if ($localStorage.authInfo.Role  == "RestaurantAdmin") {
				$state.go('Menu');

			} 
		}
		else
		{
			 $state.go('login');
		}
	}

}())