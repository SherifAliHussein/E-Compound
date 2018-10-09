(function() {
    angular
      .module('home')
      .factory('TechnicanResource', ['$resource', 'appCONSTANTS', TechnicanResource])
      .factory('AssignResource', ['$resource', 'appCONSTANTS', AssignResource]);

    function TechnicanResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Users/TechnicianByCategoryId/:categoryId', {}, { 
        GetTechnicans: { method: 'GET', useToken: true } 
      })
    }

    function AssignResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Requests/', {}, { 
        create: { method: 'POST', useToken: true }
      })
  }


}());
  