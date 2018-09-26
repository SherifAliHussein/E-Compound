(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
            .state('featuresBackgrounds', {
                url: '/features/Background',
                templateUrl: './app/admin/featuresbackground/templates/background.html',
                controller: 'featuresBackgroundController',
                'controllerAs': 'backgroundCtrl',
                data: {
                    permissions: {
                        only: ['RestaurantAdmin'],
                        redirectTo: 'root'
                    },
                    displayName: 'Backgrounds'
                },
                resolve: {
                    backgroundsPrepService: backgroundsPrepService
                }
            })
        });
        
        backgroundsPrepService.$inject = ['FeaturesBackgroundResource']
        function backgroundsPrepService(FeaturesBackgroundResource) {
            return FeaturesBackgroundResource.getAllBackgrounds().$promise; 
        }
}());
