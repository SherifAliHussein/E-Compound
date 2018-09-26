(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editSizeDialogController', ['$state', 'appCONSTANTS','$translate', 'SizeResource','ToastService','sizePrepService',  editSizeDialogController])

	function editSizeDialogController($state, appCONSTANTS, $translate, SizeResource,ToastService, sizePrepService){
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		vm.size = sizePrepService;
		vm.close = function(){
			$state.go('size');
		}
		
		vm.updateSize = function(){
			var updateSize = new SizeResource();
			updateSize.sizeNameDictionary = vm.size.sizeNameDictionary;
			updateSize.sizeId = vm.size.sizeId;
            updateSize.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('UpdateSizeSuccess'),"success");
					$state.go('size');
					
                },
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
		}
	}	
}());
