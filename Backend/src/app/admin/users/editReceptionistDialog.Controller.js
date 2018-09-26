(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editReceptionistDialogController', ['$uibModalInstance','$translate', 'ReceptionistResource','ToastService','Receptionist','callBackFunction',  editReceptionistDialogController])

	function editReceptionistDialogController($uibModalInstance, $translate, ReceptionistResource,ToastService,  Receptionist, callBackFunction){
		var vm = this;
        vm.Receptionist = Receptionist;
        vm.Receptionist.confirmPassword = Receptionist.password;
        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
        }
		vm.updateReceptionist = function(){
			var updateReceptionist = new ReceptionistResource();
            updateReceptionist.userName = vm.Receptionist.userName;
            updateReceptionist.name = vm.Receptionist.name;
            updateReceptionist.password = vm.Receptionist.password;
            updateReceptionist.userId = Receptionist.userId;
            updateReceptionist.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('ReceptionistUpdateSuccess'),"success");
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
