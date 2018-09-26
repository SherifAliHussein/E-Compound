(function() {
  'use strict';

  (function() {
    angular
      .module('core')
      .factory('vlGlobalInterceptor', vlGlobalInterceptor);

    vlGlobalInterceptor.$inject = ['$q', 'messageTypeEnum', 'vlSignalingService'];

    function vlGlobalInterceptor($q, messageTypeEnum, vlSignalingService) {
      var factory = {
        response: responseInterceptor,
        responseError: responseErrorInterceptor
      };
      return factory;

      function responseInterceptor(response) {
		  
        if (response.config.shouldNotify) {
          if (response.data.messageType == messageTypeEnum.success) {
            vlSignalingService.success(getMessageFromResponse(response))
          } else if (response.data.messageType == messageTypeEnum.warning) {
            vlSignalingService.warning(getMessageFromResponse(response))
          }
        }
        return response;
      }

      function responseErrorInterceptor(response) {
        debugger;
        vlSignalingService.error(getMessageFromResponse(response))
        return $q.reject(response);
      }

      function getMessageFromResponse(response) {
        return response.data.message;
      }
    }
  })();



  //inject interceptor to $http
  (function() {
    angular
      .module("core")
      .config(config);

    config.$inject = ['$httpProvider'];

    function config($httpProvider) {
      $httpProvider.interceptors.push('vlGlobalInterceptor');
    }
  })();

})();
