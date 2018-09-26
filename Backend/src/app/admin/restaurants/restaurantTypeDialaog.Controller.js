(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('restaurantTypeDialogController', ['$state', 'appCONSTANTS', 'RestaurantTypeResource','ToastService','$rootScope','$translate',  restaurantTypeDialogController])

	function restaurantTypeDialogController($state, appCONSTANTS, RestaurantTypeResource,ToastService,$rootScope,$translate){
		var vm = this;
		
		vm.typeNameDictionary = {};
		vm.language = appCONSTANTS.supportedLanguage;
		
		vm.AddNewType = function(){
			console.log(vm.typeNameDictionary)
			var newType = new RestaurantTypeResource();
            newType.typeNameDictionary = vm.typeNameDictionary;
            newType.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('RestaurantTypeAddSuccess'),"success");
					$state.go('restaurantType');
                },
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
		}
	}	
}());
