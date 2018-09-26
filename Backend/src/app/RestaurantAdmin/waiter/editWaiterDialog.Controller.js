(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editWaiterDialogController', ['$uibModalInstance','$translate', 'WaiterResource','ToastService','waiter', 'branches','callBackFunction','selectedLanguage',  editWaiterDialogController])

	function editWaiterDialogController($uibModalInstance, $translate, WaiterResource,ToastService,  waiter, branches, callBackFunction,selectedLanguage){
		var vm = this;
		vm.menuName = "";
        vm.waiter = waiter;
        vm.waiter.confirmPassword = waiter.password;
        vm.selectedLanguage = selectedLanguage;
        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
        }
        vm.Branches = branches;
		if(branches.length>0){
            branches.forEach(function(element) {
                if(element.branchId == vm.waiter.branchId){
                    vm.selectedBranch = element;
                }
            }, this);
		}
		vm.updateWaiter = function(){
			var newWaiter = new WaiterResource();
            newWaiter.userName = vm.waiter.userName;
            newWaiter.name = vm.waiter.name;
            newWaiter.password = vm.waiter.password;
            newWaiter.userId = waiter.userId;
            newWaiter.branchId = vm.selectedBranch.branchId;
            newWaiter.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('WaiterUpdateSuccess'),"success");
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
