(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('featureController', ['$scope','$stateParams','$translate', 'appCONSTANTS', 'controlEnum','$uibModal', 'FeatureResource','ActivateFeatureResource','DeactivateFeatureResource','featuresPrepService','featureAsRestaurantPrepService','featureAsInvitationPrepService','featureAsTicketPrepService','ToastService',  featureController])

    function featureController($scope,$stateParams ,$translate , appCONSTANTS, controlEnum,$uibModal, FeatureResource,ActivateFeatureResource,DeactivateFeatureResource,featuresPrepService,featureAsRestaurantPrepService,featureAsInvitationPrepService,featureAsTicketPrepService,ToastService){

        var vm = this;
        vm.features = featuresPrepService;
		vm.Now = $scope.getCurrentTime();
		vm.control = controlEnum;
		vm.featureAsRestaurant = featureAsRestaurantPrepService;
		vm.featureAsInvitation = featureAsInvitationPrepService;
		vm.featureAsTicket = featureAsTicketPrepService;
		console.log(vm.features)
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		
		function refreshFeatures(){
			var k = FeatureResource.getAllFeatures({ page:vm.currentPage }).$promise.then(function(results) {
				vm.features = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshFeatures();
		}
		function confirmationDelete(itemId){
			FeatureResource.deleteFeature({featureId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureDeleteSuccess'),"success");
				if(vm.features.results.length ==1 && vm.currentPage > 1)
					vm.currentPage = vm.currentPage -1;
				refreshFeatures();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteFeatureDialog = function(name,id){			
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
        
		vm.Activate = function(feature){
			ActivateFeatureResource.Activate({featureId:feature.featureId})
			.$promise.then(function(result){
				feature.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.Deactivate = function(feature){
			DeactivateFeatureResource.Deactivate({featureId:feature.featureId})
			.$promise.then(function(result){
				feature.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}		
		
		
		
	}
	
}());
