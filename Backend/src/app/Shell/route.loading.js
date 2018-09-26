(function() {
    'use strict';
  
    angular
      .module('home')
      .config(config)
      .run(runBlock);
  
    config.$inject = ['ngProgressLiteProvider'];
    runBlock.$inject = ['$transitions', 'ngProgressLite' ];
  
    function config(ngProgressLiteProvider) {
      ngProgressLiteProvider.settings.speed = 1000;
  
    }
  
    function runBlock($transitions, ngProgressLite ) {
  
      // $transitions.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
      //     startProgress();
      // });
      $transitions.onStart({}, function(transition) {
        startProgress();
      });
      $transitions.onSuccess({}, function(transition) {
        endProgress()
      });
      $transitions.onError({  }, function(transition) {
        endProgress()
      });
      var routingDoneEvents = ['$stateChangeSuccess', '$stateChangeError', '$stateNotFound'];
  
      // angular.forEach(routingDoneEvents, function(event) {
      //   $transitions.$on(event, function(event, toState, toParams, fromState, fromParams) {
      //     endProgress();
      //   });
      // });
  
      function startProgress() {
        ngProgressLite.start();
      }
  
      function endProgress() {
        ngProgressLite.done();
      }
  
    }
  })();
  