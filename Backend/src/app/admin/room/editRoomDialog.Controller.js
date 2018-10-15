(function () {
    'use strict';

    angular
        .module('home')
        .controller('editRoomDialogController', ['$uibModalInstance', '$filter', '$translate', 'RoomResource', 'ToastService', 'Room', 'callBackFunction', 'Units', editRoomDialogController])

    function editRoomDialogController($uibModalInstance, $filter, $translate, RoomResource, ToastService, Room, callBackFunction, Units) {
        var vm = this;
        vm.Room = Room;
        vm.Units = Units.results;
        vm.Room.confirmPassword = Room.password;
       debugger;
        var indexType = vm.Units.indexOf($filter('filter')(vm.Units, { 'unitId': vm.Room.unitId }, true)[0]);
        vm.selectedUnit = vm.Units[indexType];

        vm.close = function () {
            $uibModalInstance.dismiss('cancel');
        }
        vm.updateRoom = function () {
            var updateRoom = new RoomResource();
            updateRoom.roomId = vm.Room.roomId;
            updateRoom.phone = vm.Room.phone;
            updateRoom.lastName = vm.Room.lastName;
            updateRoom.firstName = vm.Room.firstName;
            updateRoom.roomName = vm.Room.roomName;
            updateRoom.password = vm.Room.password;
            updateRoom.unitId = vm.selectedUnit.unitId;
            updateRoom.$update().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('RoomUpdateSuccess'), "success");
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
