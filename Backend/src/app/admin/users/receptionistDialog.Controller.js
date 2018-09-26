(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('receptionistDialogController', ['$scope','$uibModalInstance','$translate' , 'ReceptionistResource','ToastService','callBackFunction','$rootScope',  receptionistDialogController])

	function receptionistDialogController($scope,$uibModalInstance, $translate , ReceptionistResource,ToastService,callBackFunction,$rootScope){
		var vm = this;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		
		vm.AddNewReceptionist = function(){
			var newReceptionist = new ReceptionistResource();
            newReceptionist.userName = vm.userName;
            newReceptionist.name = vm.name;
			newReceptionist.password = vm.password;
            newReceptionist.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('ReceptionistAddSuccess'),"success");
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
