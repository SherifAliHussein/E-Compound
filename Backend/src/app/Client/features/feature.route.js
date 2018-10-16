(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('clientFeatures', {
                    url: '/Clientfeature',
                    templateUrl: './app/Client/features/templates/features.html',
                    controller: 'clientFeatureController',
                    'controllerAs': 'featureCtrl',
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
        
        featuresPrepService.$inject = ['FeatureResource']
        function featuresPrepService(FeatureResource) {
            return FeatureResource.getAllActivatedFeatures().$promise;
        }
        featureDetailPrepService.$inject = ['FeatureResource','$stateParams']
        function featureDetailPrepService(FeatureResource,$stateParams) {
            return FeatureResource.getFeature({featureId: $stateParams.featureId}).$promise;
        }
}());
