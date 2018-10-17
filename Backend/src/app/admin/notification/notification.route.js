(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('notifications', {
                    url: '/notifications',
                    templateUrl: './app/admin/notification/templates/sendNotification.html',
                    controller: 'addnotificationController',
                    'controllerAs': 'notificationCtrl',
                    data: {
                        permissions: {
                            only: ['GlobalAdmin'],
                            redirectTo: 'root'
                        }
                    } 
                })
        }); 
}());
