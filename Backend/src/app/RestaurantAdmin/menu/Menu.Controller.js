(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('menuController', ['$scope','$translate', 'appCONSTANTS','$uibModal', 'MenuResource','menusPrepService','RestaurantIsReadyPrepService','ToastService','ActivateMenuResource','DeactivateMenuResource','PublishRestaurantResource',  menuController])

    function menuController($scope ,$translate , appCONSTANTS,$uibModal, MenuResource,menusPrepService,RestaurantIsReadyPrepService,ToastService,ActivateMenuResource,DeactivateMenuResource,PublishRestaurantResource){

        var vm = this;
		vm.menus = menusPrepService;
		vm.RestaurantIsReady = RestaurantIsReadyPrepService.isReady;
		vm.Now = $scope.getCurrentTime();
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[0].children[0]).addClass("active")
		
		function refreshMenu(){
			var k = MenuResource.getAllMenus({page:vm.currentPage}).$promise.then(function(results) {
				vm.Now = $scope.getCurrentTime();	
				vm.menus = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshMenu();
		}
		vm.openMenuDialog = function(){		
			if($scope.selectedLanguage != appCONSTANTS.defaultLanguage)
			{
				var englishMenus;
				var k = MenuResource.getAllMenus({pagesize:0, lang: appCONSTANTS.defaultLanguage}).$promise.then(function(results) {
					englishMenus = results;
					var modalContent = $uibModal.open({
						templateUrl: './app/RestaurantAdmin/templates/editMenu.html',
						controller: 'editMenuDialogController',
						controllerAs: 'editMenuDlCtrl',
						resolve:{
							mode:function(){return "map"},
							englishMenus: function(){return englishMenus.results;},
							menu:function(){ return null},
							callBackFunction:function(){return refreshMenu;}
						}
						
					});
				});
			}
			else{
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/newMenu.html',
					controller: 'menuDialogController',
					controllerAs: 'menuDlCtrl',
					resolve:{
						callBackFunction:function(){return refreshMenu;}
					}
					
				});
			}
		}
		function confirmationDelete(itemId){
			MenuResource.deleteMenu({menuId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('menuDeleteSuccess'),"success");
				refreshMenu();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteMenuDialog = function(name,id){			
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
		
		vm.openEditMenuDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/RestaurantAdmin/templates/editMenu.html',
				controller: 'editMenuDialogController',
				controllerAs: 'editMenuDlCtrl',
				resolve:{
					mode:function(){return "edit"},
					englishMenus: function(){return null;},
					menu:function(){ return vm.menus.results[index]},
					callBackFunction:function(){return refreshMenu;}
				}
				
			});
			
		}
		
		vm.Activate = function(menu){
			ActivateMenuResource.Activate({MenuId:menu.menuId})
			.$promise.then(function(result){
				menu.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.Deactivate = function(menu){
			DeactivateMenuResource.Deactivate({MenuId:menu.menuId})
			.$promise.then(function(result){
				menu.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}
		vm.Publish = function(){
			PublishRestaurantResource.Publish()
			.$promise.then(function(result){
				vm.RestaurantIsReady = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}
		
		
	}
	
}
    ());
