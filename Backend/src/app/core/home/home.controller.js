(function() {
    'use strict';

    angular
        .module('home')
        .controller('homeCtrl', ['$rootScope','$translate', '$scope', 'appCONSTANTS',  '$state',  '_', 'authenticationService', 'authorizationService', '$localStorage', homeCtrl])
       
    function homeCtrl($rootScope, $translate, $scope, appCONSTANTS, $state, _,authenticationService, authorizationService,$localStorage) {
        
        var vm=this;
        $scope.emailEmpty = false;
        $scope.passwordEmpty = false;
		$scope.languages = [{
            id:"en-us",
            label:"english"
        },
        {
            id:"ar-eg",
            label:"arabic"
        }];
		if($localStorage.language == null){
            $scope.selectedLanguage = $scope.languages[0].id;
            $localStorage.language = $scope.selectedLanguage;
        }
        else
            $scope.selectedLanguage = $localStorage.language;
            
        $translate.use($scope.selectedLanguage); 
		$scope.init =
            function() {
                $scope.user = authorizationService.getUser();
                
            }
        $scope.init();
		
        $scope.submit = function(username, password) {
           
            authorizationService.isPasswordchanged=false;
            $('#passwordChanged').hide();
          //  $('#userInActivated').hide();
            if (!username)
                $scope.emailEmpty = true;
            if (!password)
                $scope.passwordEmpty = true;;
            if (username && password) {
                $scope.afterSubmit = false;
                $scope.emailEmpty = $scope.passwordEmpty = false;
                authenticationService.authenticate(username, password).then(loginSuccess,loginFailed)
                    //.error(loginFailed);;
            } else {
                $scope.afterSubmit = false;
            }
        };
		
        $scope.reloadPage = true;
        $rootScope.$on('$stateChangeStart', function(e, toState, toParams, fromState, fromParams) {
            if(fromState.name != "" && $scope.reloadPage){
                    e.preventDefault();
                    $scope.reloadPage = false;
                    $state.go(toState.name,toParams, { reload: true });
                }     
        });

		$scope.$watch(function () { return $localStorage.authInfo; },function(newVal,oldVal){
		   if(oldVal!=undefined && newVal === undefined && $localStorage.authInfo == undefined){
			 console.log('logout'); 
			   $state.go('login');
		  }
		  if(oldVal===undefined && newVal !== undefined&&$localStorage.authInfo != undefined){
			 console.log('login'); 
					$scope.user = authorizationService.getUser();
					loginSuccess()
			// authorizationService.isLoggedIn() && !location.href.contains('connect')
		  }
		})
        function loginSuccess(response) {
            $scope.afterSubmit = false;
            $scope.invalidLoginInfo = false;
            $scope.inActiveUser = false;
            $scope.user = authorizationService.getUser();
            $scope.restaurantName = "";
            if ($scope.user.role == "Admin") {
                $state.go('features');

            } 
            // else if ($scope.user.role == "Room") {
            //     $state.go('clientFeatures');

            // }
            else if ($scope.user.role == "Supervisor") {
                $state.go('adminRequests');

            } 
            else if ($scope.user.role == "Receptionist") {
                $state.go('adminRequests');

            } 
            else if ($scope.user.role == "Waiter") {
                $state.go('adminRequests');

            }  else if ($scope.user.role  == "RestaurantAdmin") {
                $state.go('Menu');
                // RestaurantInfoResource.getRestaurantInfo().$promise.then(function(results) {
                //    $scope.restaurantName = results.restaurantName;
                // },
                // function(data, status) {
                // });

            }
            else  {
                authorizationService.logout();
                $state.go('login');
            } 

        }

        function loginFailed(response) {
            $scope.afterSubmit = true;
            
            // $scope.invalidLoginInfo = true;
            if (response) {
                if (response.data.error == "invalid grant") {
                    $scope.invalidLoginInfo = true;
                    $scope.inActiveUser = false;
                    $scope.AccountDeActivated = false;
                }
                if (response.data.error == "user deleted" || response.data.error == "Account deleted") {
                    $scope.invalidLoginInfo = false;
                    $scope.inActiveUser = true;
                    $scope.AccountDeActivated = false;                    
                }
                if (response.data.error == "user deactivated") {
                    $scope.invalidLoginInfo = false;
                    $scope.inActiveUser = false;
                    $scope.AccountDeActivated = true;
                }
            }
            if (response == null) {
                $scope.invalidLoginInfo = false;
                $scope.inActiveUser = true;
            }
        }

        $scope.logout = function() {
            authorizationService.logout();
            $scope.restaurantName = "";
            $translate.use(appCONSTANTS.defaultLanguage); 
            $localStorage.language = appCONSTANTS.defaultLanguage;
            $scope.selectedLanguage = appCONSTANTS.defaultLanguage;            
            $state.go('login');
        }
        $scope.reset = function() {
            $scope.invalidLoginInfo = false;
            $scope.inActiveUser = false;
        }
        $scope.isLoggedIn = function() {
            return authorizationService.isLoggedIn();
        }
		$scope.changeLanguage = function(language){
			$scope.selectedLanguage = language;
			$localStorage.language = $scope.selectedLanguage;
            // $state.reload();
            $translate.use(language); 
		}
		$scope.getCurrentTime = function(){
			return (new Date()).getTime()
		}
		
		
    }

    
}());
