(function() {
    angular
      .module('home')
      .factory('TicketResource', ['$resource', 'appCONSTANTS', TicketResource])   
      .factory('CategoryResource', ['$resource', 'appCONSTANTS', CategoryResource])    
      .factory('CreateTicketResource', ['$resource', 'appCONSTANTS', CreateTicketResource])   ;
    function TicketResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Ticket/CheckUnitTicketLimit', {}, {
        CheckUnitTicketLimit: { method: 'GET', useToken: true } 
      })
    }
     
    function CategoryResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'UserCategories', {}, {
        UserCategories: { method: 'GET', isArray: true , useToken: true} 
      })
    }

    function CreateTicketResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'TicketRequests/', {}, { 
        create: { method: 'POST', useToken: true },
      })
    }
}());
  