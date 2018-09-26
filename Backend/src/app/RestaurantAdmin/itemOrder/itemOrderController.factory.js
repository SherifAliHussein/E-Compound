(function() {
    angular
      .module('home')
      .factory('ItemOrderResource', ['$resource', 'appCONSTANTS', ItemOrderResource])
      .factory('UpdateItemOrderResource', ['$resource', 'appCONSTANTS', UpdateItemOrderResource])  

      function ItemOrderResource($resource, appCONSTANTS) {  
              return $resource(appCONSTANTS.API_URL + 'Categories/:categoryId/Items/Templates', {}, { 
                getAllItemOrder: { method: 'GET', useToken: true }
        })
    }

    function UpdateItemOrderResource($resource, appCONSTANTS) {  
        return $resource(appCONSTANTS.API_URL + 'Items/Order', {}, { 
          updateOrder: { method: 'PUT', useToken: true,isArray: true }
  })
}
   
}());
  