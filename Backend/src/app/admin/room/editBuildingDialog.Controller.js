(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editBuildingDialogController', ['$uibModalInstance','$translate', 'BuildingResource','ToastService','Building','callBackFunction',  editBuildingDialogController])

	function editBuildingDialogController($uibModalInstance, $translate, BuildingResource,ToastService,  Building, callBackFunction){
		var vm = this;
        vm.Building = Building;
        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
        }
		vm.updateBuilding = function(){
			var updateBuilding = new BuildingResource();
            updateBuilding.buildingName = vm.Building.buildingName;
            updateBuilding.buildingId = Building.buildingId;
            updateBuilding.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('BuildingUpdateSuccess'),"success");
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
