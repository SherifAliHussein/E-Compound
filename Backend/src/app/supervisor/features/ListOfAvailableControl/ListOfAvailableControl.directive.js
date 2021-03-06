angular.module('home').directive('listofavailableControl', function(){
    return {
        restrict: 'E',
        replace: true,
        scope: { featureControl: '=',selectedLanguage:'=',islistAvailable:"=",featureName:"=" },
        templateUrl: "./app/supervisor/features/ListOfAvailableControl/templates/ListOfAvailableControl.html",
        controllerAs:"listOfAvailableControler",
        controller:function($scope,$rootScope,controlEnum ,daysEnum,ManageFeatureResource,appCONSTANTS,$uibModal,ToastService,$translate,AvailableControlService,$filter){
            var vm = this;
            vm.featureControl = $scope.featureControl;
            vm.controls = controlEnum;
            vm.days = daysEnum;
            vm.language = appCONSTANTS.supportedLanguage;
            vm.currentPage = 1;
            vm.featureDetails = [];
            function loadDetails(){
                vm.isLoading = true;
                ManageFeatureResource.getFeatureDetails({featureControlId:vm.featureControl.featureControlId,page:vm.currentPage}).$promise.then(function (results) {
                    vm.featureDetails = results;
                    vm.isLoading = false;        
                    
                    vm.featureDetails.results.forEach(function(element) {
                        element.availables.forEach(function(item) {
                            item.from= item.from+"Z";
                            item.from = $filter('date')(new Date(item.from), "hh:mm a");
                            item.to= item.to+"Z";
                            item.to = $filter('date')(new Date(item.to), "hh:mm a");
                        }, this);    
                    }, this);        
                },
                function (data, status) {
                        
                });
            }
            vm.controlType = vm.featureControl.controlType == "Multiple"?true:false;
            vm.showMore = function(element)
            {
                $(element.currentTarget).toggleClass( "child-table-collapse" );
            }
            vm.switch = function(){
                ManageFeatureResource.switchFeatureControl({featureControlId:vm.featureControl.featureControlId});
            }
            loadDetails();
            $rootScope.$on('listofavailableChange', function (event) {
                loadDetails();
            });
            vm.changePage  = function (page) {
                vm.currentPage = page;
                loadDetails();
            }
            vm.addNew = function(){
                $scope.islistAvailable = true;
                AvailableControlService.setFeatureName($scope.featureName)
                AvailableControlService.setFeatureControlId(vm.featureControl.featureControlId)
                AvailableControlService.setFeatureDetail(null);
              
            }
            vm.openEditDialog=function(index){
                $scope.islistAvailable = true;
                AvailableControlService.setFeatureName($scope.featureName)
                AvailableControlService.setFeatureControlId(vm.featureControl.featureControlId)
                AvailableControlService.setFeatureDetail(angular.copy(vm.featureDetails.results[index]));
             
            }
            function confirmationDelete(itemId){
                ManageFeatureResource.deleteFeatureDetail({featureDetailId:itemId}).$promise.then(function(results) {
                    ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureUpdateSuccess'),"success");
                    if(vm.featureDetails.results.length ==1 && vm.currentPage > 1)
                        vm.currentPage = vm.currentPage -1;
                        loadDetails();
                },
                function(data, status) {
                    ToastService.show("right","bottom","fadeInUp",data.message,"error");
                });
            }
            vm.openDeleteDialog = function(name,id){			
                var modalContent = $uibModal.open({
                    templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
                    controller: 'confirmDeleteDialogController',
                    controllerAs: 'deleteDlCtrl',
                    resolve: {
                        itemName: function () { return name },
                        itemId: function() { return id },
                        message:function() { return null},
                        callBackFunction:function() { return confirmationDelete }
                    }
                    
                });
            }
                 
        }
        
    };
});