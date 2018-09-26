(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('categoryDialogController', ['$scope','$state','$stateParams','$http','$translate','appCONSTANTS' , 'CategoryResource','ToastService','$rootScope',  categoryDialogController])

	function categoryDialogController($scope, $state,$stateParams ,$http, $translate,appCONSTANTS , CategoryResource,ToastService,$rootScope){
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		vm.close = function(){
			$state.go('Category', {menuId: $stateParams.menuId});
		}
		vm.isChanged = false;
		vm.AddNewCategory = function(){
			vm.isChanged = true;
            var newCategroy = new Object();
            newCategroy.categoryNameDictionary = vm.categoryNameDictionary;
            newCategroy.menuId = $stateParams.menuId;

			var model = new FormData();
			model.append('data', JSON.stringify(newCategroy));
			model.append('file', categoryImage);
			$http({
				method: 'POST',
				url: appCONSTANTS.API_URL + 'Categories/',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('CategoryAddSuccess'),"success"); 
					 vm.isChanged = false;
					 $state.go('Category', {menuId: $stateParams.menuId});
				},
				function(data, status) {
					vm.isChanged = false;
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
				}
            );
        }
        
        vm.LoadUploadImage = function() {
			$("#categoryImage").click();
		}
		var categoryImage; 
		$scope.AddCategoryImage = function(element) {
			var imageFile = element[0];

			var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

			if (imageFile && imageFile.size >= 0 && ((imageFile.size / (1024 * 1000)) < 2)) {

				if (allowedImageTypes.indexOf(imageFile.type) !== -1) {
					$scope.newCategoryForm.$dirty=true;
					$scope.$apply(function() {
						
						categoryImage= imageFile;
						var reader = new FileReader();

						reader.onloadend = function() {
							vm.categoryImage= reader.result; 
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
