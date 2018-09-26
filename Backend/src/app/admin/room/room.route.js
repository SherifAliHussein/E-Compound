(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

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
                        roomLimitPrepService:roomLimitPrepService,
                        BuildingsPrepService:BuildingsPrepService,
                        FloorsPrepService:FloorsPrepService
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

        BuildingsPrepService.$inject = ['BuildingResource']
        function BuildingsPrepService(BuildingResource) {
            return BuildingResource.getAllBuildings().$promise;
        }

        FloorsPrepService.$inject = ['FloorResource']
        function FloorsPrepService(FloorResource) {
            return FloorResource.getAllFloors().$promise;
        }
}());
