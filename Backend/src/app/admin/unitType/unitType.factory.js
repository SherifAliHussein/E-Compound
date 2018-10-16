(function() {
    angular
      .module('home')
      .factory('UnitTypePagingResource', ['$resource', 'appCONSTANTS', UnitTypePagingResource])
      .factory('AddUnitTypeResource', ['$resource', 'appCONSTANTS', AddUnitTypeResource])
      .factory('DeleteUnitTypeResource', ['$resource', 'appCONSTANTS', DeleteUnitTypeResource])
      .factory('UpdateUnitTypeResource', ['$resource', 'appCONSTANTS', UpdateUnitTypeResource])
   ;
  

    function UnitTypePagingResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'UnitTypesPaging', {}, {
        getAllPagingUnitTypes: { method: 'GET', useToken: true} 
      })
    }

   
      
    function DeleteUnitTypeResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'UnitTypes/:unitTypeId', {}, {
          deleteUnitType: { method: 'DELETE', useToken: true} 
        })
    }

    function AddUnitTypeResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'UnitTypes/', {}, { 
            addUnitType: { method: 'POST',useToken: true}
        })
    }

    function UpdateUnitTypeResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'UnitTypes', {}, {
          updateUnitType: { method: 'PUT', useToken: true} 
        })
    }

  }());
  
