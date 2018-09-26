(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('clientFeatureController', ['$scope','$uibModal','$translate', 'appCONSTANTS', 'FeatureResource','featuresPrepService','featureBackgroundPrepService','RequestResource','ToastService','totalCartService',  clientFeatureController])

    function clientFeatureController($scope,$uibModal ,$translate , appCONSTANTS, FeatureResource,featuresPrepService,featureBackgroundPrepService,RequestResource,ToastService,totalCartService){

        var vm = this;
        vm.features = featuresPrepService;
        //vm.Now = $scope.getCurrentTime();
        $scope.$parent.featureBackground = featureBackgroundPrepService;
        localStorage.removeItem('checkOut');
        totalCartService.homeTotalNo = 0;
        vm.featureMode = true;
        $scope.$parent.globalInfo= {};
        $scope.$parent.globalInfo.featureMode = true;
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
        vm.selectedFeatureName;
        vm.selectedFeatureId;
        vm.selectedFeatureIndex;
        vm.request = function(featureId,featureName){
            // var modalContent = $uibModal.open({
			// 	templateUrl: './app/features/templates/ConfirmRequestDialog.html',
			// 	controller: 'confirmRequestDialogController',
			// 	controllerAs: 'requestDlCtrl',
			// 	resolve: {
			// 		itemName: function () { return featureName },
			// 		itemId: function() { return featureId },
			// 		callBackFunction:function() { return confirmRequest }
			// 	}
				
            // });
            
vm.selectedFeatureName = featureName
vm.selectedFeatureId = featureId

        }
        vm.confirmRequest = function(featureId){
            var newRequest = new RequestResource();
            newRequest.featureId = featureId;
            newRequest.$create().then(
                function(data, status) {
					//ToastService.show("right","bottom","fadeInUp",$translate.instant('RequestSuccess'),"success");
                },
                function(data, status) {
                    //ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
        }

        vm.showRestaurants = function(index){
            vm.featureMode = false;
            $scope.$parent.globalInfo.featureMode = false;            
            vm.selectedFeatureIndex = index
        }
		vm.openFeature = function(featureId){
            FeatureResource.getFeature({featureId: featureId}).$promise.then(function(results) {
                var modalContent = $uibModal.open({
                    templateUrl: './app/features/templates/featureDetail.html',
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
