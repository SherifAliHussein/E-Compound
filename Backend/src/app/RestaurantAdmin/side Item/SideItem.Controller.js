(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('sideItemController', ['$scope','$translate', 'appCONSTANTS','$uibModal', 'SideItemResource','sideItemPrepService','ToastService',  sideItemController])

    function sideItemController($scope ,$translate , appCONSTANTS,$uibModal, SideItemResource,sideItemPrepService,ToastService){

        var vm = this;
		vm.sideItems = sideItemPrepService;
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[3].children[0]).addClass("active")
		
		function refreshSideItems(){
			var k = SideItemResource.getAllSideItems({page:vm.currentPage}).$promise.then(function(results) {
				vm.sideItems = results
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshSideItems();
		}
		vm.openSideItemDialog = function(){		
			if($scope.selectedLanguage != appCONSTANTS.defaultLanguage)
			{
				var englishSideItems;
				var k = SideItemResource.getAllSideItems({pagesize:0, lang: appCONSTANTS.defaultLanguage}).$promise.then(function(results) {
                    englishSideItems = results;
					var modalContent = $uibModal.open({
						templateUrl: './app/RestaurantAdmin/templates/editSideItem.html',
						controller: 'editSideItemDialogController',
						controllerAs: 'editSideItemDlCtrl',
						resolve:{
							mode:function(){return "map"},
							englishSideItems: function(){return englishSideItems.results;},
							sideItem:function(){ return null},
							callBackFunction:function(){return refreshSideItems;}
						}
						
					});
				});
			}
			else{
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/newSideItem.html',
					controller: 'sideItemDialogController',
					controllerAs: 'sideItemDlCtrl',
					resolve:{
						callBackFunction:function(){return refreshSideItems;}
					}
					
				});
			}
		}
		function confirmationDelete(itemId){
			SideItemResource.deleteSideItem({SideItemId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('SideItemDeleteSuccess'),"success");
				refreshSideItems();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            });
		}
		vm.openDeleteSideItemDialog = function(name,id){			
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
		
		vm.openEditSideItemDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/RestaurantAdmin/templates/editSideItem.html',
				controller: 'editSideItemDialogController',
				controllerAs: 'editSideItemDlCtrl',
				resolve:{
					mode:function(){return "edit"},
					englishSideItems: function(){return null;},
					sideItem:function(){ return vm.sideItems.results[index]},
					callBackFunction:function(){return refreshSideItems;}
				}
				
			});
			
		}
		
		
		
	}
	
}
());
