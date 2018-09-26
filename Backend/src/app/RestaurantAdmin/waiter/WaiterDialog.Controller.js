(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('waiterDialogController', ['$uibModalInstance','$translate' , 'WaiterResource','ToastService', 'branches','callBackFunction','selectedLanguage','$rootScope',  waiterDialogController])

	function waiterDialogController($uibModalInstance, $translate , WaiterResource,ToastService,branches,callBackFunction,selectedLanguage,$rootScope){
		var vm = this;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		vm.Branches = branches;
		vm.selectedLanguage = selectedLanguage;
		if(branches.length>0){
			vm.selectedBranch = branches[0];
		}
		
		vm.AddNewWaiter = function(){
			var newWaiter = new WaiterResource();
            newWaiter.userName = vm.userName;
            newWaiter.name = vm.name;
			newWaiter.password = vm.password;
			newWaiter.branchId = vm.selectedBranch.branchId;
            newWaiter.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('WaiterAddSuccess'),"success");
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
