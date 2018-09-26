(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('branchDialogController', ['$scope','$state','appCONSTANTS','$http','$translate' , 'BranchResource','ToastService','$rootScope',  branchDialogController])

	function branchDialogController($scope, $state , appCONSTANTS,$http, $translate , BranchResource,ToastService,$rootScope){
		var vm = this;
        vm.language = appCONSTANTS.supportedLanguage;
        
		vm.close = function(){
            $state.go('branch');            
		}
		vm.isChanged = false;
		vm.AddNewBranch = function(){
			vm.isChanged = true;
            var newBranch = new BranchResource();
            newBranch.branchTitleDictionary = vm.branchTitleDictionary;
            newBranch.branchAddressDictionary = vm.branchAddressDictionary;
            newBranch.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('BranchAddSuccess'),"success");
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
}());
