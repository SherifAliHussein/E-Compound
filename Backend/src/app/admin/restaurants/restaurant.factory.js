(function() {
  angular
    .module('home')
    .factory('RestaurantResource', ['$resource', 'appCONSTANTS', RestaurantResource])
    .factory('RestaurantInfoResource', ['$resource', 'appCONSTANTS', RestaurantInfoResource])
    .factory('ActivateRestaurantResource', ['$resource', 'appCONSTANTS', ActivateRestaurantResource])
    .factory('DeactivateRestaurantResource', ['$resource', 'appCONSTANTS', DeactivateRestaurantResource])
    .factory('AdminWaitersLimitResource', ['$resource', 'appCONSTANTS', AdminWaitersLimitResource]);

  function RestaurantResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Restaurants/:restaurantId', {}, {
      getAllRestaurant: { method: 'GET', useToken: true,params:{lang:'@lang'} },
      getAllRestaurantsName: {url: appCONSTANTS.API_URL + 'Restaurants/Name', method: 'GET', useToken: true,isArray:true },      
      getRestaurant: { method: 'GET', useToken: true },
	    create: { method: 'POST', useToken: true },
	    deleteRestaurant: { method: 'DELETE', useToken: true },
	    update: { method: 'PUT', useToken: true }
    })
  }

  function ActivateRestaurantResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Restaurants/:restaurantId/Activate', {}, {
	    Activate: { method: 'GET', useToken: true }
    })
  }
  function DeactivateRestaurantResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Restaurants/:restaurantId/DeActivate', {}, {
	    DeActivate: { method: 'GET', useToken: true }
    })
  }
  
  function AdminWaitersLimitResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Users/GetMaxAndConUsers', {}, {
	    getWaitersLimitAndConsumed: { method: 'GET', useToken: true }
    })
  }
  function RestaurantInfoResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Restaurants/GetGlobalRestaurantInfo', {}, {
	    getRestaurantInfo: { method: 'GET', useToken: true }
    })
  }
}());
