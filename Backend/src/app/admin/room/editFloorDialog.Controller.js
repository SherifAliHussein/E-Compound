(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editFloorDialogController', ['$uibModalInstance','$translate', 'FloorResource','ToastService','Floor','callBackFunction',  editFloorDialogController])

	function editFloorDialogController($uibModalInstance, $translate, FloorResource,ToastService,  Floor, callBackFunction){
		var vm = this;
        vm.Floor = Floor;
        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
        }
		vm.updateFloor = function(){
			var updateFloor = new FloorResource();
            updateFloor.FloorName = vm.Floor.floorName;
            updateFloor.FloorId = Floor.floorId;
            updateFloor.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('FloorUpdateSuccess'),"success");
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
