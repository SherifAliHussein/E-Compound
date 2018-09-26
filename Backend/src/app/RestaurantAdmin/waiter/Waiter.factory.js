(function() {
    angular
      .module('home')
      .factory('WaiterResource', ['$resource', 'appCONSTANTS', WaiterResource])
      .factory('WaitersLimitResource', ['$resource', 'appCONSTANTS', WaitersLimitResource]);
  
    function WaiterResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Waiters/:waiterId', {}, {
        getAllWaiters: { method: 'GET', useToken: true, params:{lang:'@lang'} },
        create: { method: 'POST', useToken: true },
        deleteWaiter: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }

    function WaitersLimitResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Waiters/Limit', {}, {
        getWaitersLimit: { method: 'GET', useToken: true ,transformResponse: function (data) {return {waiterLimit: angular.fromJson(data)} }},
      })
    }
    
  }());
  