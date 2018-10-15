(function () {
	'use strict';

	angular
		.module('home')
		.controller('roomDialogController', ['$scope', '$uibModalInstance', '$translate', 'RoomResource', 'ToastService',
			'callBackFunction', 'Units', roomDialogController])

	function roomDialogController($scope, $uibModalInstance, $translate, RoomResource, ToastService,
		callBackFunction, Units) {
		var vm = this;
		vm.Units = Units.results;

		vm.selectedUnit = vm.Units.length > 0 ? vm.Units[0] : null;
		vm.close = function () {
			$uibModalInstance.dismiss('cancel');
		}

		vm.AddNewRoom = function () {
			var newRoom = new RoomResource();
			newRoom.phone = vm.Phone;
			newRoom.lastName = vm.lastName;
			newRoom.firstName = vm.firstName;
			newRoom.roomName = vm.roomName;
			newRoom.password = vm.password;
			newRoom.unitId = vm.selectedUnit.unitId;
			newRoom.$create().then(
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", $translate.instant('RoomAddSuccess'), "success");
					$uibModalInstance.dismiss('cancel');
					callBackFunction();
				},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
				}
			);
		}
	}
}());
