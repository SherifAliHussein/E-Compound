(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('featuresBackgroundController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal','FeaturesBackgroundResource','backgroundsPrepService','ToastService',  featuresBackgroundController])

    function featuresBackgroundController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal,FeaturesBackgroundResource,backgroundsPrepService,ToastService){

        var vm = this;
		vm.Backgrounds = backgroundsPrepService;
		console.log(vm.Backgrounds);
		vm.Now = $scope.getCurrentTime();
		$('.pmd-sidebar-nav>li>a').removeClass("active")	
		$($('.pmd-sidebar-nav').children()[6].children[0]).addClass("active")
		
		function refreshBackgrounds(){
			var k = FeaturesBackgroundResource.getAllBackgrounds({page:vm.currentPage }).$promise.then(function(results) {
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
					templateUrl: './app/admin/featuresbackground/templates/newBackground.html',
					controller: 'featuresbackgroundDialogController',
					controllerAs: 'backgroundCtrl',
					resolve:{ 
						callBackFunction:function(){return refreshBackgrounds;}
					}
					
				});
		 
		}
		
		vm.Activate = function(background){
			FeaturesBackgroundResource.Activate({backgroundId:background.featuresBackgroundId})
			.$promise.then(function(result){
				background.isActive = true;
				refreshBackgrounds();
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		
	}
	
}
    ());
