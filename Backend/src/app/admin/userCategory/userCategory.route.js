(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('userCategories', {
                    url: '/userCategories',
                    templateUrl: './app/admin/userCategory/templates/userCategory.html',
                    controller: 'userCategoryController',
                    'controllerAs': 'userCategoryCtrl',
                    data: {
                        permissions: {
                            only: ['GlobalAdmin'],
                            redirectTo: 'root'
                        }
                    },
                    resolve: {
                        userCategoryPagingPrepService: userCategoryPagingPrepService
                    }
                })
        });

        userCategoryPagingPrepService.$inject = ['UserCategoryPagingResource']
        
        function userCategoryPagingPrepService(UserCategoryPagingResource) {
            return UserCategoryPagingResource.getAllPagingUserCategories().$promise;
        }

        userCategoryPrepService.$inject = ['UserCategoryResource']
        
        function userCategoryPrepService(UserCategoryResource) {
            return UserCategoryResource.getAllUserCategories().$promise;
        }
}());
