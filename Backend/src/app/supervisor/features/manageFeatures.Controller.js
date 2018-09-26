(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('manageFeaturesController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal', 'ManageFeatureResource'
        ,'featuresPrepService','$filter','ToastService', 'controlEnum',  manageFeaturesController])

    function manageFeaturesController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal, ManageFeatureResource
        ,featuresPrepService,$filter,ToastService , controlEnum){

        var vm = this;
        vm.features = featuresPrepService;
        vm.controls = controlEnum;
        vm.isAvailable = false;
        vm.islistAvailable = false;
	}
	
}());
