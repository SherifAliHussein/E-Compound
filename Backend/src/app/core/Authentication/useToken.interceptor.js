(function() {
  'use strict';
  (function() {
    angular
      .module('core')
      .factory('useTokenInterceptor', useTokenInterceptor);

    useTokenInterceptor.$inject = ['authenticationService','$localStorage'];


    function useTokenInterceptor(authenticationService,$localStorage) {
      var tokenInterceptor = {
        request: requestInterceptor
      };
      return tokenInterceptor;

      function requestInterceptor(config) {
          if (config.useToken) {
            return authenticationService.getToken()
              .then(function(data){
                config.headers['Authorization'] = data['token_type'] + " " + data['access_token'];
				if(config.params== null || config.params.lang ==null)
					config.headers['Accept-Language'] = $localStorage.language;//"en";
				else
					config.headers['Accept-Language'] = config.params.lang;
                if (!config.headers.hasOwnProperty('Content-Type')) 
                {
                    config.headers['Content-Type'] = 'application/json';
                }
                return config;
              });

          }
          return config;
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
      $httpProvider.interceptors.push('useTokenInterceptor');
    }
  })();

})();
