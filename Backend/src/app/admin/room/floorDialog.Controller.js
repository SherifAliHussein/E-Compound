(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('floorDialogController', ['$scope','$uibModalInstance','$translate' , 'FloorResource','ToastService','callBackFunction',  floorDialogController])

	function floorDialogController($scope,$uibModalInstance, $translate , FloorResource,ToastService,callBackFunction){
		var vm = this;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		
		vm.AddNewFloor = function(){
			var newFloor = new FloorResource();
            newFloor.floorName = vm.floorName;
            newFloor.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('FloorAddSuccess'),"success");
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
