(function() {
    angular
      .module('home')
      .factory('FeaturesBackgroundResource', ['$resource', 'appCONSTANTS', FeaturesBackgroundResource])

    function FeaturesBackgroundResource($resource, appCONSTANTS) {  
        return $resource(appCONSTANTS.API_URL + 'FeatureBackgrounds/', {}, { 
            getAllBackgrounds: { method: 'GET', useToken: true },
            Activate: {url: appCONSTANTS.API_URL + 'FeatureBackgrounds/:backgroundId/Activate/', method: 'GET', useToken: true}
            
        })
    }
  

}());
  