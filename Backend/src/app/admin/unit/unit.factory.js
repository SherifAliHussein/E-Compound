(function () {
  angular
    .module('home')
    .factory('UnitPagingResource', ['$resource', 'appCONSTANTS', UnitPagingResource])
    .factory('UnitResource', ['$resource', 'appCONSTANTS', UnitResource])
    .factory('UnitTypeResource', ['$resource', 'appCONSTANTS', UnitTypeResource])
    .factory('AddUnitResource', ['$resource', 'appCONSTANTS', AddUnitResource])
    .factory('DeleteUnitResource', ['$resource', 'appCONSTANTS', DeleteUnitResource])
    .factory('UpdateUnitResource', ['$resource', 'appCONSTANTS', UpdateUnitResource])
    ;


  function UnitTypeResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'UnitTypes', {}, {
      getUnitTypes: { method: 'GET', useToken: true, isArray: true }
    })
  }

  function UnitPagingResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'UnitsPaging', {}, {
      getAllPagingUnits: { method: 'GET', useToken: true }
    })
  }

  function UnitResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Units', {}, {
      getAllUnits: { method: 'GET', useToken: true, isArray: true }
    })
  }

  function DeleteUnitResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Units/:unitId', {}, {
      deleteUnit: { method: 'DELETE', useToken: true }
    })
  }

  function AddUnitResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Units/', {}, {
      addUnit: { method: 'POST', useToken: true }
    })
  }

  function UpdateUnitResource($resource, appCONSTANTS) {
    return $resource(appCONSTANTS.API_URL + 'Units', {}, {
      updateUnit: { method: 'PUT', useToken: true }
    })
  }

}());

