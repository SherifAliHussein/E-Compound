(function() {
    angular
      .module('home') 
      .factory('AddnotificationResource', ['$resource', 'appCONSTANTS', AddnotificationResource]) 
   ;
  
 
    function AddnotificationResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Notification/', {}, { 
            send: { method: 'POST',useToken: true}
        })
    } 

  }());
  
