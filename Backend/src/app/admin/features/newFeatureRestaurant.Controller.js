(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('newFeatureRestaurantController', ['$scope','$state','appCONSTANTS','$http','$translate' , 'FeatureResource','ToastService','restaurantsNamePrepService',  newFeatureRestaurantController])

	function newFeatureRestaurantController($scope, $state , appCONSTANTS,$http, $translate , FeatureResource,ToastService,restaurantsNamePrepService){
		var vm = this;
        vm.language = appCONSTANTS.supportedLanguage;
        
		vm.close = function(){
            $state.go('features');            
		}
        vm.isChanged = false;
        vm.isFree=false;
        vm.restaurants = restaurantsNamePrepService;
        vm.currentPage = 0;
        vm.SelectedRestaurant = [];
        vm.changePage = function(page){
            vm.currentPage = page-1;
        }
        $scope.$watch('selectedLanguage',function(){
            $(".select-tags").select2({
                tags: false,
                theme: "bootstrap",
            })
        })
		vm.AddNewFeature = function(){
			vm.isChanged = true;
            var newFeature = new FeatureResource();
            newFeature.featureNameDictionary = vm.featureNameDictionary;
            newFeature.hasDetails = true;
            newFeature.type = "1";
            newFeature.restaurants = vm.SelectedRestaurant;
            var model = new FormData();
			model.append('data', JSON.stringify(newFeature));
			model.append('file', featureImage);
			$http({
				method: 'POST',
				url: appCONSTANTS.API_URL + 'Features/',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureAddSuccess'),"success");
					 vm.isChanged = false;                     
                     $state.go('features');                     
                },
                function(data, status) {
                    vm.isChanged = false;                     
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );

        }
        vm.LoadUploadLogo = function () {
            $("#logoImage").click();
        }
        var featureImage;
        $scope.AddFeatureImage = function (element) {
            var logoFile = element[0];

            var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

            if (logoFile && logoFile.size >= 0 && ((logoFile.size / (1024 * 1000)) < 2)) {

                if (allowedImageTypes.indexOf(logoFile.type) !== -1) {
                    $scope.newFeatureForm.$dirty = true;
                    $scope.$apply(function () {

                        featureImage = logoFile;
                        var reader = new FileReader();

                        reader.onloadend = function () {
                            vm.featureImage = reader.result;
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
        
	}	
}());
