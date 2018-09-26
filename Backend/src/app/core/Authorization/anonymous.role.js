(function() {
  'use strict';

  angular
    .module('core')
    .run(runBlock);

  runBlock.$inject = ['PermissionStore','authorizationService'];

  function runBlock (PermissionStore, authorizationService){
    PermissionStore
      .definePermission('anonymous',function(){
        return !authorizationService.isLoggedIn();
      });
  }

}());
