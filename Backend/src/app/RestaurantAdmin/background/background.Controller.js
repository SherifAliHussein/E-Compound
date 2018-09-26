(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('BackgroundController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal','BackgroundResource', 'ActivatebackgroundResource','DeactivateBackgroundResource','backgroundsPrepService','ToastService',  BackgroundController])

    function BackgroundController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal,BackgroundResource, ActivatebackgroundResource,DeactivateBackgroundResource,backgroundsPrepService,ToastService){

        var vm = this;
		vm.Backgrounds = backgroundsPrepService;
		console.log(vm.Backgrounds);
		vm.Now = $scope.getCurrentTime();
		$('.pmd-sidebar-nav>li>a').removeClass("active")	
		$($('.pmd-sidebar-nav').children()[3].children[0]).addClass("active")
		
		function refreshBackgrounds(){
			var k = BackgroundResource.getAllBackgrounds({page:vm.currentPage }).$promise.then(function(results) {
				vm.Backgrounds = results
				console.log(vm.Backgrounds);
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshBackgrounds();
		}
		vm.openbackgroundDialog = function(){		
			 
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/newBackground.html',
					controller: 'backgroundDialogController',
					controllerAs: 'backgroundCtrl',
					resolve:{ 
						callBackFunction:function(){return refreshBackgrounds;
						}
					}
					
				});
		 
		}
		function confirmationDelete(itemId){
			backgroundResource.deletebackground({backgroundId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('backgroundDeleteSuccess'),"success");
				refreshBackgrounds();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeletebackgroundDialog = function(name,id){			
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
		
		vm.openEditbackgroundDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/RestaurantAdmin/templates/editbackground.html',
				controller: 'editbackgroundDialogController',
				controllerAs: 'editbackgroundDlCtrl',
				resolve:{
					mode:function(){return "edit"},
					englishBackgrounds: function(){return null;},
					background:function(){ return vm.Backgrounds.results[index]},
					callBackFunction:function(){return refreshBackgrounds;}
				}
				
			});
			
		}
		vm.Activate = function(background){
			ActivatebackgroundResource.Activate({BackgroundId:background.backgroundId})
			.$promise.then(function(result){
				background.isActive = true;
				refreshBackgrounds();
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.Deactivate = function(background){
			DeactivatebackgroundResource.Deactivate({backgroundId:background.backgroundId})
			.$promise.then(function(result){
				background.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}		
		
		
		
	}
	
}
    ());
