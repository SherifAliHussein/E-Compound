(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('technicianDialogController', ['$scope','$uibModalInstance','$translate' , 'TechnicianResource','ToastService','categories','callBackFunction','selectedLanguage',  technicianDialogController])

	function technicianDialogController($scope,$uibModalInstance, $translate , TechnicianResource,ToastService, categories,callBackFunction,selectedLanguage){
        var vm = this;
        vm.categories = categories;
        vm.SelectedCategory= [];
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		vm.selectedLanguage = selectedLanguage
		vm.AddNewTechnician = function(){
			var newTechnician = new TechnicianResource();
            newTechnician.userName = vm.userName;
            newTechnician.name = vm.name;
            newTechnician.password = vm.password;
            newTechnician.userCategories = [];
			
         	vm.SelectedCategory.forEach(function(element) {
            	newTechnician.userCategories.push(element);
            }, this);
            
            newTechnician.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('TechnicianAddSuccess'),"success");
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
