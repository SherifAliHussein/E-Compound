(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('buildingDialogController', ['$scope','$uibModalInstance','$translate' , 'BuildingResource','ToastService','callBackFunction',  buildingDialogController])

	function buildingDialogController($scope,$uibModalInstance, $translate , BuildingResource,ToastService,callBackFunction){
		var vm = this;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		
		vm.AddNewBuilding = function(){
			var newBuliding = new BuildingResource();
            newBuliding.buildingName = vm.buildingName;
            newBuliding.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('BuildingAddSuccess'),"success");
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
