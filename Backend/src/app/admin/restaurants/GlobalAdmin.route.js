(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
              .state('restaurantType', {
					url: '/restaurantType',
                    templateUrl: './app/admin/restaurants/templates/restaurantType.html',
                    controller: 'restaurantTypeController',
                    'controllerAs': 'restaurantTypeCtrl',
                    data: {
                        permissions: {
                            only: ['Admin'],
                           redirectTo: 'root'
                        },
                        displayName: 'restaurantType'
                    },
                    resolve: {
                        restaurantTypesPrepService: restaurantTypesPrepService
                    }
                 
                })
                .state('newRestaurantType', {
					url: '/newRestaurantType',
                    templateUrl: './app/admin/restaurants/templates/newType.html',
                    controller: 'restaurantTypeDialogController',
                    'controllerAs': 'restTypeDlCtrl',
                    data: {
                        permissions: {
                            only: ['Admin'],
                           redirectTo: 'root'
                        },
                        displayName: 'restaurantType'
                    }
                 
                })
                .state('editRestaurantType', {
					url: '/restaurantType/:restaurantTypeId',
                    templateUrl: './app/admin/restaurants/templates/editType.html',
                    controller: 'editRestaurantTypeDialogController',
                    'controllerAs': 'editRestTypeDlCtrl',
                    data: {
                        permissions: {
                            only: ['Admin'],
                           redirectTo: 'root'
                        },
                        displayName: 'restaurantType'
                    },
                    resolve: {
                        restaurantTypePrepService: restaurantTypePrepService
                    }
                 
                })
				.state('restaurants', {
					url: '/restaurants',
                    templateUrl: './app/admin/restaurants/templates/restaurant.html',
                    controller: 'restaurantController',
                    'controllerAs': 'restaurantCtrl',
                    data: {
                        permissions: {
                            only: ['Admin'],
                           redirectTo: 'root'
                        },
                        displayName: 'restaurants'
                    },
                    resolve: {
                        restaurantsPrepService: restaurantsPrepService,
                        waitersLimitPrepService: waitersLimitPrepService
                    }
                 
                })
				.state('newRestaurant', {
					url: '/newRestaurant',
                    templateUrl: './app/admin/restaurants/templates/newRestaurant.html',
                    controller: 'newRestaurantController',
                    'controllerAs': 'rewRestCtrl',
                    data: {
                        permissions: {
                            only: ['Admin'],
                           redirectTo: 'root'
                        },
                        displayName: 'restaurants'
                    },
                    resolve: {
                        allRestaurantTypePrepService: allRestaurantTypePrepService,
                        waitersLimitPrepService: waitersLimitPrepService
                    }                 
                })
				.state('editRestaurant', {
					url: '/Restaurant/:restaurantId',
                    templateUrl: './app/admin/restaurants/templates/editRestaurant.html',
                    controller: 'editRestaurantController',
                    'controllerAs': 'editRestCtrl',
                    data: {
                        permissions: {
                            only: ['Admin'],
                           redirectTo: 'root'
                        },
                        displayName: 'restaurants'
                    },
                    resolve: {
						restaurantPrepService:restaurantPrepService,
                        allRestaurantTypePrepService: allRestaurantTypePrepService,
                        waitersLimitPrepService: waitersLimitPrepService
                    }                 
                })
        });

	restaurantTypesPrepService.$inject = ['RestaurantTypeResource']
    function restaurantTypesPrepService(RestaurantTypeResource) {
        return RestaurantTypeResource.getAllRestaurantType().$promise;
    }

    restaurantTypePrepService.$inject = ['RestaurantTypeResource','$stateParams']
    function restaurantTypePrepService(RestaurantTypeResource,$stateParams) {
        return RestaurantTypeResource.getRestaurantType({restaurantTypeId:$stateParams.restaurantTypeId}).$promise;
    }

	restaurantsPrepService.$inject = ['RestaurantResource']
    function restaurantsPrepService(RestaurantResource) {
        return RestaurantResource.getAllRestaurant().$promise;
    }
	
	allRestaurantTypePrepService.$inject = ['RestaurantTypeResource']
    function allRestaurantTypePrepService(RestaurantTypeResource) {
        return RestaurantTypeResource.getAllRestaurantType().$promise;
    }
	
	restaurantPrepService.$inject = ['RestaurantResource','$stateParams']
    function restaurantPrepService(RestaurantResource,$stateParams) {
        return RestaurantResource.getRestaurant({ restaurantId: $stateParams.restaurantId }).$promise;
    }
	
	englishRestaurantPrepService.$inject = ['RestaurantResource','$localStorage','appCONSTANTS']
    function englishRestaurantPrepService(RestaurantResource,$localStorage,appCONSTANTS) {
		if($localStorage.language != appCONSTANTS.defaultLanguage){
			return RestaurantResource.getAllRestaurant({pagesize:0, lang:'en'}).$promise;
		}
		else
			return null;
        
    }

    waitersLimitPrepService.$inject = ['AdminWaitersLimitResource']
    function waitersLimitPrepService(AdminWaitersLimitResource) {
        return AdminWaitersLimitResource.getWaitersLimitAndConsumed().$promise;
    }


}());
