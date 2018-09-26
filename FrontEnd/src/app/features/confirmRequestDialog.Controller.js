(function () {
    'use strict';	
    angular
        .module('home')
        .controller('confirmRequestDialogController', ['$uibModalInstance', 'itemName','itemId', 'callBackFunction',  confirmRequestDialogController])

	function confirmRequestDialogController($uibModalInstance, itemName,itemId, callBackFunction){
		var vm = this;
		vm.itemName = itemName;
		
		vm.close = function(){
			$uibModalInstance.dismiss();
		}
		
		vm.Confirm = function(){
			callBackFunction(itemId);
			$uibModalInstance.dismiss();
        }
		
	}	
}());
