(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editFeatureTicketController', ['$scope','$state','$http','$translate','appCONSTANTS', 'FeatureResource','ToastService','featurePrepService','TicketsNamePrepService',  editFeatureTicketController])

	function editFeatureTicketController($scope, $state ,$http, $translate,appCONSTANTS, FeatureResource,ToastService, featurePrepService,TicketsNamePrepService,callBackFunction){
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		
		vm.feature = featurePrepService;
        vm.Tickets = TicketsNamePrepService;
        vm.SelectedTicketId=[];
        vm.SelectedTicket = [];
        featurePrepService.Tickets.forEach(function(element) {
			var kk = vm.Tickets.filter(function(item){
				return (item.TicketId ===  element.TicketId);
              })[0];
              
			vm.SelectedTicketId.push(element.TicketId)
			vm.SelectedTicket.push(element)
        }, this);
        vm.TicketChange = function(){
			vm.SelectedTicket = []
			for(var i=0;i<vm.SelectedTicketId.length;i++){
				var Ticket = vm.Tickets.filter(function(item){
					return (item.TicketId ===  vm.SelectedTicketId[i]);
				})[0]
				vm.SelectedTicket.push(Ticket)  
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
            updateFeature.Tickets = [];
			vm.SelectedTicket.forEach(function(element) {
                updateFeature.Tickets.push(element);
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
