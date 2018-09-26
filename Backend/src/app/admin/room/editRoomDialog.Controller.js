(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editRoomDialogController', ['$uibModalInstance','$translate', 'RoomResource','ToastService','Room','callBackFunction',  'Buildings', 'Floors',  editRoomDialogController])

	function editRoomDialogController($uibModalInstance, $translate, RoomResource,ToastService,  Room, callBackFunction, Buildings, Floors){
		var vm = this;
        vm.Room = Room;
		vm.Buildings = Buildings.results;
		vm.Floors = Floors.results;
        vm.Room.confirmPassword = Room.password;
        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
        }
		vm.updateRoom = function(){
			var updateRoom = new RoomResource();
            updateRoom.roomName = vm.Room.roomName;
            updateRoom.name = vm.Room.name;
            updateRoom.password = vm.Room.password;
            updateRoom.roomId = Room.roomId;
			updateRoom.buildingId = vm.Room.buildingId;
			updateRoom.floorId = vm.Room.floorId;
            updateRoom.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('RoomUpdateSuccess'),"success");
					$uibModalInstance.dismiss('cancel');
					callBackFunction();
                },
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
		}
	}	
}());
