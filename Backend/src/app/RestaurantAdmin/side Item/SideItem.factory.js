(function() {
    angular
      .module('home')
      .factory('SideItemResource', ['$resource', 'appCONSTANTS', SideItemResource]);
  
    function SideItemResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'SideItems/:SideItemId', {}, {
        getAllSideItems: { method: 'GET', useToken: true, params:{lang:'@lang'} },
        create: { method: 'POST', useToken: true },
        deleteSideItem: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }
    
  }());
  