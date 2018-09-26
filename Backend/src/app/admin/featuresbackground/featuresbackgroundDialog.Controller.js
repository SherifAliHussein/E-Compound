(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('featuresbackgroundDialogController', ['$scope','$state','$uibModalInstance','$http','$translate','appCONSTANTS' , 'BackgroundResource','ToastService','callBackFunction','$rootScope',  featuresbackgroundDialogController])

	function featuresbackgroundDialogController($scope, $state , $uibModalInstance,$http, $translate,appCONSTANTS , BackgroundResource,ToastService,callBackFunction,$rootScope){
		var vm = this;
		vm.menuName = "";
		vm.close = function(){
			$uibModalInstance.dismiss('cancel');
		}
		
		vm.AddNewbackground = function(){
            var newbackground = new Object();
            /*newbackground.backgroundName = vm.backgroundName; */

			var model = new FormData();
			model.append('data', JSON.stringify(newbackground));
			model.append('file', backgroundImage);
			$http({
				method: 'POST',
				url: appCONSTANTS.API_URL + 'FeatureBackgrounds/',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('backgroundAddSuccess'),"success"); 
					 $uibModalInstance.dismiss('cancel');
					 callBackFunction();
				},
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
				}
            );
        }
        
        vm.LoadUploadImage = function() {
			$("#backgroundImage").click();
		}
		var backgroundImage; 
		$scope.AddbackgroundImage = function(element) {
			var imageFile = element[0];

			var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

			if (imageFile && imageFile.size >= 0 && ((imageFile.size / (1024 * 1000)) < 2)) {

				if (allowedImageTypes.indexOf(imageFile.type) !== -1) {
					$scope.newbackgroundForm.$dirty=true;
					$scope.$apply(function() {
						
						backgroundImage= imageFile;
						var reader = new FileReader();

						reader.onloadend = function() {
							vm.backgroundImage= reader.result;
							
							$scope.$apply();
						};
						if (imageFile) {
							reader.readAsDataURL(imageFile);
						}
					})
				} else {
					$("#logoImage").val('');
					ToastService.show("right","bottom","fadeInUp",$translate.instant('imageTypeError'),"error");
				}

			} else {
				if (imageFile) {
					$("#logoImage").val('');
					ToastService.show("right","bottom","fadeInUp",$translate.instant('imgaeSizeError'),"error");
				}

			}


		}
	}	
}());
