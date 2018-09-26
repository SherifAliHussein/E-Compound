(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('supervisorDialogController', ['$scope','$uibModalInstance','$translate' , 'SupervisorResource','ToastService','features','callBackFunction','selectedLanguage',  supervisorDialogController])

	function supervisorDialogController($scope,$uibModalInstance, $translate , SupervisorResource,ToastService, features,callBackFunction,selectedLanguage){
        var vm = this;
        vm.features = features.results;
        vm.SelectedFeature= [];
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		vm.selectedLanguage = selectedLanguage
		vm.AddNewSupervisor = function(){
			var newSupervisor = new SupervisorResource();
            newSupervisor.userName = vm.userName;
            newSupervisor.name = vm.name;
            newSupervisor.password = vm.password;
            newSupervisor.features = [];
			
         	vm.SelectedFeature.forEach(function(element) {
            	newSupervisor.features.push(element);
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
