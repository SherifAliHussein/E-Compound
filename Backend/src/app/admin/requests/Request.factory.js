(function() {
    angular
      .module('home')
      .factory('RequestResource', ['$resource', 'appCONSTANTS', RequestResource]);
  
    function RequestResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Requests/', {}, {
        getAllRequest: { method: 'GET', useToken: true },
        Approve: {url: appCONSTANTS.API_URL + 'Requests/:requestId/Approve', method: 'POST', useToken: true },
        Reject: {url: appCONSTANTS.API_URL + 'Requests/:requestId/Reject', method: 'GET', useToken: true },
        create: { method: 'POST', useToken: true },
        deleteReceptionist: { method: 'DELETE', useToken: true }
      })
    }

}());
  