(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editBranchDialogController', ['$scope','$state','$http','$translate','appCONSTANTS', 'BranchResource','ToastService','branchPrepService',  editBranchDialogController])

	function editBranchDialogController($scope, $state ,$http, $translate,appCONSTANTS, BranchResource,ToastService, branchPrepService,callBackFunction){
		var vm = this;
		vm.categoryName = "";
		vm.language = appCONSTANTS.supportedLanguage;
		
		vm.branch = branchPrepService;
		
		vm.close = function(){
			$state.go('branch');
		}
		
		vm.updateBranch = function(){
            var updateBranch = new BranchResource();
            updateBranch.branchTitleDictionary = vm.branch.branchTitleDictionary;
            updateBranch.branchAddressDictionary = vm.branch.branchAddressDictionary;
			updateBranch.branchId = vm.branch.branchId;
			
			updateBranch.$update().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('BranchUpdateSuccess'),"success");
					 vm.isChanged = false;                     
					 $state.go('branch');
                },
                function(data, status) {
                    vm.isChanged = false;                     
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
            
        }
        
	}	
})();
