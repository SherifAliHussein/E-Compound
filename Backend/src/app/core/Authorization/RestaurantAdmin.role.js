(function() {
    'use strict';
  
    angular
      .module('core')
      .run(runBlock);
  
    runBlock.$inject = ['PermissionStore', 'authorizationService', 'userRolesEnum'];
  
    function runBlock(PermissionStore, authorizationService, userRolesEnum) {
      PermissionStore
        .definePermission('RestaurantAdmin', function () {
            return authorizationService.hasRole(String(userRolesEnum.RestaurantAdmin));
        });
    }
  
  }());
  