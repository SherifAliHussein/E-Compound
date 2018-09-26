(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editRestaurantTypeDialogController', ['$state', 'appCONSTANTS', 'RestaurantTypeResource','ToastService','restaurantTypePrepService','$translate',  editRestaurantTypeDialogController])

	function editRestaurantTypeDialogController($state, appCONSTANTS, RestaurantTypeResource,ToastService, restaurantTypePrepService ,$translate){
		var vm = this;
		vm.typeName = "";
		vm.language = appCONSTANTS.supportedLanguage;
		
		vm.restaurantType = restaurantTypePrepService;
		
		vm.updateType = function(){
			var newType = new RestaurantTypeResource();
			
			newType.restaurantTypeId = vm.restaurantType.restaurantTypeId;
			newType.typeNameDictionary = vm.restaurantType.typeNameDictionary;
            newType.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('RestaurantTypeUpdateSuccess'),"success");
					$state.go('restaurantType');
					
                },
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
		}
	}	
}());
