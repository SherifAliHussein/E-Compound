(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('Features', {
                    url: '/feature',
                    templateUrl: './app/features/templates/features.html',
                    controller: 'clientFeatureController',
                    'controllerAs': 'featureCtrl',
                    data: {
                        permissions: {
                            only: ['Room'],
                            redirectTo: 'root'
                        }
                    },
                    resolve: {
                        featuresPrepService: featuresPrepService,
                        featureBackgroundPrepService: featureBackgroundPrepService                        
                    }
                    
                })
                .state('featureDetail', {
                    url: '/feature/:featureId/Info',
                    templateUrl: './app/features/templates/featureDetail.html',
                    controller: 'featureDetailController',
                    'controllerAs': 'featureDetailCtrl',
                    data: {
                        permissions: {
                            only: ['Room'],
                            redirectTo: 'root'
                        }
                    },
                    resolve: {
                        featureDetailPrepService: featureDetailPrepService,
                        featureBackgroundPrepService:featureBackgroundPrepService,
                        requestDetailPrepService:requestDetailPrepService,
                        lastRequestPrepService:lastRequestPrepService
                    }
                    
                })
        });
        
        featuresPrepService.$inject = ['FeatureResource']
        function featuresPrepService(FeatureResource) {
            return FeatureResource.getAllActivatedFeatures({pageSize:0}).$promise;
        }
        featureDetailPrepService.$inject = ['FeatureResource','$stateParams']
        function featureDetailPrepService(FeatureResource,$stateParams) {
            return FeatureResource.getFeature({featureId: $stateParams.featureId}).$promise;
        }
        
        requestDetailPrepService.$inject = ['FeatureResource','$stateParams']
        function requestDetailPrepService(FeatureResource,$stateParams) {
            return FeatureResource.getRequestByFeatureId({featureId: $stateParams.featureId}).$promise;
        }

        featureBackgroundPrepService.$inject = ['FeatureBackgroundResource']
        function featureBackgroundPrepService(FeatureBackgroundResource) {
            return FeatureBackgroundResource.getActivatedBackground().$promise;
        }

        lastRequestPrepService.$inject = ['FeatureResource','$stateParams']
        function lastRequestPrepService(FeatureResource, $stateParams) {
            return FeatureResource.geLasttRequestByFeatureId({featureId: $stateParams.featureId}).$promise;
        }
}());
