(function() {
    angular
      .module('home')
      .factory('BranchResource', ['$resource', 'appCONSTANTS', BranchResource])
      .factory('ActivateBranchResource', ['$resource', 'appCONSTANTS', ActivateBranchResource])
      .factory('DeactivateBranchResource', ['$resource', 'appCONSTANTS', DeactivateBranchResource]);
  
    function BranchResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Branches/:branchId', {}, {
        getAllBranches: { method: 'GET', useToken: true, params:{lang:'@lang'} },
        getBranch: { method: 'GET', useToken: true },
        create: { method: 'POST', useToken: true },
        deleteBranch: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }
    function ActivateBranchResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Branches/:branchId/Activate', {}, {
          Activate: { method: 'GET', useToken: true}
        })
    }
    function DeactivateBranchResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Branches/:branchId/DeActivate', {}, {
          Deactivate: { method: 'GET', useToken: true }
        })
    }
}());
  