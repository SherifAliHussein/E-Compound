(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('menuDialogController', ['$scope','$http','$state','appCONSTANTS','$translate' , 'MenuResource','ToastService','$rootScope',  menuDialogController])

	function menuDialogController($scope,$http , $state , appCONSTANTS, $translate , MenuResource,ToastService,$rootScope){
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		vm.close = function(){
			$state.go('Menu');
		}
		vm.isChanged = false;
		
		vm.AddNewMenu = function(){
			vm.isChanged = true;
            var newMenu = new Object();
            newMenu.menuNameDictionary = vm.menuNameDictionary;

			var model = new FormData();
			model.append('data', JSON.stringify(newMenu));
			model.append('file', menuImage);
			$http({
				method: 'POST',
				url: appCONSTANTS.API_URL + 'Menus/',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('menuAddSuccess'),"success");
					 // $state.go('Category',{MenuId:menuId});
					 $state.go('Menu');
					 vm.isChanged = false;
				},
				function(data, status) {
					vm.isChanged = false;
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
				}
            ); 
		}

		vm.LoadUploadImage = function() {
			$("#menuImage").click();
		}
		var menuImage; 
		$scope.AddMenuImage = function(element) {
			var imageFile = element[0];

			var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

			if (imageFile && imageFile.size >= 0 && ((imageFile.size / (1024 * 1000)) < 2)) {

				if (allowedImageTypes.indexOf(imageFile.type) !== -1) {
					$scope.newMenuForm.$dirty=true;
					$scope.$apply(function() {
						
						menuImage= imageFile;
						var reader = new FileReader();

						reader.onloadend = function() {
							vm.menuImage= reader.result; 
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
