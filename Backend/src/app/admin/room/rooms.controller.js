(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('roomsController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal', 'RoomResource'
		,'ActivateRoomResource','DeactivateRoomResource','RoomsPrepService','roomLimitPrepService','ToastService','AdminRoomsLimitResource'
		,'BuildingsPrepService','FloorsPrepService','BuildingResource','FloorResource',  roomsController])

    function roomsController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal, RoomResource,
		ActivateRoomResource,DeactivateRoomResource,RoomsPrepService,roomLimitPrepService,ToastService,AdminRoomsLimitResource,
		BuildingsPrepService,FloorsPrepService,BuildingResource,FloorResource){

        var vm = this;
        vm.rooms = RoomsPrepService;
		vm.roomsLimit = roomLimitPrepService;
		vm.buildings = BuildingsPrepService;
		vm.floors = FloorsPrepService;
		
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[2].children[0]).addClass("active")
		
		function refreshRooms(){
			var k = RoomResource.getAllRooms({ page:vm.currentPage }).$promise.then(function(results) {
				
				vm.rooms = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
			});
			AdminRoomsLimitResource.getRoomsLimitAndConsumed().$promise.then(function(results) {
				vm.roomsLimit = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
			});
		}
		vm.currentPage  = 1;
        vm.changePageRooms  = function (page) {
            vm.currentPage = page;
            refreshRooms();
		}
		vm.openRoomDialog = function(){
			var buildings;
			var floors;
			BuildingResource.getAllBuildings({ pageSize:0 }).$promise.then(function(results) {
				buildings = results;
				FloorResource.getAllFloors({ pageSize:0 }).$promise.then(function(results) {
					floors = results;
					var modalContent = $uibModal.open({
						templateUrl: './app/admin/room/templates/newRoom.html',
						controller: 'roomDialogController',
						controllerAs: 'roomDlCtrl',
						resolve:{
							callBackFunction:function(){return refreshRooms;},
							Buildings:function(){return buildings;},
							Floors:function(){return floors;}
						}
						
					});
				},
				function(data, status) {
					// ToastService.show("right","bottom","fadeInUp",data.message,"error");
				});
			},
            function(data, status) {
				// ToastService.show("right","bottom","fadeInUp",data.message,"error");
			});
			
				
		}
		function confirmationDelete(itemId){
			RoomResource.deleteRoom({roomId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('RoomDeleteSuccess'),"success");
				if(vm.rooms.results.length ==1 && vm.currentPage > 1)
					vm.currentPage = vm.currentPage -1;
				refreshRooms();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteRoomDialog = function(name,id){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function() { return id },
					message:function() { return null},
					callBackFunction:function() { return confirmationDelete }
				}
				
			});
		}
		
		vm.openEditRoomDialog = function(index){
			var buildings;
			var floors;
			BuildingResource.getAllBuildings({ pageSize:0 }).$promise.then(function(results) {
				buildings = results;
				FloorResource.getAllFloors({ pageSize:0 }).$promise.then(function(results) {
					floors = results;
					var modalContent = $uibModal.open({
						templateUrl: './app/admin/room/templates/editRoom.html',
						controller: 'editRoomDialogController',
						controllerAs: 'editRoomDlCtrl',
						resolve:{
							Room:function(){ return angular.copy(vm.rooms.results[index])},
							callBackFunction:function(){return refreshRooms;},
							Buildings:function(){return buildings;},
							Floors:function(){return floors;}
						}
						
					});
				},
				function(data, status) {
					// ToastService.show("right","bottom","fadeInUp",data.message,"error");
				});
			},
			function(data, status) {
				// ToastService.show("right","bottom","fadeInUp",data.message,"error");
			});
		}
		vm.ActivateRoom = function(room){
			ActivateRoomResource.Activate({roomId:room.roomId})
			.$promise.then(function(result){
				room.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.DeactivateRoom = function(room){
			DeactivateRoomResource.Deactivate({roomId:room.roomId})
			.$promise.then(function(result){
				room.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}		



		function refreshBuildings(){
			var k = BuildingResource.getAllBuildings({ page:vm.currentPageBuilding }).$promise.then(function(results) {
				
				vm.buildings = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
			});
			
		}
		vm.currentPageBuilding  = 1;
        vm.changePageBuilding  = function (page) {
            vm.currentPageBuilding = page;
            refreshBuildings();
		}
		vm.openBuildingDialog = function(){
				var modalContent = $uibModal.open({
					templateUrl: './app/admin/room/templates/newBuilding.html',
					controller: 'buildingDialogController',
					controllerAs: 'buildingDlCtrl',
					resolve:{
						callBackFunction:function(){return refreshBuildings;}
					}
					
				});
		}
		function confirmationDeleteBuilding(itemId){
			BuildingResource.deleteBuilding({buildingId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('BuildingDeleteSuccess'),"success");
				if(vm.buildings.results.length ==1 && vm.currentPageBuilding > 1)
					vm.currentPageBuilding = vm.currentPageBuilding -1;
                    refreshBuildings();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteBuildingDialog = function(name,id){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function() { return id },
					message:function() { return null},
					callBackFunction:function() { return confirmationDeleteBuilding }
				}
				
			});
		}
		
		vm.openEditBuildingDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/admin/room/templates/editBuilding.html',
				controller: 'editBuildingDialogController',
				controllerAs: 'editBuildingDlCtrl',
				resolve:{
					Building:function(){ return angular.copy(vm.buildings.results[index])},
					callBackFunction:function(){return refreshBuildings;}
				}
				
			});
			
		}
		


		function refreshFloors(){
			var k = FloorResource.getAllFloors({ page:vm.currentPageFloor }).$promise.then(function(results) {
				
				vm.floors = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
			});
			
		}
		vm.currentPageFloor  = 1;
        vm.changePageFloor  = function (page) {
            vm.currentPageFloor = page;
            refreshFloors();
		}
		vm.openFloorDialog = function(){
				var modalContent = $uibModal.open({
					templateUrl: './app/admin/room/templates/newFloor.html',
					controller: 'floorDialogController',
					controllerAs: 'floorDlCtrl',
					resolve:{
						callBackFunction:function(){return refreshFloors;}
					}
					
				});
		}
		function confirmationDeleteFloor(itemId){
			FloorResource.deleteFloor({floorId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('FloorDeleteSuccess'),"success");
				if(vm.floors.results.length ==1 && vm.currentPageFloor > 1)
					vm.currentPageFloor = vm.currentPageFloor -1;
                    refreshFloors();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteFloorDialog = function(name,id){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function() { return id },
					message:function() { return null},
					callBackFunction:function() { return confirmationDeleteFloor }
				}
				
			});
		}
		
		vm.openEditFloorDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/admin/room/templates/editFloor.html',
				controller: 'editFloorDialogController',
				controllerAs: 'editFloorDlCtrl',
				resolve:{
					Floor:function(){ return angular.copy(vm.floors.results[index])},
					callBackFunction:function(){return refreshFloors;}
				}
				
			});
			
		}
	}
	
}());
