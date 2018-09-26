(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editFeatureRestaurantController', ['$scope','$state','$http','$translate','appCONSTANTS', 'FeatureResource','ToastService','featurePrepService','restaurantsNamePrepService',  editFeatureRestaurantController])

	function editFeatureRestaurantController($scope, $state ,$http, $translate,appCONSTANTS, FeatureResource,ToastService, featurePrepService,restaurantsNamePrepService,callBackFunction){
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		
		vm.feature = featurePrepService;
        vm.restaurants = restaurantsNamePrepService;
        vm.SelectedRestaurantId=[];
        vm.SelectedRestaurant = [];
        featurePrepService.restaurants.forEach(function(element) {
			var kk = vm.restaurants.filter(function(item){
				return (item.restaurantId ===  element.restaurantId);
              })[0];
              
			vm.SelectedRestaurantId.push(element.restaurantId)
			vm.SelectedRestaurant.push(element)
        }, this);
        vm.restaurantChange = function(){
			vm.SelectedRestaurant = []
			for(var i=0;i<vm.SelectedRestaurantId.length;i++){
				var restaurant = vm.restaurants.filter(function(item){
					return (item.restaurantId ===  vm.SelectedRestaurantId[i]);
				})[0]
				vm.SelectedRestaurant.push(restaurant)  
			}
		}
		vm.close = function(){
			$state.go('features');
		}

        vm.currentPage = 0;
        vm.changePage = function(page){
            vm.currentPage = page-1;
        }
        
        $scope.$watch('selectedLanguage',function(){
            $(".select-tags").select2({
                tags: false,
                theme: "bootstrap",
            })
        })
		vm.updateFeature = function(){
            var updateFeature = new FeatureResource();
            updateFeature.featureNameDictionary = vm.feature.featureNameDictionary;
            updateFeature.hasDetails = vm.feature.hasDetails;
			updateFeature.featureId = vm.feature.featureId;
			updateFeature.isImageChange = isImageChange;
            updateFeature.type = "1";
            // updateFeature.restaurants = vm.feature.restaurants;
            updateFeature.restaurants = [];
			vm.SelectedRestaurant.forEach(function(element) {
                updateFeature.restaurants.push(element);
			}, this);
			
			var model = new FormData();
			model.append('data', JSON.stringify(updateFeature));
			model.append('file', featureImage);
			$http({
				method: 'put',
				url: appCONSTANTS.API_URL + 'Features/',
				useToken: true,
				headers: { 'Content-Type': undefined },
				data: model
			}).then(
				function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureUpdateSuccess'),"success");
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
		var isImageChange = false;
        $scope.AddFeatureImage = function (element) {
            var logoFile = element[0];

            var allowedImageTypes = ['image/jpg', 'image/png', 'image/jpeg']

            if (logoFile && logoFile.size >= 0 && ((logoFile.size / (1024 * 1000)) < 2)) {

                if (allowedImageTypes.indexOf(logoFile.type) !== -1) {
                    $scope.editFeatureForm.$dirty = true;
                    $scope.$apply(function () {

						featureImage = logoFile;
						isImageChange = true;
                        var reader = new FileReader();

                        reader.onloadend = function () {
							vm.feature.imageURL = reader.result;
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
})();
