(function() {
    angular
      .module('home')
      .factory('HomeResource', ['$resource', 'appCONSTANTS', HomeResource])   
      .factory('ResturantResource', ['$resource', 'appCONSTANTS', ResturantResource])

    function HomeResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Menus/:MenuId', {}, {
        getAllMenus: { method: 'GET', useToken: true, params:{lang:'@lang'} } 
      })
    }
     
    function ResturantResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Restaurants/GetGlobalRestaurantInfo', {}, {
        getResturantGlobalInfo: { method: 'GET', useToken: true, params:{lang:'@lang'} }
      })
  }


}());
  