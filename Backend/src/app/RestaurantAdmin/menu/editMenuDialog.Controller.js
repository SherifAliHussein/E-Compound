(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editMenuDialogController', ['$scope','$http', '$state','appCONSTANTS','$translate', 'MenuResource','ToastService','menuPrepService',  editMenuDialogController])

	function editMenuDialogController($scope,$http, $state , appCONSTANTS, $translate, MenuResource,ToastService, menuPrepService){
		var vm = this;
		vm.menuName = "";
		vm.language = appCONSTANTS.supportedLanguage;
		vm.menu = menuPrepService;
		vm.close = function(){
			$state.go('Menu');
		}
		
		vm.updateMenu = function(){
			var updateMenu  = new Object();
            updateMenu.menuNameDictionary = vm.menu.menuNameDictionary;
			updateMenu.isImageChange = isImageChange;
			updateMenu.menuId = vm.menu.menuId;

			var model = new FormData();
			model.append('data', JSON.stringify(updateMenu));
			model.append('file', menuImage);
			$http({
				method: 'PUT',
				url: appCONSTANTS.API_URL + 'Menus/',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('menuUpdateSucess'),"success");
                    $state.go('Menu');
				},
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
				}
            );
		 
		}
		vm.LoadUploadImage = function() {
			$("#menuImage").click();
		}
        var menuImage; 
        var isImageChange = false;
		$scope.AddMenuImage = function(element) {
			var imageFile = element[0];

			var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

			if (imageFile && imageFile.size >= 0 && ((imageFile.size / (1024 * 1000)) < 2)) {

				if (allowedImageTypes.indexOf(imageFile.type) !== -1) {
					$scope.editMenuForm.$dirty=true;
					$scope.$apply(function() {
						
                        menuImage= imageFile;
                        isImageChange = true;
						var reader = new FileReader();

						reader.onloadend = function() {
							vm.menu.imageURL= reader.result;
							// $scope.Photo = reader.result;
							$scope.$apply();
						};
						if (imageFile) {
							reader.readAsDataURL(imageFile);
						}
					})
				} else {
					$("#menuImage").val('');
					ToastService.show("right","bottom","fadeInUp",$translate.instant('imageTypeError'),"error");
				}

			} else {
				if (imageFile) {
					$("#menuImage").val('');
					ToastService.show("right","bottom","fadeInUp",$translate.instant('imgaeSizeError'),"error");
				}

			}


		}
	}	
}());
