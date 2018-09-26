(function() {
    angular
      .module('home')
      .factory('BuildingResource', ['$resource', 'appCONSTANTS', BuildingResource]);
  
    function BuildingResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Buildings/:buildingId', {}, {
        getAllBuildings: { method: 'GET', useToken: true },
        create: { method: 'POST', useToken: true },
        deleteBuilding: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }


}());
  