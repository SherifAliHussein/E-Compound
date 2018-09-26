(function() {
    angular
      .module('home')
      .factory('MenuResource', ['$resource', 'appCONSTANTS', MenuResource])  
      .factory('MenuOfflineResource', ['$resource', 'appCONSTANTS', MenuOfflineResource])
      .factory('CategoriesResource', ['$resource', 'appCONSTANTS', CategoriesResource])
      .factory('ResturantResource', ['$resource', 'appCONSTANTS', ResturantResource])
      .factory('FeedBackResource', ['$resource', 'appCONSTANTS', FeedBackResource])

    function MenuResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Restaurants/:restaurantId/Menus/:MenuId', {}, {
        getAllMenus: { method: 'GET', useToken: true, params:{lang:'@lang'} } 
      })
    }
    
    function CategoriesResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Menus/:MenuId/Categories', {}, {
          getAllCategories: { method: 'GET', useToken: true, params:{lang:'@lang'} }
        })
    }

    function ResturantResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Restaurants/:restaurantId/GetGlobalRestaurantInfo', {}, {
          getResturantGlobalInfo: { method: 'GET', useToken: true, params:{lang:'@lang'} }
        })
    }

    function MenuOfflineResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Menus/OfflineData', {}, {
        getAllMenus: { method: 'GET', useToken: true, params:{lang:'@lang'},isArray:true } 
      })
    }
    function FeedBackResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'FeedBacks/', {}, {
        getAllFeedBack: {url: appCONSTANTS.API_URL + 'Restaurants/:restaurantId/FeedBacks/', method: 'GET', useToken: true },
        createFeedBack: { method: 'POST', useToken: true }
      })
    }

}());
  