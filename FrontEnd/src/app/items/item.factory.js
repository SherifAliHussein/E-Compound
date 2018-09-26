(function() {
    angular
      .module('home')
      .factory('ItemsResource', ['$resource', 'appCONSTANTS', ItemsResource]);
  
    function ItemsResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Categories/:CategoryId/Items/Templates', {}, {
          getAllItems: { method: 'GET', useToken: true, params:{lang:'@lang'}},
          likeItem: {url: appCONSTANTS.API_URL + 'Items/:itemId/Like', method: 'GET', useToken: true},
          dislikeItem: {url: appCONSTANTS.API_URL + 'Items/:itemId/Dislike', method: 'GET', useToken: true},
        })
    }

}());
  