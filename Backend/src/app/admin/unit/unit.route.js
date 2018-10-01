(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('units', {
                    url: '/units',
                    templateUrl: './app/admin/unit/templates/unit.html',
                    controller: 'unitController',
                    'controllerAs': 'unitCtrl',
                    data: {
                        permissions: {
                            only: ['GlobalAdmin'],
                            redirectTo: 'root'
                        }
                    },
                    resolve: {
                        unitPagingPrepService: unitPagingPrepService,
                        unitTypesPagingPrepService: unitTypesPagingPrepService
                    }
                })
        });

        unitPagingPrepService.$inject = ['UnitPagingResource']
        
        function unitPagingPrepService(UnitPagingResource) {
            return UnitPagingResource.getAllPagingUnits().$promise;
        }

        unitTypesPagingPrepService.$inject = ['UnitTypeResource']
        
        function unitTypesPagingPrepService(UnitTypeResource) {
            return UnitTypeResource.getUnitTypes().$promise;
        }

        unitPrepService.$inject = ['UnitResource']
        
        function unitPrepService(UnitResource) {
            return UnitResource.getAllUnits().$promise;
        }
}());
