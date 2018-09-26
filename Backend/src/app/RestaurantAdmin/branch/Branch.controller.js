(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('branchController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal', 'BranchResource','ActivateBranchResource','DeactivateBranchResource','branchsPrepService','ToastService',  branchController])

    function branchController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal, BranchResource,ActivateBranchResource,DeactivateBranchResource,branchsPrepService,ToastService){

        var vm = this;
		vm.branches = branchsPrepService;
		vm.Now = $scope.getCurrentTime();
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[5].children[0]).addClass("active")
		
		function refreshBranches(){
			var k = BranchResource.getAllBranches({ page:vm.currentPage }).$promise.then(function(results) {
				vm.Now = $scope.getCurrentTime();	
				vm.branches = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshBranches();
		}
		vm.openBranchDialog = function(){		
			if($scope.selectedLanguage != appCONSTANTS.defaultLanguage)
			{
				var englishBranches;
				var k = BranchResource.getAllBranches({pagesize:0, lang: appCONSTANTS.defaultLanguage}).$promise.then(function(results) {
					englishBranches = results;
					var modalContent = $uibModal.open({
						templateUrl: './app/RestaurantAdmin/templates/editBranch.html',
						controller: 'editBranchDialogController',
						controllerAs: 'editBranchDlCtrl',
						resolve:{
							mode:function(){return "map"},
							englishBranches: function(){return englishBranches.results;},
							branch:function(){ return null},
							callBackFunction:function(){return refreshBranches;}
						}
						
					});
				});
			}
			else{
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/newBranch.html',
					controller: 'branchDialogController',
					controllerAs: 'branchDlCtrl',
					resolve:{
						callBackFunction:function(){return refreshBranches;}
					}
					
				});
			}
		}
		function confirmationDelete(itemId){
			BranchResource.deleteBranch({branchId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('CategoryDeleteSuccess'),"success");
				refreshBranches();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteBranchDialog = function(name,id){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function() { return id },
					message:function() { return null},
					callBackFunction:function() { return confirmationDelete }
				}
				
			});
		}
		
		vm.openEditBranchDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/RestaurantAdmin/templates/editBranch.html',
				controller: 'editBranchDialogController',
				controllerAs: 'editBranchDlCtrl',
				resolve:{
					mode:function(){return "edit"},
					englishBranches: function(){return null;},
					branch:function(){ return vm.branches.results[index]},
					callBackFunction:function(){return refreshBranches;}
				}
				
			});
			
		}
		vm.Activate = function(branch){
			ActivateBranchResource.Activate({branchId:branch.branchId})
			.$promise.then(function(result){
				branch.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.Deactivate = function(branch){
			DeactivateBranchResource.Deactivate({branchId:branch.branchId})
			.$promise.then(function(result){
				branch.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}		
		
		
		
	}
	
}
    ());
