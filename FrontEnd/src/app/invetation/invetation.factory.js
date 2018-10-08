(function() {
    angular
      .module('home')
      .factory('invetationResource', ['$resource', 'appCONSTANTS', invetationResource])   

    function invetationResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Invetation/CheckUnitInvetationLimit', {}, {
        CheckUnitInvetationLimit: { method: 'GET', useToken: true } 
      })
    }
     

}());
  