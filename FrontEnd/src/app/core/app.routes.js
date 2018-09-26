(function() {
    'use strict';

    angular
        .module('core')
        .config(function($stateProvider, $urlRouterProvider,) {

            $urlRouterProvider.otherwise('/');

            // main views
            $stateProvider
              .state('root', {
                    url: '/',
               
                    controller: 'loginController',
                   
                    data: {
                       permissions: {
                          
                        }
                    },
                 
                })
                .state('login', {
                    url: '/login',
                    templateUrl: './app/core/login/templates/login.html',
                    'controller': 'loginController'
                })
                .state('menu', {
                    url: '/feature/:featureId/restaurant/:restaurantId/menu',
                    templateUrl: './app/menu/menu.html',
                    controller: 'menuController',
                    'controllerAs': 'menuCtrl',
                    resolve: {
                        menuPrepService: menuPrepService,
                        ResturantPrepService: ResturantPrepService
                    }
                })


                //$locationProvider.html5Mode(true);
                .state('Items', {
                    url: '/feature/:featureId/restaurant/:restaurantId/menu/:menuId/Category/:categoryId/Item',
                    templateUrl: './app/items/Templates/Item.html',
                    controller: 'ItemController',
                    'controllerAs': 'itemCtrl',
                    data: {
                        permissions: {
                            // only: ['Waiter'],
                            redirectTo: 'root'
                        },
                        displayName: 'items'
                    },
                    resolve: {
                        categoryItemsTemplatePrepService: categoryItemsTemplatePrepService,
                        ResturantPrepService: ResturantPrepService
                    }
                })
                .state('cart', {
                    url: '/feature/:featureId/restaurant/:restaurantId/cart',
                    templateUrl: './app/cart/cart.html',
                    'controller': 'cartController',
                    'controllerAs': 'cartCtrl',
                    resolve: {
                        ResturantPrepService: ResturantPrepService
                    }
                })
        });

        
        menuPrepService.$inject = ['MenuResource','OfflineDataResource','$stateParams']
            function menuPrepService(MenuResource,OfflineDataResource, $stateParams) {
                if(navigator.onLine){
                    return MenuResource.getAllMenus({ restaurantId: $stateParams.restaurantId }).$promise;
                }
                else{
                    return OfflineDataResource.getMenus();
                }
                // return MenuResource.getAllMenus().$promise;
                
            }
            
        ResturantPrepService.$inject = ['ResturantResource','$stateParams']
        function ResturantPrepService(ResturantResource, $stateParams) {
            return ResturantResource.getResturantGlobalInfo({ restaurantId: $stateParams.restaurantId }).$promise;
        }


        categoryItemsTemplatePrepService.$inject = ['ItemsResource','$stateParams','OfflineDataResource']
        function categoryItemsTemplatePrepService(ItemsResource,$stateParams,OfflineDataResource) {
            if(navigator.onLine){
            return ItemsResource.getAllItems({ CategoryId: $stateParams.categoryId }).$promise;
        }
        else{
            return OfflineDataResource.getAllItems($stateParams.menuId,$stateParams.categoryId);
        }
        //return ItemsResource.getAllItems({ CategoryId: $stateParams.categoryId }).$promise;
        }
    
}());
