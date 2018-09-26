(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('manageFeatures', {
                    url: '/ManageFeature',
                    templateUrl: './app/supervisor/features/templates/features.html',
                    controller: 'manageFeaturesController',
                    'controllerAs': 'manageFeaturesCtrl',
                    data: {
                        permissions: {
                            only: ['Room'],
                            redirectTo: 'root'
                        }
                    },
                    resolve: {
                        featuresPrepService: featuresPrepService
                    }
                    
                })
        });
        
        featuresPrepService.$inject = ['ManageFeatureResource']
        function featuresPrepService(ManageFeatureResource) {
            return ManageFeatureResource.getAllFeatures().$promise;
        }
}());
