(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('sideItemDialogController', ['$uibModalInstance','$translate' , 'SideItemResource','ToastService','callBackFunction','$rootScope',  sideItemDialogController])

	function sideItemDialogController($uibModalInstance, $translate , SideItemResource,ToastService,callBackFunction,$rootScope){
		var vm = this;
        vm.sideItemName = "";
        vm.value;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		vm.isChanged = false;
		vm.AddNewSideItem = function(){
            vm.isChanged = true;
			var newSideItem = new SideItemResource();
            newSideItem.sideItemName = vm.sideItemName;
            newSideItem.value = vm.value;
            newSideItem.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('SideItemAddSuccess'),"success");
					$uibModalInstance.dismiss('cancel');
                    callBackFunction();
                    vm.isChanged = false;
                },
                function(data, status) {
                    vm.isChanged = false;
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
		}
	}	
}());
