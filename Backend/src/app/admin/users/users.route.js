(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
              .state('users', {
					url: '/users',
                    templateUrl: './app/admin/users/templates/users.html',
                    controller: 'usersController',
                    'controllerAs': 'userCtrl',
                    data: {
                        permissions: {
                            only: ['admin'],
                           redirectTo: 'root'
                        }
                    },
                    resolve: {
                        ReceptionistsPrepService: ReceptionistsPrepService,
                        SupervisorsPrepService: SupervisorsPrepService
                    }
                 
                })
        });
        
        ReceptionistsPrepService.$inject = ['ReceptionistResource']
        function ReceptionistsPrepService(ReceptionistResource) {
            return ReceptionistResource.getAllReceptionists().$promise;
        }
        SupervisorsPrepService.$inject = ['SupervisorResource']
        function SupervisorsPrepService(SupervisorResource) {
            return SupervisorResource.getAllSupervisors().$promise;
        }
}());
