(function () {
    'use strict';

    angular
        .module('home')
        .controller('menuController', ['$rootScope', '$translate', '$scope', 'appCONSTANTS', '$stateParams', 'MenuResource', 'menuPrepService', 'ResturantPrepService', 'CategoriesResource', '$state', '_', 'authenticationService', 'authorizationService', '$localStorage', 'userRolesEnum', 'ToastService', 'ResturantResource', 'MenuOfflineResource', 'OfflineDataResource','CartIconService', menuController])

    function menuController($rootScope, $translate, $scope, appCONSTANTS, $stateParams, MenuResource, menuPrepService, ResturantPrepService, CategoriesResource, $state, _, authenticationService, authorizationService, $localStorage, userRolesEnum, ToastService, ResturantResource, MenuOfflineResource, OfflineDataResource,CartIconService) {
        var vm = this;
        // ResturantResource.getResturantGlobalInfo().$promise.then(function (results) {

        //     $scope.$parent.globalInfo = results

        // },
        //   function (data, status) {
        //       ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
        //   });
        $scope.cartIcon = true;        
        CartIconService.cartIcon = true;

        $scope.$parent.globalInfo= ResturantPrepService;
        vm.restaurantId = $stateParams.restaurantId;
        
        if (navigator.onLine)
            vm.menus = menuPrepService.results;
        else
            vm.menus = menuPrepService;
        vm.categories = ""; 
        
        function refreshMenu() {
            var k = MenuResource.getAllMenus({ page: vm.currentPage }).$promise.then(function (results) {
                vm.menus = results
            },
            function (data, status) {
                ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
            });
        }
        vm.ShowId = function (_menuId) {
            refreshCategories(_menuId);
        };

        function refreshCategories(mnuId) {
            if(navigator.onLine){
                var k = CategoriesResource.getAllCategories({ MenuId: mnuId, page: vm.currentPage }).$promise.then(function (results) {
                    console.log(results);
                    vm.categories = results.results
                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
            }
            else{
             vm.categories =  OfflineDataResource.getAllCategories(mnuId);  
            }
        }
        

        // if (navigator.onLine) {
        //     MenuOfflineResource.getAllMenus({lang:'en'}).$promise.then(function (results) {
        //         console.log(results)
        //         if ('serviceWorker' in navigator) {
        //               navigator.serviceWorker.ready.then(function (reg) {
        //                 results.forEach(function(menu) {
        //                     navigator.serviceWorker.controller.postMessage(menu.imageURL);
        //                     menu.categoryModels.forEach(function(category) {
        //                         navigator.serviceWorker.controller.postMessage(category.imageURL);
        //                         category.categoryPageTemplateModel.templates.forEach(function(template) {
        //                             template.itemModels.forEach(function(item) {
        //                                 navigator.serviceWorker.controller.postMessage(item.imageURL);
        //                                 navigator.serviceWorker.controller.postMessage(item.imageURL+"?type=orignal2");
        //                             }, this);
        //                         }, this);
        //                     }, this);
        //                   }, this);
                        
                    
        //               })
        //             }
        //         OfflineDataResource.setAllData('en',results);
                
        //     },
        //     function (data, status) {
        //         ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
        //     });
        //     MenuOfflineResource.getAllMenus({lang:'ar'}).$promise.then(function (results) {
              
        //         OfflineDataResource.setAllData('ar',results);
                
        //     },
        //     function (data, status) {
        //         ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
        //     });

        // }
    }


}());
