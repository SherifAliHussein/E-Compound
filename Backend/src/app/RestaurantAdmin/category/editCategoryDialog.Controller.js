(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editCategoryDialogController', ['$scope','$state','$stateParams','$http','$translate','appCONSTANTS','ToastService','categoryPrepService',  editCategoryDialogController])

	function editCategoryDialogController($scope, $state , $stateParams,$http, $translate,appCONSTANTS,ToastService,  categoryPrepService){
		var vm = this;
		
		vm.language = appCONSTANTS.supportedLanguage;
		vm.category = categoryPrepService;
		vm.close = function(){
			$state.go('Category', {menuId: $stateParams.menuId});
		}
		
		vm.updateCategory = function(){
            var updateCategory = new Object();
            updateCategory.categoryNameDictionary = vm.category.categoryNameDictionary;
			updateCategory.isImageChange = isImageChange;
			updateCategory.categoryId = vm.category.categoryId;
			updateCategory.menuId = vm.category.menuId;
				

			var model = new FormData();
			model.append('data', JSON.stringify(updateCategory));
			model.append('file', categoryImage);
			$http({
				method: 'PUT',
				url: appCONSTANTS.API_URL + 'Categories/',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('CategoryupdateSuccess'),"success");
                    // $state.go('Category',{MenuId:menuId});
                    $state.go('Category', {menuId: $stateParams.menuId});
				},
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
				}
            );
            
        }
        vm.LoadUploadImage = function() {
			$("#categoryImage").click();
		}
        var categoryImage; 
        var isImageChange = false;
		$scope.AddCategoryImage = function(element) {
			var imageFile = element[0];

			var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

			if (imageFile && imageFile.size >= 0 && ((imageFile.size / (1024 * 1000)) < 2)) {

				if (allowedImageTypes.indexOf(imageFile.type) !== -1) {
					$scope.editCategoryForm.$dirty=true;
					$scope.$apply(function() {
						
                        categoryImage= imageFile;
                        isImageChange = true;
						var reader = new FileReader();

						reader.onloadend = function() {
							vm.category.imageURL= reader.result;
							// $scope.Photo = reader.result;
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
})();
