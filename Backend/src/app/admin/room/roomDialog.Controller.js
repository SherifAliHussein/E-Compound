(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('roomDialogController', ['$scope','$uibModalInstance','$translate' , 'RoomResource','ToastService','callBackFunction','Buildings' ,'Floors' ,  roomDialogController])

	function roomDialogController($scope,$uibModalInstance, $translate , RoomResource,ToastService,callBackFunction,Buildings,Floors){
		var vm = this;
		vm.Buildings = Buildings.results;
		vm.Floors = Floors.results;
		vm.selectedBuilding = vm.Buildings.length >0 ? vm.Buildings[0]:null;
		vm.selectedFloor = vm.Floors.length >0 ? vm.Floors[0]:null;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		
		vm.AddNewRoom = function(){
			var newRoom = new RoomResource();
            newRoom.roomName = vm.roomName;
            newRoom.name = vm.name;
			newRoom.password = vm.password;
			newRoom.buildingId = vm.selectedBuilding.buildingId;
			newRoom.floorId = vm.selectedFloor.floorId;
            newRoom.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('RoomAddSuccess'),"success");
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
