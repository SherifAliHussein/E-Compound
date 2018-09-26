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
                // .state('featureDetail', {
                //     url: '/feature/:featureId/Info',
                //     templateUrl: './app/Client/features/templates/featureDetail.html',
                //     controller: 'featureDetailController',
                //     'controllerAs': 'featureDetailCtrl',
                //     data: {
                //         permissions: {
                //             only: ['Room'],
                //             redirectTo: 'root'
                //         }
                //     },
                //     resolve: {
                //         featureDetailPrepService: featureDetailPrepService
                //     }
                    
                // })
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
