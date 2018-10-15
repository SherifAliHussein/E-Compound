(function () {
	'use strict';

	angular
		.module('home')
		.controller('roomsController', ['$scope', '$stateParams', '$translate', 'appCONSTANTS', '$uibModal', 'RoomResource'
			, 'ActivateRoomResource', 'DeactivateRoomResource', 'RoomsPrepService', 'UnitPrepService', 'roomLimitPrepService', 'ToastService', 'AdminRoomsLimitResource'
			, roomsController])

	function roomsController($scope, $stateParams, $translate, appCONSTANTS, $uibModal, RoomResource,
		ActivateRoomResource, DeactivateRoomResource, RoomsPrepService, UnitPrepService, roomLimitPrepService, ToastService, AdminRoomsLimitResource) {

		var vm = this;
		vm.rooms = RoomsPrepService;
		vm.roomsLimit = roomLimitPrepService;
		vm.unit = UnitPrepService;
		console.log(UnitPrepService);
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[2].children[0]).addClass("active")

		function refreshRooms() {
			var k = RoomResource.getAllRooms({ page: vm.currentPage }).$promise.then(function (results) {

				vm.rooms = results;
			},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
				});
			AdminRoomsLimitResource.getRoomsLimitAndConsumed().$promise.then(function (results) {
				vm.roomsLimit = results;
			},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
				});
		}
		vm.currentPage = 1;
		vm.changePageRooms = function (page) {
			vm.currentPage = page;
			refreshRooms();
		}
		vm.openRoomDialog = function () {
			var modalContent = $uibModal.open({
				templateUrl: './app/admin/room/templates/newRoom.html',
				controller: 'roomDialogController',
				controllerAs: 'roomDlCtrl',
				resolve: {
					callBackFunction: function () { return refreshRooms; },
					Units: function () { return vm.unit; }
				}

			});


		}
		function confirmationDelete(itemId) {
			RoomResource.deleteRoom({ roomId: itemId }).$promise.then(function (results) {
				ToastService.show("right", "bottom", "fadeInUp", $translate.instant('RoomDeleteSuccess'), "success");
				if (vm.rooms.results.length == 1 && vm.currentPage > 1)
					vm.currentPage = vm.currentPage - 1;
				refreshRooms();
			},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
				});
		}
		vm.openDeleteRoomDialog = function (name, id) {
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function () { return id },
					message: function () { return null },
					callBackFunction: function () { return confirmationDelete }
				}

			});
		}

		vm.openEditRoomDialog = function (index) { 
			var modalContent = $uibModal.open({
				templateUrl: './app/admin/room/templates/editRoom.html',
				controller: 'editRoomDialogController',
				controllerAs: 'editRoomDlCtrl',
				resolve: {
					Room: function () { return angular.copy(vm.rooms.results[index]) },
					callBackFunction: function () { return refreshRooms; },
					Units: function () { return vm.unit; }

				}

			});
		}
		vm.ActivateRoom = function (room) {
			ActivateRoomResource.Activate({ roomId: room.roomId })
				.$promise.then(function (result) {
					room.isActive = true;
				},
					function (data, status) {
						ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
					})
		}

		vm.DeactivateRoom = function (room) {
			DeactivateRoomResource.Deactivate({ roomId: room.roomId })
				.$promise.then(function (result) {
					room.isActive = false;
				},
					function (data, status) {
						ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
					})
		}

	}

}());
