(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editSideItemDialogController', ['$uibModalInstance','$translate', 'SideItemResource','ToastService','mode','englishSideItems','sideItem','callBackFunction',  editSideItemDialogController])

	function editSideItemDialogController($uibModalInstance, $translate, SideItemResource,ToastService, mode, englishSideItems, sideItem,callBackFunction){
		var vm = this;
		vm.sideItemName = "";
		
		vm.mode = mode;
		vm.englishSideItems = englishSideItems;
        if(mode == "edit"){
            vm.sideItemName = sideItem.sideItemName;
            vm.value = sideItem.value;
        }
		else
			vm.selectedSideItem = englishSideItems[0];
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		
		vm.updateSideItem = function(){
			var updateSideItem = new SideItemResource();
            updateSideItem.sideItemName = vm.sideItemName;
            
			if(mode == "edit"){
				updateSideItem.sideItemId = sideItem.sideItemId;
				updateSideItem.value = vm.value;
			}
			else{
				updateSideItem.sideItemId = vm.selectedSideItem.sideItemId;
				updateSideItem.value = vm.selectedSideItem.value;
			}
            updateSideItem.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('UpdateSideItemSuccess'),"success");
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
