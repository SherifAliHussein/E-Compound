(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editTechnicianDialogController', ['$uibModalInstance','$translate', 'TechnicianResource','ToastService','Technician', 'categories','callBackFunction', 'selectedLanguage',  editTechnicianDialogController])

	function editTechnicianDialogController($uibModalInstance, $translate, TechnicianResource,ToastService,  Technician, categories, callBackFunction,selectedLanguage){
		var vm = this;
        vm.Technician = Technician;
        vm.Technician.confirmPassword = Technician.password;
        vm.selectedLanguage = selectedLanguage;
        vm.categories = categories        
        vm.SelectedCategoryId=[];
        vm.SelectedCategory = [];
        Technician.userCategories.forEach(function(element) {
			var kk = vm.categories.filter(function(item){
				return (item.userCategoryId ===  element.userCategoryId);
              })[0];
              
			vm.SelectedCategoryId.push(element.userCategoryId)
			vm.SelectedCategory.push(element)
        }, this);

        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
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
		vm.updateTechnician = function(){
			var updateTechnician = new TechnicianResource();
            updateTechnician.userName = vm.Technician.userName;
            updateTechnician.name = vm.Technician.name;
            updateTechnician.password = vm.Technician.password;
            updateTechnician.userId = Technician.userId;
			updateTechnician.userCategories = [];
			vm.SelectedCategory.forEach(function(element) {
                updateTechnician.userCategories.push(element);
			}, this);
            updateTechnician.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('TechnicianUpdateSuccess'),"success");
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
