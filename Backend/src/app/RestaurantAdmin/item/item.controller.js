(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('ItemController', ['$scope','$translate', '$stateParams', 'appCONSTANTS','$uibModal', 'GetItemsResource', 'ItemResource','itemsPrepService','ToastService' ,'ActivateItemResource' ,'DeactivateItemResource',  ItemController])

    function ItemController($scope,$translate,$stateParams, appCONSTANTS,$uibModal, GetItemsResource,ItemResource,itemsPrepService,ToastService, ActivateItemResource, DeactivateItemResource){
		
        var vm = this;
		vm.items = itemsPrepService;
		//vm.totalCount = restaurantsPrepService.totalCount;
		//$('.pmd-sidebar-nav>li>a').removeClass("active")
		//$($('.pmd-sidebar-nav').children()[2].children[0]).addClass("active")
		
        vm.Now = $scope.getCurrentTime();		
		function refreshItems(){
			var k = GetItemsResource.getAllItems({ CategoryId: $stateParams.categoryId, page:vm.currentPage}).$promise.then(function(results) {
				vm.items = results//.results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshItems();
		}
		
		
		function confirmationDelete(itemId){
			ItemResource.deleteItem({itemId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('itemDeleteSuccess'),"success");
				refreshItems();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
		}
		vm.openDeleteItemDialog = function(name,id){			
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
		
		vm.Activate = function(item){
			ActivateItemResource.Activate({itemId:item.itemID})
			.$promise.then(function(result){
				item.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.Deactivate = function(item){
			DeactivateItemResource.Deactivate({itemId:item.itemID})
			.$promise.then(function(result){
				item.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}
		
	}
	
}
());
