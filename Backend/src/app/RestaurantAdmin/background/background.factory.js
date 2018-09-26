(function() {
    angular
      .module('home')
      .factory('BackgroundResource', ['$resource', 'appCONSTANTS', BackgroundResource]) 
      .factory('ActivatebackgroundResource', ['$resource', 'appCONSTANTS', ActivatebackgroundResource])
      .factory('DeactivateBackgroundResource', ['$resource', 'appCONSTANTS', DeactivateBackgroundResource]) 

      function BackgroundResource($resource, appCONSTANTS) {  
              return $resource(appCONSTANTS.API_URL + 'Backgrounds/GetAllBackground', {}, { 
                getAllBackgrounds: { method: 'GET', useToken: true, params:{lang:'@lang'} }
        })
    }
  

    function ActivatebackgroundResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Backgrounds/:BackgroundId/Activate', {}, {
          Activate: { method: 'GET', useToken: true}
        })
    }
    function DeactivateBackgroundResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Backgrounds/:BackgroundId/DeActivate', {}, {
          Deactivate: { method: 'GET', useToken: true }
        })
    }
}());
  