angular.module('home').directive('imageControl', function(){
    return {
        restrict: 'E',
        replace: true,
        scope: { featureControl: '=',selectedLanguage:'=' },
        templateUrl: "./app/supervisor/features/ImageControl/templates/ImageControl.html",
        controllerAs:"imageControler",
        controller:function($scope,controlEnum,ManageFeatureResource,appCONSTANTS,$uibModal,ToastService,$translate){
            var vm = this;
            vm.featureControl = $scope.featureControl;
            vm.controls = controlEnum;
            vm.Now = (new Date()).getTime();
            vm.language = appCONSTANTS.supportedLanguage;
            vm.currentPage = 1;
            vm.featureDetails = [];
            function loadDetails(){
                vm.isLoading = true;
                ManageFeatureResource.getFeatureDetails({featureControlId:vm.featureControl.featureControlId,page:vm.currentPage}).$promise.then(function (results) {
                    vm.featureDetails = results;
                    vm.isLoading = false;                
                },
                function (data, status) {
                        
                });
            }
            loadDetails();
            vm.changePage  = function (page) {
                vm.currentPage = page;
                loadDetails();
            }
            vm.addNew = function(){
                var modalContent = $uibModal.open({
                    templateUrl: "./app/supervisor/features/ImageControl/templates/ImageControlPopup.html",
                    controller: 'ImageControlDialogController',
                    controllerAs: 'imageControlDlCtrl',
                    resolve:{
                        callBackFunction:function(){return loadDetails;},
                        featureDetail:function(){return null;},
                        language:function(){return vm.language;},
                        featureControlId:function(){return vm.featureControl.featureControlId;}
                    }
                    
                });
            }
            vm.openEditDialog=function(index){
                var modalContent = $uibModal.open({
                    templateUrl: "./app/supervisor/features/ImageControl/templates/ImageControlPopup.html",
                    controller: 'ImageControlDialogController',
                    controllerAs: 'imageControlDlCtrl',
                    resolve:{
                        callBackFunction:function(){return loadDetails;},
                        featureDetail:function(){return angular.copy(vm.featureDetails.results[index]);},
                        language:function(){return vm.language;},
                        featureControlId:function(){return vm.featureControl.featureControlId;}
                    }
                    
                });
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