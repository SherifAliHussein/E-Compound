(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
              .state('features', {
					url: '/feature',
                    templateUrl: './app/admin/features/templates/features.html',
                    controller: 'featureController',
                    'controllerAs': 'featureCtrl',
                    data: {
                        permissions: {
                            only: ['admin'],
                           redirectTo: 'root'
                        }
                    },
                    resolve: {
                        featuresPrepService: featuresPrepService,
                        featureAsRestaurantPrepService:featureAsRestaurantPrepService
                    }
                 
                })                
                .state('newFeature', {
                    url: '/newFeature',
                    templateUrl: './app/admin/features/templates/newFeature.html',
                    controller: 'newFeatureController',
                    'controllerAs': 'newFeatureCtrl',
                    data: {
                        permissions: {
                            only: ['admin'],
                           redirectTo: 'root'
                        }
                    },
                    // resolve: {
                    //   controlsPrepService: controlsPrepService
                    // }
                 
                })
                .state('editFeature', {
                      url: '/feature/:featureId',
                      templateUrl: './app/admin/features/templates/editFeature.html',
                      controller: 'editFeatureController',
                      'controllerAs': 'editFeatureDlCtrl',
                      data: {
                          permissions: {
                              only: ['Admin'],
                             redirectTo: 'root'
                          }
                      },
                      resolve: {
                        featurePrepService: featurePrepService,
                      //  controlsPrepService: controlsPrepService                        
                      }
                  })               
                  .state('newFeatureRestaurant', {
                      url: '/newFeatureRestaurant',
                      templateUrl: './app/admin/features/templates/newFeatureRestaurant.html',
                      controller: 'newFeatureRestaurantController',
                      'controllerAs': 'newFeatureCtrl',
                      data: {
                          permissions: {
                              only: ['admin'],
                             redirectTo: 'root'
                          }
                      },
                      resolve: {
                        restaurantsNamePrepService: restaurantsNamePrepService
                      }
                   
                  })
                  
                .state('editFeatureRestaurant', {
                    url: '/feature/:featureId/Restaurant',
                    templateUrl: './app/admin/features/templates/editFeatureRestaurant.html',
                    controller: 'editFeatureRestaurantController',
                    'controllerAs': 'editFeatureDlCtrl',
                    data: {
                        permissions: {
                            only: ['Admin'],
                           redirectTo: 'root'
                        }
                    },
                    resolve: {
                      featurePrepService: featurePrepService,
                      restaurantsNamePrepService: restaurantsNamePrepService
                      
                    }
                }) 
        });
        
        featuresPrepService.$inject = ['FeatureResource']
        function featuresPrepService(FeatureResource) {
            return FeatureResource.getAllFeatures().$promise;
        }

        featurePrepService.$inject = ['FeatureResource','$stateParams']
        function featurePrepService(FeatureResource,$stateParams) {
            return FeatureResource.getFeature({featureId: $stateParams.featureId}).$promise;
        }

        featureAsRestaurantPrepService.$inject = ['FeatureResource']
        function featureAsRestaurantPrepService(FeatureResource) {
            return FeatureResource.checkFeatureAsRestaurant().$promise;
        }

        restaurantsNamePrepService.$inject = ['RestaurantResource']
        function restaurantsNamePrepService(RestaurantResource) {
            return RestaurantResource.getAllRestaurantsName().$promise;
        }

        controlsPrepService.$inject = ['ControlResource']
        function controlsPrepService(ControlResource) {
            return ControlResource.GetAllControls().$promise;
        }
}());
