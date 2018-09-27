(function() {
    angular
      .module('home')
      .factory('CartResource', ['$resource', 'appCONSTANTS', CartResource])   

    function CartResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Items/:itemId', {}, {
        getItemById: { method: 'GET', useToken: true, params:{lang:'@lang'} } 
      })
    }
     

}());
  