(function() {
    angular
      .module('home')
      .factory('ManageFeatureResource', ['$resource', 'appCONSTANTS', ManageFeatureResource]);
  
    function ManageFeatureResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Features/', {}, {
        getAllFeatures: { method: 'GET', useToken: true,isArray:true },
        getFeatureDetails: {url: appCONSTANTS.API_URL + 'Features/Detail/:featureControlId', method: 'GET', useToken: true },
        // addFeatureDetailAvailability: {url: appCONSTANTS.API_URL + 'Features/Detail/Available', method: 'POST', useToken: true },
        // updateFeatureDetailAvailability: {url: appCONSTANTS.API_URL + 'Features/Detail/Available', method: 'PUT ', useToken: true },
        deleteFeatureDetailAvailability: {url: appCONSTANTS.API_URL + 'Features/Detail/Available/:availableId', method: 'DELETE', useToken: true },
        getFeatureDetails: {url: appCONSTANTS.API_URL + 'Features/Detail/:featureControlId', method: 'GET', useToken: true },
        switchFeatureControl: {url: appCONSTANTS.API_URL + 'Features/Control/:featureControlId/Switch', method: 'GET', useToken: true },
        deleteFeatureDetail: {url: appCONSTANTS.API_URL + 'Features/Detail/:featureDetailId', method: 'DELETE', useToken: true }
      })
    }

}());
  