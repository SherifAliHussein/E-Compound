(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('unitTypes', {
                    url: '/unitTypes',
                    templateUrl: './app/admin/unitType/templates/unitType.html',
                    controller: 'unitTypeController',
                    'controllerAs': 'unitTypeCtrl',
                    data: {
                        permissions: {
                            only: ['GlobalAdmin'],
                            redirectTo: 'root'
                        }
                    },
                    resolve: {
                        unitTypePagingPrepService: unitTypePagingPrepService
                    }
                })
        });

        unitTypePagingPrepService.$inject = ['UnitTypePagingResource']
        
        function unitTypePagingPrepService(UnitTypePagingResource) {
            return UnitTypePagingResource.getAllPagingUnitTypes().$promise;
        }

        unitTypePrepService.$inject = ['UnitTypeResource']
        
        function unitTypePrepService(UnitTypeResource) {
            return UnitTypeResource.getAllUnitTypes().$promise;
        }
}());
