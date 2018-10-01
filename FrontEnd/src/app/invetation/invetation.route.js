(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('Invetations', {
                    url: '/Invetation',
                    templateUrl: './app/invetation/templates/invetation.html',
                    controller: 'invetationController',
                    'controllerAs': 'InvetationCtrl',
                    data: {
                        permissions: {
                            only: ['Room'],
                            redirectTo: 'root'
                        }
                    } 
                    
                }) 
        });
        
        InvetationsPrepService.$inject = ['InvetationResource']
        function InvetationsPrepService(InvetationResource) {
            return InvetationResource.getAllActivatedInvetations({pageSize:0}).$promise;
        }
        InvetationDetailPrepService.$inject = ['InvetationResource','$stateParams']
        function InvetationDetailPrepService(InvetationResource,$stateParams) {
            return InvetationResource.getInvetation({InvetationId: $stateParams.InvetationId}).$promise;
        }
        
        requestDetailPrepService.$inject = ['InvetationResource','$stateParams']
        function requestDetailPrepService(InvetationResource,$stateParams) {
            return InvetationResource.getRequestByInvetationId({InvetationId: $stateParams.InvetationId}).$promise;
        }

        InvetationBackgroundPrepService.$inject = ['InvetationBackgroundResource']
        function InvetationBackgroundPrepService(InvetationBackgroundResource) {
            return InvetationBackgroundResource.getActivatedBackground().$promise;
        }

        lastRequestPrepService.$inject = ['InvetationResource','$stateParams']
        function lastRequestPrepService(InvetationResource, $stateParams) {
            return InvetationResource.geLasttRequestByInvetationId({InvetationId: $stateParams.InvetationId}).$promise;
        }
}());
