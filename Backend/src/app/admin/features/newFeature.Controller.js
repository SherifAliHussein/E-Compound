(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('newFeatureController', ['$scope','$state','appCONSTANTS', 'controlEnum','$http','$translate' , 'FeatureResource','ToastService',  newFeatureController])

	function newFeatureController($scope, $state , appCONSTANTS,controlEnum,$http, $translate , FeatureResource,ToastService){
		var vm = this;
        vm.language = appCONSTANTS.supportedLanguage;
        vm.controls= controlEnum;
		vm.close = function(){
            $state.go('features');            
		}
        vm.isChanged = false;
        vm.isFree=false;
        vm.editmode = false;
        vm.featureDetails = [] ;
        vm.featureDetailExist =false;
        vm.currentPage = 0;
        vm.SelectedControl = []
        vm.changePage = function(page){
            vm.currentPage = page-1;
        }
        vm.checkFeatureDetail = function(){
            var isFound = false;
            vm.featureDetails.forEach(function(detail) {
                if((detail.descriptionDictionary["en-us"] == vm.featureDetailDescDictionary["en-us"]) 
                || (detail.descriptionDictionary["ar-eg"] == vm.featureDetailDescDictionary["ar-eg"])){
                    
                    vm.featureDetailExist =true;
                    isFound = true;
                    return;
                }
            }, this);
            if(!isFound)
            vm.featureDetailExist =false;            
        }
        vm.AddFeatureDetail = function(){   
            if(vm.editmode){
                vm.featureDetails[vm.editIndex].descriptionDictionary=vm.featureDetailDescDictionary;
                vm.featureDetails[vm.editIndex].price = vm.isFree?0:vm.price;
                vm.featureDetails[vm.editIndex].isFree = vm.isFree;
            }
            else{
                
                vm.featureDetails.push({
                    descriptionDictionary:vm.featureDetailDescDictionary,
                    price:vm.isFree?0:vm.price,
                    isFree:vm.isFree
                })
            }
            vm.featureDetailDescDictionary=null;
            vm.price=null
            vm.isFree=false;
            vm.editmode = false;
            vm.featureDetailExist =false;
            
        }
        vm.edit = function(featureDetail,index){
            index = index + ((vm.currentPage) *10);
            vm.featureDetailDescDictionary=featureDetail.descriptionDictionary;
            vm.price=featureDetail.price;
            vm.isFree=featureDetail.isFree;
            vm.editmode = true;
            vm.editIndex = index;
        }
        vm.remove = function(index){
            index = index + ((vm.currentPage) *10);
            vm.featureDetails.splice(index,1);
        }
		vm.AddNewFeature = function(){
			vm.isChanged = true;
            var newFeature = new FeatureResource();
            newFeature.featureNameDictionary = vm.featureNameDictionary;
            var count = 1;
            newFeature.featureControl =[]
            vm.SelectedControl.forEach(function(element) {
                newFeature.featureControl.push({control:element.id,order:count})
                count++;
            }, this);
            // newFeature.featureControl = vm.SelectedControl;
            newFeature.type = "0";
            // if(vm.hasDetails){
            //     newFeature.featureDetails = vm.featureDetails
            //     // if(vm.editmode){
            //     //     vm.featureDetails[vm.editIndex].descriptionDictionary=vm.featureDetailDescDictionary;
            //     //     vm.featureDetails[vm.editIndex].price = vm.isFree?0:vm.price;
            //     //     vm.featureDetails[vm.editIndex].isFree = vm.isFree;
            //     // }
            //     // else{
                    
            //     //     vm.featureDetails.push({
            //     //         descriptionDictionary:vm.featureDetailDescDictionary,
            //     //         price:vm.isFree?0:vm.price,
            //     //         isFree:vm.isFree
            //     //     })
            //     // }
            // }

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

            // newFeature.$create().then(
            //     function(data, status) {
			// 		ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureAddSuccess'),"success");
			// 		 vm.isChanged = false;                     
            //          $state.go('features');                     
            //     },
            //     function(data, status) {
            //         vm.isChanged = false;                     
			// 		ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
            //     }
            // );
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
