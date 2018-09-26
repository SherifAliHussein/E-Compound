(function() {
    angular
      .module('home')
      .factory('FloorResource', ['$resource', 'appCONSTANTS', FloorResource]);
  
    function FloorResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Floors/:floorId', {}, {
        getAllFloors: { method: 'GET', useToken: true },
        create: { method: 'POST', useToken: true },
        deleteFloor: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }


}());
  