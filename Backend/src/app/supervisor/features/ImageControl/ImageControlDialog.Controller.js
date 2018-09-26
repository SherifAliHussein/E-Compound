(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('ImageControlDialogController', ['$scope','$uibModalInstance','$translate','appCONSTANTS' ,'$http' , 'ManageFeatureResource','ToastService','callBackFunction' ,'featureDetail', 'language','featureControlId' ,  ImageControlDialogController])

	function ImageControlDialogController($scope,$uibModalInstance, $translate, appCONSTANTS, $http, ManageFeatureResource,ToastService,callBackFunction,featureDetail,language,featureControlId){
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
            // newFeatureDetail.descriptionDictionary = vm.featureDetail.descriptionDictionary;
            // newFeatureDetail.isFree= vm.featureDetail.isFree;
            newFeatureDetail.isImageChange= isImageChange;
            // newFeatureDetail.price = vm.featureDetail.isFree?0:vm.featureDetail.price;
            newFeatureDetail.featureControlId = featureControlId;
            var model = new FormData();
			model.append('data', JSON.stringify(newFeatureDetail));
			model.append('file', image);
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
            // newFeatureDetail.descriptionDictionary = vm.featureDetail.descriptionDictionary;
            newFeatureDetail.featureDetailId = vm.featureDetail.featureDetailId;
            // newFeatureDetail.isFree= vm.featureDetail.isFree;
            // newFeatureDetail.price = vm.featureDetail.isFree?0:vm.featureDetail.price;
            newFeatureDetail.isImageChange= isImageChange;            
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
			model.append('file', image);
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
        vm.LoadUpload = function () {
            $("#logoImage").click();
        }
        var image;
        var isImageChange = false;
        
        $scope.AddImage = function (element) {
            var logoFile = element[0];

            var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

            if (logoFile && logoFile.size >= 0 && ((logoFile.size / (1024 * 1000)) < 2)) {

                if (allowedImageTypes.indexOf(logoFile.type) !== -1) {
                    $scope.newForm.$dirty = true;
                    $scope.$apply(function () {

                        image = logoFile;
                        isImageChange = true;                        
                        var reader = new FileReader();

                        reader.onloadend = function () {
                            vm.featureDetail.imageURL = reader.result;
                            // $scope.Photo = reader.result;
                            $scope.$apply();
                        };
                        if (logoFile) {
                            reader.readAsDataURL(logoFile);
                        }
                    })
                } else {
                    $("#logoImage").val('');
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('imageTypeError'), "error");
                }

            } else {
                if (logoFile) {
                    $("#logoImage").val('');
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('imgaeSizeError'), "error");
                }

            }


        }
        if(featureDetail == null){
            vm.save = add;
            vm.featureDetail = {};
            vm.featureDetail.isFree = true;
        }
        else{
            vm.save = update;
            vm.featureDetail = featureDetail;
        }
	}	
}());
