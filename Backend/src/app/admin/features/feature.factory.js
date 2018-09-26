(function() {
    angular
      .module('home')
      .factory('FeatureResource', ['$resource', 'appCONSTANTS', FeatureResource])
      .factory('ControlResource', ['$resource', 'appCONSTANTS', ControlResource])
      .factory('ActivateFeatureResource', ['$resource', 'appCONSTANTS', ActivateFeatureResource])
      .factory('DeactivateFeatureResource', ['$resource', 'appCONSTANTS', DeactivateFeatureResource]);
  
    function FeatureResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Features/:featureId', {}, {
        getAllFeatures: { method: 'GET', useToken: true },
        getAllActivatedFeatures: {url: appCONSTANTS.API_URL + 'Features/Activated', method: 'GET', useToken: true },        
        getAllFeaturesName: {url:appCONSTANTS.API_URL + 'Features/Name', method: 'GET', useToken: true,isArray:true },
        checkFeatureAsRestaurant: {url: appCONSTANTS.API_URL + 'Features/Restaurant', method: 'GET', useToken: true },
        getFeature: { method: 'GET', useToken: true },
        create: { method: 'POST', useToken: true },
        deleteFeature: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }

    function ControlResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Features/Control', {}, {
        GetAllControls: { method: 'GET', useToken: true, isArray:true}
      })
  }

    function ActivateFeatureResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Features/:featureId/Activate', {}, {
          Activate: { method: 'GET', useToken: true}
        })
    }
    function DeactivateFeatureResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Features/:featureId/DeActivate', {}, {
          Deactivate: { method: 'GET', useToken: true }
        })
    }

}());
  