(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('supervisorDialogController', ['$scope','$uibModalInstance','$translate' , 'SupervisorResource','ToastService','features', 'categories','callBackFunction','selectedLanguage',  supervisorDialogController])

	function supervisorDialogController($scope,$uibModalInstance, $translate , SupervisorResource,ToastService, features, categories,callBackFunction,selectedLanguage){
        var vm = this;
        vm.features = features.results;
        vm.categories = categories;
        vm.SelectedFeature= [];
        vm.SelectedCategory= [];
        vm.showCategories = false;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
        vm.selectedLanguage = selectedLanguage;
        
        vm.featureChange = function(){
            vm.SelectedFeature;
            for(var i=0; i<vm.SelectedFeature.length; i++){
                if(vm.SelectedFeature[i].type == "Ticket"){
                    vm.showCategories = true;
                }
            }
            if(vm.SelectedFeature.length == 0){
                vm.showCategories = false;
            }
		}

		vm.AddNewSupervisor = function(){
			var newSupervisor = new SupervisorResource();
            newSupervisor.userName = vm.userName;
            newSupervisor.name = vm.name;
            newSupervisor.password = vm.password;
            newSupervisor.features = [];
            newSupervisor.userCategories = [];
			
         	vm.SelectedFeature.forEach(function(element) {
            	newSupervisor.features.push(element);
            }, this);

            vm.SelectedCategory.forEach(function(element) {
            	newSupervisor.userCategories.push(element);
            }, this);
            
            newSupervisor.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('SupervisorAddSuccess'),"success");
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
