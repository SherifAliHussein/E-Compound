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
                        featureAsRestaurantPrepService:featureAsRestaurantPrepService,
                        featureAsInvitationPrepService:featureAsInvitationPrepService,
                        featureAsTicketPrepService:featureAsTicketPrepService
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
                .state('newFeatureInvetition', {
                    url: '/newFeatureInvetition',
                    templateUrl: './app/admin/features/templates/newFeatureInvitation.html',
                    controller: 'newFeatureInvitationController',
                    'controllerAs': 'newFeatureCtrl',
                    data: {
                        permissions: {
                            only: ['admin'],
                           redirectTo: 'root'
                        }
                    } 
                 
                })
                
              .state('editFeatureInvetition', {
                  url: '/feature/:featureId/Invetition',
                  templateUrl: './app/admin/features/templates/editFeatureInvitation.html',
                  controller: 'editFeatureInvitationController',
                  'controllerAs': 'editFeatureDlCtrl',
                  data: {
                      permissions: {
                          only: ['Admin'],
                         redirectTo: 'root'
                      }
                  },
                  resolve: {
                    featurePrepService: featurePrepService, 
                    
                  }
              }) 
              .state('newFeatureTicket', {
                url: '/newFeatureTicket',
                templateUrl: './app/admin/features/templates/newFeatureTicket.html',
                controller: 'newFeatureTicketController',
                'controllerAs': 'newFeatureCtrl',
                data: {
                    permissions: {
                        only: ['admin'],
                       redirectTo: 'root'
                    }
                } 
             
            })
            
          .state('editFeatureTicket', {
              url: '/feature/:featureId/Ticket',
              templateUrl: './app/admin/features/templates/editFeatureTicket.html',
              controller: 'editFeatureTicketController',
              'controllerAs': 'editFeatureDlCtrl',
              data: {
                  permissions: {
                      only: ['Admin'],
                     redirectTo: 'root'
                  }
              },
              resolve: {
                featurePrepService: featurePrepService 
                
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
       
        featureAsInvitationPrepService.$inject = ['FeatureResource']
        function featureAsInvitationPrepService(FeatureResource) {
            return FeatureResource.checkFeatureAsInvitation().$promise;
        }

        featureAsTicketPrepService.$inject = ['FeatureResource']
        function featureAsTicketPrepService(FeatureResource) {
            return FeatureResource.CheckFeatureAsTicket().$promise;
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
