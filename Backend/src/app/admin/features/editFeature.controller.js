(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editFeatureController', ['$scope','$state','$http','$translate','appCONSTANTS', 'controlEnum', 'FeatureResource','ToastService','featurePrepService',  editFeatureController])

	function editFeatureController($scope, $state ,$http, $translate,appCONSTANTS, controlEnum, FeatureResource,ToastService, featurePrepService){
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		
        vm.feature = featurePrepService;
        vm.controls= controlEnum;
        vm.SelectedControlId=[];
        vm.SelectedControl = [];
        vm.feature.featureControl.forEach(function(element) {
			var kk = vm.controls.filter(function(item){
				return (item.id ===  element.control);
              })[0];
              
			vm.SelectedControlId.push(element.control)
			vm.SelectedControl.push(element.control)
        }, this);
		vm.moreDetail = false;
		vm.editmode = false;		
		vm.enableMoreDetail = function(){
			vm.moreDetail = true;
		}
		vm.close = function(){
			$state.go('features');
		}

		vm.changeFeatureDetail = function(){
			if(vm.feature.hasDetails &&  vm.feature.featureDetails.length <=0){
				vm.moreDetail = true;
				
			}
		}
		vm.featureDetailExist =false;
        vm.currentPage = 0;
        vm.changePage = function(page){
            vm.currentPage = page-1;
        }
        vm.checkFeatureDetail = function(){
            var isFound = false;
            vm.feature.featureDetails.forEach(function(detail) {
                if(((detail.descriptionDictionary["en-us"] == vm.featureDetailDescDictionary["en-us"]) 
                || (detail.descriptionDictionary["ar-eg"] == vm.featureDetailDescDictionary["ar-eg"]))&& detail.featureDetailId !=vm.featureDetailId ){
                    
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
                vm.feature.featureDetails[vm.editIndex].descriptionDictionary=vm.featureDetailDescDictionary;
                vm.feature.featureDetails[vm.editIndex].price = vm.isFree?0:vm.price;
                vm.feature.featureDetails[vm.editIndex].isFree = vm.isFree;
            }
            else{
                
                vm.feature.featureDetails.push({
                    descriptionDictionary:vm.featureDetailDescDictionary,
                    price:vm.isFree?0:vm.price,
					isFree:vm.isFree,
					isDeleted:false
                })
            }
            vm.featureDetailDescDictionary=null;
            vm.price=null
            vm.isFree=false;
            vm.editmode = false;
			vm.featureDetailExist =false;
			
        }
        vm.edit = function(featureDetail){
			// index = index + ((vm.currentPage) *10);
            vm.featureDetailDescDictionary=featureDetail.descriptionDictionary;
            vm.price=featureDetail.price;
            vm.isFree=featureDetail.isFree;
            vm.editmode = true;
            vm.editIndex = vm.feature.featureDetails.indexOf(featureDetail);
			vm.moreDetail = true;
			vm.featureDetailId = featureDetail.featureDetailId
		}
        vm.remove = function(featureDetail){
			// index = index + ((vm.currentPage) *10);
			featureDetail.isDeleted = true;
        }
        
        vm.controlChange = function(){
			vm.SelectedControl = []
			for(var i=0;i<vm.SelectedControlId.length;i++){
				var control = vm.controls.filter(function(item){
					return (item.id ===  vm.SelectedControlId[i]);
				})[0]
				vm.SelectedControl.push(control.id)  
			}
		}
		vm.updateFeature = function(){
            var updateFeature = new FeatureResource();
            updateFeature.featureNameDictionary = vm.feature.featureNameDictionary;
           // updateFeature.hasDetails = vm.feature.hasDetails;
           var count = 1;
           updateFeature.featureControl =[]
           vm.SelectedControl.forEach(function(element) {
            updateFeature.featureControl.push({control:element,order:count})
               count++;
           }, this);
			updateFeature.featureId = vm.feature.featureId;
			updateFeature.isImageChange = isImageChange;
			updateFeature.type = "0";
			// if(vm.feature.hasDetails){
            //     updateFeature.featureDetails = vm.feature.featureDetails
            //     // if(vm.editmode){
            //     //     updateFeature.featureDetails[vm.editIndex].descriptionDictionary=vm.featureDetailDescDictionary;
            //     //     updateFeature.featureDetails[vm.editIndex].price = vm.isFree?0:vm.price;
            //     //     updateFeature.featureDetails[vm.editIndex].isFree = vm.isFree;
            //     // }
            //     // else{
            //     //     if(vm.featureDetailDescDictionary !=null && (vm.price >0 || vm.isFree))
            //     //     updateFeature.featureDetails.push({
            //     //         descriptionDictionary:vm.featureDetailDescDictionary,
            //     //         price:vm.isFree?0:vm.price,
            //     //         isFree:vm.isFree
            //     //     })
            //     // }
			// }
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
			// updateFeature.$update().then(
            //     function(data, status) {
			// 		ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureUpdateSuccess'),"success");
			// 		 vm.isChanged = false;                     
			// 		 $state.go('features');
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
