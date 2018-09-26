(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('WaiterController', ['$scope','$translate', 'appCONSTANTS','$uibModal', 'WaiterResource' ,'BranchResource','waitersPrepService', 'ToastService',  WaiterController])

    function WaiterController($scope ,$translate , appCONSTANTS,$uibModal, WaiterResource , BranchResource ,waitersPrepService,ToastService){

        var vm = this;
		vm.waiters = waitersPrepService;
		/*vm.waitersLimit = WaitersLimitPrepService.waiterLimit;*/
		
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[2].children[0]).addClass("active")
		
		function refreshWaiter(){
			var k = WaiterResource.getAllWaiters({page:vm.currentPage}).$promise.then(function(results) {
				vm.waiters = results
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshWaiter();
		}
		vm.openWaiterDialog = function(){		
			var branches;
			var k = BranchResource.getAllBranches({pagesize:0}).$promise.then(function(results) {
				branches = results;
				
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/newWaiter.html',
					controller: 'waiterDialogController',
					controllerAs: 'waiterDlCtrl',
					resolve:{
						branches: function(){return branches.results;},
						callBackFunction:function(){return refreshWaiter;},
						selectedLanguage:function(){return $scope.selectedLanguage;}
					}
						
				});
			});
        }
		function confirmationDelete(itemId){
			WaiterResource.deleteWaiter({waiterId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('WaiterDeleteSuccess'),"success");
				refreshWaiter();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
		}
		vm.openDeleteWaiterDialog = function(name,id){			
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
		
		vm.openEditWaiterDialog = function(index){
            var waiter;
            waiter=angular.copy(vm.waiters.results[index]);

			var branches;
			var k = BranchResource.getAllBranches({pagesize:0}).$promise.then(function(results) {
				branches = results;
				
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/editWaiter.html',
					controller: 'editWaiterDialogController',
					controllerAs: 'editWaiterDlCtrl',
					resolve:{
						mode:function(){return "edit"},
						waiter:function(){ return waiter},
						branches: function(){return branches.results;},
						callBackFunction:function(){return refreshWaiter;},
						selectedLanguage:function(){return $scope.selectedLanguage;}						
					}
					
				});
			});
			
		}
				
		
	}
	
}
());
