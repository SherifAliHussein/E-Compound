(function () {
    'use strict';

    angular
        .module('home')
        .config(function ($stateProvider, $urlRouterProvider) {

            $stateProvider
                .state('rooms', {
                    url: '/rooms',
                    templateUrl: './app/admin/room/templates/rooms.html',
                    controller: 'roomsController',
                    'controllerAs': 'roomsCtrl',
                    data: {
                        permissions: {
                            only: ['admin'],
                            redirectTo: 'root'
                        }
                    },
                    resolve: {
                        RoomsPrepService: RoomsPrepService,
                        roomLimitPrepService: roomLimitPrepService,
                        UnitPrepService: UnitPrepService
                    }

                })
        });

    RoomsPrepService.$inject = ['RoomResource']
    function RoomsPrepService(RoomResource) {
        return RoomResource.getAllRooms().$promise;
    }

    roomLimitPrepService.$inject = ['AdminRoomsLimitResource']
    function roomLimitPrepService(AdminRoomsLimitResource) {
        return AdminRoomsLimitResource.getRoomsLimitAndConsumed().$promise;
    }

    UnitPrepService.$inject = ['UnitPagingResource']
  
    function UnitPrepService(UnitPagingResource) {
        return UnitPagingResource.getAllPagingUnits().$promise;
    }
}());
