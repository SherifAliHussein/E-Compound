(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('Tickets', {
                    url: '/Ticket/:featureId',
                    templateUrl: './app/ticket/templates/ticket.html',
                    controller: 'TicketController',
                    'controllerAs': 'TicketCtrl',
                    data: {
                        permissions: {
                            only: ['Room'],
                            redirectTo: 'root'
                        }
                    } 
                    ,
                    resolve: {
                        featureDetailPrepService: featureDetailPrepService ,
                       /* userCategoryPagingPrepService: userCategoryPagingPrepService*/
                    }
                }) 
        });
        userCategoryPagingPrepService.$inject = ['CategoryResource'] 
        function userCategoryPagingPrepService(CategoryResource) {
            return CategoryResource.UserCategories().$promise;
        }


        featureDetailPrepService.$inject = ['FeatureResource','$stateParams']
        function featureDetailPrepService(FeatureResource,$stateParams) {
            return FeatureResource.getFeature({featureId: $stateParams.featureId}).$promise;
        }

        TicketsPrepService.$inject = ['TicketResource']
        function TicketsPrepService(TicketResource) {
            return TicketResource.getAllActivatedTickets({pageSize:0}).$promise;
        }
        TicketDetailPrepService.$inject = ['TicketResource','$stateParams']
        function TicketDetailPrepService(TicketResource,$stateParams) {
            return TicketResource.getTicket({TicketId: $stateParams.TicketId}).$promise;
        }
        
        requestDetailPrepService.$inject = ['TicketResource','$stateParams']
        function requestDetailPrepService(TicketResource,$stateParams) {
            return TicketResource.getRequestByTicketId({TicketId: $stateParams.TicketId}).$promise;
        }

        TicketBackgroundPrepService.$inject = ['TicketBackgroundResource']
        function TicketBackgroundPrepService(TicketBackgroundResource) {
            return TicketBackgroundResource.getActivatedBackground().$promise;
        }

        lastRequestPrepService.$inject = ['TicketResource','$stateParams']
        function lastRequestPrepService(TicketResource, $stateParams) {
            return TicketResource.geLasttRequestByTicketId({TicketId: $stateParams.TicketId}).$promise;
        }
}());
