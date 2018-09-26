(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('requestDetailDialogController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal', 'RequestResource'
        ,'feature','requestId','ToastService','callBackFunction','$uibModalInstance','language',  requestDetailDialogController])

    function requestDetailDialogController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal, RequestResource
        ,feature,requestId,ToastService,callBackFunction,$uibModalInstance,language){

        var vm = this;
        vm.feature = feature;
        vm.language = language;
        vm.close = function(){
			$uibModalInstance.dismiss('cancel');
        }
        vm.Approve = function(){
            var requestDetail = [];
            vm.feature.featureDetails.forEach(function(element) {
                if(element.isSelectedDetail){
                    requestDetail.push({featureDetailId:element.featureDetailId,number:element.number})
                }
            }, this);
            callBackFunction(requestId,requestDetail)
            $uibModalInstance.dismiss('cancel');
        }
	}
	
}());
