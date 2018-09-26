(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('showCategoryDialogController', ['$uibModalInstance','$translate', 'MenuResource','ToastService','GetCategoriesResource','category',  showCategoryDialogController])

	function showCategoryDialogController($uibModalInstance, $translate, MenuResource,ToastService,GetCategoriesResource, category){
		var vm = this;
		vm.menuName = "";
		
		vm.categories = category; 
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
	 
	}	
}());
