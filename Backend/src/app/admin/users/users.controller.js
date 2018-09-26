(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('usersController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal', 'ReceptionistResource'
        ,'ActivateReceptionistResource','DeactivateReceptionistResource','SupervisorResource','ActivateSupervisorResource','DeactivateSupervisorResource',
        'ReceptionistsPrepService','SupervisorsPrepService','ToastService','FeatureResource',  usersController])

    function usersController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal, ReceptionistResource,
        ActivateReceptionistResource,DeactivateReceptionistResource,SupervisorResource,ActivateSupervisorResource,DeactivateSupervisorResource,
        ReceptionistsPrepService,SupervisorsPrepService,ToastService,FeatureResource){

        var vm = this;
		vm.receptionists = ReceptionistsPrepService;
		vm.supervisors = SupervisorsPrepService;
		
		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[1].children[0]).addClass("active")
		
		function refreshReceptionists(){
			var k = ReceptionistResource.getAllReceptionists({ page:vm.currentPageReceptionists }).$promise.then(function(results) {
				
				vm.receptionists = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPageReceptionists = 1;
        vm.changePageReceptionists = function (page) {
            vm.currentPageReceptionists = page;
            refreshReceptionists();
		}
		vm.openReceptionistDialog = function(){
				var modalContent = $uibModal.open({
					templateUrl: './app/admin/users/templates/newReceptionist.html',
					controller: 'receptionistDialogController',
					controllerAs: 'receptionistDlCtrl',
					resolve:{
						callBackFunction:function(){return refreshReceptionists;}
					}
					
				});
		}
		function confirmationDeleteReceptionists(itemId){
			ReceptionistResource.deleteReceptionist({receptionistId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('ReceptionistDeleteSuccess'),"success");
				if(vm.receptionists.results.length ==1 && vm.currentPageReceptionists > 1)
					vm.currentPageReceptionists = vm.currentPageReceptionists -1;
				refreshReceptionists();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteReceptionistDialog = function(name,id){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function() { return id },
					message:function() { return null},
					callBackFunction:function() { return confirmationDeleteReceptionists }
				}
				
			});
		}
		
		vm.openEditReceptionistDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/admin/users/templates/editReceptionist.html',
				controller: 'editReceptionistDialogController',
				controllerAs: 'editReceptionistDlCtrl',
				resolve:{
					Receptionist:function(){ return angular.copy(vm.receptionists.results[index])},
					callBackFunction:function(){return refreshReceptionists;}
				}
				
			});
			
		}
		vm.ActivateReceptionist = function(receptionist){
			ActivateReceptionistResource.Activate({receptionistId:receptionist.receptionistId})
			.$promise.then(function(result){
				receptionist.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.DeactivateReceptionist = function(receptionist){
			DeactivateReceptionistResource.Deactivate({receptionistId:receptionist.receptionistId})
			.$promise.then(function(result){
				receptionist.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}		
		
		
        

        function refreshSupervisors(){
			var k = SupervisorResource.getAllSupervisors({ page:vm.currentPageSupervisors }).$promise.then(function(results) {
				
				vm.supervisors = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPageSupervisors = 1;
        vm.changePageSupervisors = function (page) {
            vm.currentPageSupervisors = page;
            refreshSupervisors();
		}
		vm.openSupervisorDialog = function(){
            FeatureResource.getAllActivatedFeatures({ pageSize : 0 }).$promise.then(function(results) {
                var modalContent = $uibModal.open({
					templateUrl: './app/admin/users/templates/newSupervisor.html',
					controller: 'supervisorDialogController',
					controllerAs: 'supervisorDlCtrl',
					resolve:{
                        features:function(){ return results},
                        callBackFunction:function(){return refreshSupervisors;},
                        selectedLanguage:function(){return $scope.selectedLanguage;}
					}
					
                });			
            },
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
				
		}
		function confirmationDeleteSupervisor(itemId){
			SupervisorResource.deleteSupervisor({supervisorId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('ReceptionistDeleteSuccess'),"success");
				if(vm.supervisors.results.length ==1 && vm.currentPageSupervisors > 1)
					vm.currentPageSupervisors = vm.currentPageSupervisors -1;
				refreshSupervisors();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteSupervisorDialog = function(name,id){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function() { return id },
					message:function() { return null},
					callBackFunction:function() { return confirmationDeleteSupervisor }
				}
				
			});
		}
		
		vm.openEditSupervisorDialog = function(index){
            FeatureResource.getAllActivatedFeatures({ pageSize : 0 }).$promise.then(function(results) {
                var modalContent = $uibModal.open({
                    templateUrl: './app/admin/users/templates/editSupervisor.html',
                    controller: 'editSupervisorDialogController',
                    controllerAs: 'editSupervisorDlCtrl',
                    resolve:{
                        Supervisor:function(){ return angular.copy(vm.supervisors.results[index])},
                        features:function(){ return results},                        
                        callBackFunction:function(){return refreshSupervisors;},
                        selectedLanguage:function(){return $scope.selectedLanguage;}
                    }
                    
                });
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.ActivateSupervisor = function(supervisor){
			ActivateSupervisorResource.Activate({supervisorId:supervisor.supervisorId})
			.$promise.then(function(result){
				supervisor.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.DeactivateSupervisor = function(receptionist){
			DeactivateSupervisorResource.Deactivate({supervisorId:supervisor.supervisorId})
			.$promise.then(function(result){
				supervisor.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}		
	}
	
}());
