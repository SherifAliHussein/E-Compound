(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('VideoControlDialogController', ['$scope','$uibModalInstance','$translate','appCONSTANTS' ,'$http' , 'ManageFeatureResource','ToastService','callBackFunction' ,'featureDetail', 'language','featureControlId' ,  VideoControlDialogController])

	function VideoControlDialogController($scope,$uibModalInstance, $translate, appCONSTANTS, $http, ManageFeatureResource,ToastService,callBackFunction,featureDetail,language,featureControlId){
		var vm = this;
        vm.featureDetail = featureDetail;
        vm.language = language;
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		vm.isChanged = false;
		function add(){
            vm.isChanged = true;
			var newFeatureDetail = new ManageFeatureResource();
            newFeatureDetail.link = vm.featureDetail.link;
            newFeatureDetail.featureControlId = featureControlId;
            var model = new FormData();
			model.append('data', JSON.stringify(newFeatureDetail));
			// model.append('file', featureImage);
			$http({
				method: 'POST',
				url: appCONSTANTS.API_URL + 'Features/Detail',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureUpdateSuccess'),"success");
					 vm.isChanged = false;                     
                     $uibModalInstance.dismiss('cancel');
                     callBackFunction();                    
                },
                function(data, status) {
                    vm.isChanged = false;                     
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
            
        }
        function update(){
            vm.isChanged = true;
			var newFeatureDetail = new ManageFeatureResource();
            newFeatureDetail.link = vm.featureDetail.link;
            newFeatureDetail.featureDetailId = vm.featureDetail.featureDetailId;
            newFeatureDetail.featureControlId = featureControlId;
            // newFeatureDetail.$updateFeatureDetail().then(
            //     function(data, status) {
			// 		ToastService.show("right","bottom","fadeInUp",$translate.instant('RoomAddSuccess'),"success");
			// 		$uibModalInstance.dismiss('cancel');
			// 		callBackFunction();
            //     },
            //     function(data, status) {
			// 		ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            //     }
            // );
            var model = new FormData();
			model.append('data', JSON.stringify(newFeatureDetail));
			// model.append('file', featureImage);
			$http({
				method: 'PUT',
				url: appCONSTANTS.API_URL + 'Features/Detail',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureUpdateSuccess'),"success");
					 vm.isChanged = false;
                     $uibModalInstance.dismiss('cancel');
                     callBackFunction();  
                },
                function(data, status) {
                    vm.isChanged = false;                     
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
        }
        if(featureDetail == null){
            vm.save = add;
            vm.featureDetail = {};
        }
        else{
            vm.save = update;
            vm.featureDetail = featureDetail;
        }
	}	
}());
