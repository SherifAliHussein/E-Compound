(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('sizeDialogController', ['$state', 'appCONSTANTS','$translate' , 'SizeResource','ToastService','$rootScope',  sizeDialogController])

	function sizeDialogController($state, appCONSTANTS, $translate , SizeResource,ToastService,$rootScope){
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		vm.close = function(){
			$state.go('size');
		}
		vm.isChanged = false;
		vm.AddNewSize = function(){
			vm.isChanged = true;
			var newSize = new SizeResource();
            newSize.sizeNameDictionary = vm.sizeNameDictionary;
            newSize.$create().then(
                function(data, status) {
					vm.isChanged = false;
					ToastService.show("right","bottom","fadeInUp",$translate.instant('sizeAddSuccess'),"success");
					$state.go('size');
                },
                function(data, status) {
					vm.isChanged = false;
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
		}
	}	
})();
