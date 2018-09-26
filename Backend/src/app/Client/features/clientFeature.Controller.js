(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('clientFeatureController', ['$scope','$uibModal','$translate', 'appCONSTANTS', 'FeatureResource','featuresPrepService','RequestResource','ToastService',  clientFeatureController])

    function clientFeatureController($scope,$uibModal ,$translate , appCONSTANTS, FeatureResource,featuresPrepService,RequestResource,ToastService){

        var vm = this;
        vm.features = featuresPrepService;
		vm.Now = $scope.getCurrentTime();
		
		
		function refreshFeatures(){
			var k = FeatureResource.getAllActivatedFeatures({ page:vm.currentPage }).$promise.then(function(results) {
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
        vm.request = function(featureId,featureName){
            var modalContent = $uibModal.open({
				templateUrl: './app/Client/features/templates/ConfirmRequestDialog.html',
				controller: 'confirmRequestDialogController',
				controllerAs: 'requestDlCtrl',
				resolve: {
					itemName: function () { return featureName },
					itemId: function() { return featureId },
					callBackFunction:function() { return confirmRequest }
				}
				
			});
            // var newRequest = new RequestResource();
            // newRequest.featureId = featureId;
            // newRequest.$create().then(
            //     function(data, status) {
			// 		ToastService.show("right","bottom","fadeInUp",$translate.instant('RequestSuccess'),"success");
            //     },
            //     function(data, status) {
			// 		ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            //     }
            // );
        }
        function confirmRequest(featureId){
            var newRequest = new RequestResource();
            newRequest.featureId = featureId;
            newRequest.$create().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('RequestSuccess'),"success");
                },
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
        }
		vm.openFeature = function(featureId){
            FeatureResource.getFeature({featureId: featureId}).$promise.then(function(results) {
                var modalContent = $uibModal.open({
                    templateUrl: './app/Client/features/templates/featureDetail.html',
                    controller: 'featureDetailController',
                    controllerAs: 'featureDetailCtrl',
                    resolve:{
                        feature:function(){return results;},
                        language:function(){return $scope.selectedLanguage;}
                    }
                    
                });
            },
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
                
        }
		
		
	}
	
}());
