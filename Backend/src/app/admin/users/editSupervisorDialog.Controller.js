(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editSupervisorDialogController', ['$uibModalInstance','$translate', 'SupervisorResource','ToastService','Supervisor', 'features', 'categories','callBackFunction', 'selectedLanguage',  editSupervisorDialogController])

	function editSupervisorDialogController($uibModalInstance, $translate, SupervisorResource,ToastService,  Supervisor, features, categories, callBackFunction,selectedLanguage){
		var vm = this;
        vm.Supervisor = Supervisor;
        vm.Supervisor.confirmPassword = Supervisor.password;
        vm.selectedLanguage = selectedLanguage;
        vm.features = features.results;        
        vm.SelectedFeatureId=[];
		vm.SelectedFeature = [];
		vm.categories = categories        
        vm.SelectedCategoryId=[];
		vm.SelectedCategory = [];
		
        Supervisor.userCategories.forEach(function(element) {
			var kk = vm.categories.filter(function(item){
				return (item.userCategoryId ===  element.userCategoryId);
              })[0];
              
			vm.SelectedCategoryId.push(element.userCategoryId)
			vm.SelectedCategory.push(element)
		}, this);
		
        Supervisor.features.forEach(function(element) {
			var kk = vm.features.filter(function(item){
				return (item.featureId ===  element.featureId);
              })[0];
              
			vm.SelectedFeatureId.push(element.featureId)
			vm.SelectedFeature.push(element)
        }, this);

        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
        }

		vm.featureChange = function(){
			vm.SelectedFeature = []
			for(var i=0;i<vm.SelectedFeatureId.length;i++){
				var feature = vm.features.filter(function(item){
					return (item.featureId ===  vm.SelectedFeatureId[i]);
				})[0]
				vm.SelectedFeature.push(feature)  
			}
		}

		vm.categoryChange = function(){
			vm.SelectedCategory = []
			for(var i=0;i<vm.SelectedCategoryId.length;i++){
				var feature = vm.categories.filter(function(item){
					return (item.userCategoryId ===  vm.SelectedCategoryId[i]);
				})[0]
				vm.SelectedCategory.push(feature)  
			}
		}

		vm.updateSupervisor = function(){
			var updateSupervisor = new SupervisorResource();
            updateSupervisor.userName = vm.Supervisor.userName;
            updateSupervisor.name = vm.Supervisor.name;
            updateSupervisor.password = vm.Supervisor.password;
            updateSupervisor.userId = Supervisor.userId;
			updateSupervisor.features = [];
			updateSupervisor.userCategories = [];
			vm.SelectedFeature.forEach(function(element) {
                updateSupervisor.features.push(element);
			}, this);

			updateSupervisor.userCategories = [];
			vm.SelectedCategory.forEach(function(element) {
                updateSupervisor.userCategories.push(element);
			}, this);

            updateSupervisor.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('SupervisorUpdateSuccess'),"success");
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
