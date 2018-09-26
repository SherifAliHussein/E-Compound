(function() {
    angular
      .module('home')
      .factory('FeedBackResource', ['$resource', 'appCONSTANTS', FeedBackResource])

    function FeedBackResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'FeedBacks/', {}, {
        getAllFeedBack: { method: 'GET', useToken: true }
      })
  }


}());
  