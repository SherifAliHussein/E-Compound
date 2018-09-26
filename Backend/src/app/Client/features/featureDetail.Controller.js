(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('featureDetailController', ['$scope','$stateParams','$translate', 'appCONSTANTS', 'FeatureResource','feature','language','ToastService',  featureDetailController])

    function featureDetailController($scope,$stateParams ,$translate , appCONSTANTS, FeatureResource,feature,language,ToastService){

        var vm = this;
        vm.feature = feature;
        vm.language = language;
		
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
		
		
		
	}
	
}());
