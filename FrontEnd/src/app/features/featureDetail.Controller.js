(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('featureDetailController', ['$scope','$state','$timeout', '$uibModal', '$stateParams','$translate', 'appCONSTANTS', 'RequestResource','featureDetailPrepService','featureBackgroundPrepService','$sce','requestDetailPrepService','$filter','lastRequestPrepService',  featureDetailController])

    function featureDetailController($scope, $state, $timeout, $uibModal, $stateParams ,$translate , appCONSTANTS, RequestResource,featureDetailPrepService,featureBackgroundPrepService,$sce,requestDetailPrepService, $filter,lastRequestPrepService){

        var vm = this;
        vm.feature = featureDetailPrepService;
        $scope.$parent.featureBackground = featureBackgroundPrepService;
        $scope.$parent.globalInfo= {};
        $scope.$parent.globalInfo.featureMode = true;
        $scope.$parent.globalInfo.featureMode = false;
        vm.lastRequest = lastRequestPrepService;
        if(vm.lastRequest.requestId !=undefined){
            vm.lastRequest.createTime = vm.lastRequest.createTime+"Z";
            vm.lastRequest.createTime = $filter('date')(new Date(vm.lastRequest.createTime), "dd/MM/yyyy hh:mm a");
            vm.lastRequest.modifyTime = vm.lastRequest.modifyTime+"Z";
            vm.lastRequest.modifyTime = $filter('date')(new Date(vm.lastRequest.modifyTime), "dd/MM/yyyy hh:mm a");
        }
        vm.isFrom = true;
        vm.isTo = false;
        // vm.picker3 = {
        //     date: new Date()
        // };
        // vm.openCalendar = function(e, picker) {
        //     vm.picker3.open = true;
        // };
        vm.availableChange = function(featureControl){
            vm.isFrom = !vm.isFrom;
            vm.isTo = !vm.isTo;
            if(vm.isTo == true){
                featureControl.to =angular.copy(featureControl.from);
                featureControl.to.setMinutes(featureControl.to.getMinutes()+30);
            }
            if(featureControl.to != null){
                var dayRequests = $filter('filter')(requestDetailPrepService,
                    function(value, index, array){
                        return new Date(value.from+"z").getDay() == featureControl.to.getDay() &&
                        new Date(value.from+"z").getDate() == featureControl.to.getDate() &&
                    //    ((new Date(value.from+"z").getHours() <= featureControl.to.getHours() && new Date(value.from+"z").getMinutes() <= featureControl.to.getMinutes()))
                    //    &&(new Date(value.to+"z").getHours() >= featureControl.to.getHours() && new Date(value.to+"z").getMinutes() >= featureControl.to.getMinutes())
                    new Date('1/1/2018 ' +new Date(value.from+"z").toLocaleTimeString()) <= new Date('1/1/2018 ' +new Date(time.toISOString()).toLocaleTimeString())
                    && new Date('1/1/2018 ' +new Date(value.to+"z").toLocaleTimeString()) >= new Date('1/1/2018 ' +new Date(time.toISOString()).toLocaleTimeString())
                       && value.featureDetailId == featureControl.selectedOption.featureDetailId
                    }
                );
                vm.availableNumberRequest = 0;
                dayRequests.forEach(function(element) {
                    vm.availableNumberRequest = vm.availableNumberRequest+ element.number
                }, this);
                if(featureControl.control == 4){ 
                    var check = $filter('filter')(featureControl.featureDetails[0].availables, {day:featureControl.to.getDay()});
                    vm.availableNumberRequest = check[0].max -vm.availableNumberRequest;
                    
                }
                else{
                    var check = $filter('filter')(featureControl.selectedOption.availables, {day:featureControl.to.getDay()});                    
                    vm.availableNumberRequest = check[0].max -vm.availableNumberRequest;
                }
            }
            
        }
        vm.config = {
            autoHideScrollbar: false,
            theme: '3d-dark',
            axis: 'y',
            setHeight: 545,
        }

        vm.checkAvailableFrom = function($view, $dates, $leftDate, $upDate, $rightDate,featureControl){
            vm.now = new Date();            
            if($view=="year"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getFullYear() < vm.now.getFullYear()){
                        element.selectable = false;
                    }
                }, this);
            }
            else if($view=="month"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getMonth() < vm.now.getMonth() && (new Date(element.localDateValue()).getFullYear() <= vm.now.getFullYear())){
                        element.selectable = false;
                    }
                }, this);
            
            }else if($view=="day"){
                $dates.forEach(function(date) {
                    if(featureControl.control == 4){                        
                        var availables = angular.copy(featureControl.featureDetails[0].availables);
                    }
                    else{
                        var availables = angular.copy(($filter('filter')(featureControl.featureDetails, 
                            {featureDetailId:featureControl.selectedOption.featureDetailId}))[0].availables);
                    }
                    var check = $filter('filter')(availables, {day:new Date(date.localDateValue()).getDay()});
                    if(check.length <= 0 || (new Date(new Date(date.localDateValue()).toDateString()) < new Date(vm.now.toDateString())))
                        date.selectable = false;
                }, this);
            }
            else if($view=="hour"){
                $dates.forEach(function(date) {
                    // var availables = angular.copy(featureControl.featureDetails[0].availables);
                    if(featureControl.control == 4){                        
                        var availables = angular.copy(featureControl.featureDetails[0].availables);
                    }
                    else{
                        var availables = angular.copy(($filter('filter')(featureControl.featureDetails, 
                            {featureDetailId:featureControl.selectedOption.featureDetailId}))[0].availables);
                    }
                    availables.forEach(function(element) {
                        element.from = new Date(element.from+"z").getHours();
                        element.to = new Date(element.to+"z").getHours();
                    }, this);
                    var time = new Date(date.localDateValue()).getHours();
                    var test = $filter('filter')(availables, {day:new Date(date.localDateValue()).getDay()});
                    var check = $filter('filter')(test,
                        function(value, index, array){
                            return value.from <= time && value.to>= time
                        }
                        );
                    if(check.length <= 0)
                        date.selectable = false;
                }, this);
            }
            else if($view=="minute"){
                $dates.forEach(function(date) {
                    // var availables = angular.copy(featureControl.featureDetails[0].availables);
                    if(featureControl.control == 4){                        
                        var availables = angular.copy(featureControl.featureDetails[0].availables);
                    }
                    else{
                        var availables = angular.copy(($filter('filter')(featureControl.featureDetails, 
                            {featureDetailId:featureControl.selectedOption.featureDetailId}))[0].availables);
                    }
                    availables.forEach(function(element) {
                        element.from = new Date(element.from+"z");
                        element.to = new Date(element.to+"z");
                    }, this);
                    var time = new Date(date.localDateValue());
                    var test = $filter('filter')(availables, {day:new Date(date.localDateValue()).getDay()});
                    var dayRequests = $filter('filter')(requestDetailPrepService,
                        function(value, index, array){
                            return new Date(value.from+"z").getDay() == new Date(date.localDateValue()).getDay() &&
                            new Date(value.from+"z").getDate() == new Date(date.localDateValue()).getDate() &&
                           (
                               new Date('1/1/2018 ' +new Date(value.from+"z").toLocaleTimeString()) <= new Date('1/1/2018 ' +new Date(time.toISOString()).toLocaleTimeString())
                                && new Date('1/1/2018 ' +new Date(value.to+"z").toLocaleTimeString()) >= new Date('1/1/2018 ' +new Date(time.toISOString()).toLocaleTimeString())
                    //         (new Date(value.from+"z").getHours() == time.getHours() && new Date(value.from+"z").getMinutes() <= time.getMinutes())||
                    //            ((new Date(value.from+"z").getHours() > time.getHours() && new Date(value.from+"z").getMinutes() >= time.getMinutes())
                    //     //    &&((new Date(value.to+"z").getHours() > time.getHours() && new Date(value.to+"z").getMinutes() > time.getMinutes())))
                    //     ||(((new Date(value.to+"z").getHours() == time.getHours() && new Date(value.to+"z").getMinutes() > time.getMinutes()))
                        
                    // ))
                )&& value.featureDetailId == featureControl.selectedOption.featureDetailId
                       
                       
                           //    ||(new Date(value.to+"z").getHours() == time.getHours() && new Date(value.to+"z").getMinutes() >= time.getMinutes()))
                            
                            
                            // (new Date(value.from+"z").getHours() <= time.getHours() && (new Date(value.to+"z").getHours() > time.getHours()))  
                            // && new Date(value.from+"z").getMinutes() <= time.getMinutes()
                        }
                    );
                    var requestCount = 0;
                    dayRequests.forEach(function(element) {
                        requestCount = requestCount+ element.number
                    }, this);
                    var check = $filter('filter')(test,
                        function(value, index, array){
                            return(((value.from.getHours() == time.getHours() && value.from.getMinutes() <= time.getMinutes())
                                || (value.from.getHours() != time.getHours() || value.from.getMinutes() <= time.getMinutes()))
                                && value.max > requestCount)
                                && (((value.to.getHours() == time.getHours() && value.to.getMinutes() > time.getMinutes()))
                                    || ((value.to.getHours() != time.getHours() || value.to.getMinutes() > time.getMinutes())))
                        }
                        );
                    if(check.length <= 0)
                        date.selectable = false;
                }, this);
            }
        }
        vm.checkAvailableTo = function($view, $dates, $leftDate, $upDate, $rightDate,featureControl){
            vm.now = new Date();            
            if($view=="year"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getFullYear() < featureControl.from.getFullYear()){
                        element.selectable = false;
                    }
                }, this);
            }
            else if($view=="month"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getMonth() < featureControl.from.getMonth() && (new Date(element.localDateValue()).getFullYear() <= featureControl.from.getFullYear())){
                        element.selectable = false;
                    }
                }, this);
            
            }else if($view=="day"){
                $dates.forEach(function(date) {
                    // var availables = featureControl.featureDetails[0].availables;
                    if(featureControl.control == 4){                        
                        var availables = angular.copy(featureControl.featureDetails[0].availables);
                    }
                    else{
                        var availables = angular.copy(($filter('filter')(featureControl.featureDetails, 
                            {featureDetailId:featureControl.selectedOption.featureDetailId}))[0].availables);
                    }
                    var check = $filter('filter')(availables, {day:new Date(date.localDateValue()).getDay()});
                    if(check.length <= 0 || (new Date(new Date(date.localDateValue()).toDateString())).getTime() != (new Date(featureControl.from.toDateString())).getTime())
                        date.selectable = false;
                }, this);
            }            
            else if($view=="hour"){
                $dates.forEach(function(date) {
                    // var availables = angular.copy(featureControl.featureDetails[0].availables);
                    if(featureControl.control == 4){                        
                        var availables = angular.copy(featureControl.featureDetails[0].availables);
                    }
                    else{
                        var availables = angular.copy(($filter('filter')(featureControl.featureDetails, 
                            {featureDetailId:featureControl.selectedOption.featureDetailId}))[0].availables);
                    }
                    availables.forEach(function(element) {                     
                        element.to = new Date(element.to+"z").getHours();
                    }, this);
                    var time = new Date(date.localDateValue()).getHours();
                    var test = $filter('filter')(availables, {day:new Date(date.localDateValue()).getDay()});
                    var check = $filter('filter')(test,
                        function(value, index, array){
                            return featureControl.from.getHours() <= time && value.to>= time
                        }
                        );
                    if(check.length <= 0 || (new Date(new Date(date.localDateValue()).toDateString())).getTime() != (new Date(featureControl.from.toDateString())).getTime())
                        date.selectable = false;
                }, this);
            }
            else if($view=="minute"){
                $dates.forEach(function(date) {
                    // var availables = angular.copy(featureControl.featureDetails[0].availables);
                    if(featureControl.control == 4){                        
                        var availables = angular.copy(featureControl.featureDetails[0].availables);
                    }
                    else{
                        var availables = angular.copy(($filter('filter')(featureControl.featureDetails, 
                            {featureDetailId:featureControl.selectedOption.featureDetailId}))[0].availables);
                    }
                    availables.forEach(function(element) {
                        element.to = new Date(element.to+"z");
                    }, this);
                    var time = new Date(date.localDateValue());
                    var test = $filter('filter')(availables, {day:new Date(date.localDateValue()).getDay()});
                    var dayRequests = $filter('filter')(requestDetailPrepService,
                        function(value, index, array){
                            return new Date(value.from+"z").getDay() == new Date(date.localDateValue()).getDay() &&
                            new Date(value.from+"z").getDate() == new Date(date.localDateValue()).getDate() &&
                        //    ((new Date(value.from+"z").getHours() <= time.getHours() && new Date(value.from+"z").getMinutes() <= time.getMinutes()))
                         //  (new Date(value.to+"z").getHours() >= time.getHours() && new Date(value.to+"z").getMinutes() >= time.getMinutes())
                         (
                             (new Date('1/1/2018 ' +new Date(value.from+"z").toLocaleTimeString()) < new Date('1/1/2018 ' +new Date(time.toISOString()).toLocaleTimeString())
                            && new Date('1/1/2018 ' +new Date(value.to+"z").toLocaleTimeString()) >= new Date('1/1/2018 ' +new Date(time.toISOString()).toLocaleTimeString()))
                         ||
                         (new Date('1/1/2018 ' +new Date(value.from+"z").toLocaleTimeString()) >= new Date('1/1/2018 ' +featureControl.from.toLocaleTimeString())
                         && new Date('1/1/2018 ' +new Date(value.to+"z").toLocaleTimeString()) <= new Date('1/1/2018 ' +new Date(time.toISOString()).toLocaleTimeString())))
                        && value.featureDetailId == featureControl.selectedOption.featureDetailId
                       
                           //    ||(new Date(value.to+"z").getHours() == time.getHours() && new Date(value.to+"z").getMinutes() >= time.getMinutes()))
                            
                            
                            // (new Date(value.from+"z").getHours() <= time.getHours() && (new Date(value.to+"z").getHours() > time.getHours()))  
                            // && new Date(value.from+"z").getMinutes() <= time.getMinutes()
                        }
                    );
                    var requestCount = 0;
                    dayRequests.forEach(function(element) {
                        requestCount = requestCount+ element.number
                    }, this);
                    var check = $filter('filter')(test,
                        function(value, index, array){
                            // return (value.to.getHours() == time.getHours() && featureControl.from.getMinutes() <= time.getMinutes() && value.to.getMinutes() >= time.getMinutes())
                            // || (value.to.getHours() != time.getHours() && featureControl.from.getMinutes() <= time.getMinutes() )
                            
                            return  new Date('1/1/2018 '+value.to.toLocaleTimeString()) >=  new Date('1/1/2018 '+time.toLocaleTimeString())  &&
                              new Date('1/1/2018 '+time.toLocaleTimeString()) > new Date('1/1/2018 '+featureControl.from.toLocaleTimeString())
                              && value.max > requestCount
                            // return value.to.getMinutes() > time.getMinutes()

                            // return (value.to.getHours() == time.getHours() && value.to.getMinutes() <= time.getMinutes())
                            // || (value.to.getHours() != time.getHours() || value.to.getMinutes() <= time.getMinutes())
                        }
                        );
                    if(check.length <= 0 || (new Date(new Date(date.localDateValue()).toDateString())).getTime() != (new Date(featureControl.from.toDateString())).getTime())
                        date.selectable = false;
                }, this);
            }
        }
        vm.beforeRender = function ($view, $dates, $leftDate, $upDate, $rightDate) {
            vm.now = new Date();
            if($view=="year"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getFullYear() < vm.now.getFullYear()){
                        element.selectable = false;
                    }
                }, this);
            }
            else if($view=="month"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getMonth() < vm.now.getMonth() && (new Date(element.localDateValue()).getFullYear() <= vm.now.getFullYear())){
                        element.selectable = false;
                    }
                }, this);
            
            }else if($view=="day"){
                $dates.forEach(function(element) {
                    if(new Date(new Date(element.localDateValue()).toDateString()) < new Date(vm.now.toDateString())){
                        element.selectable = false;
                    }
                }, this);
            }
            else if($view=="hour"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getHours() < vm.now.getHours() && (new Date(new Date(element.localDateValue()).toDateString()) <= new Date(vm.now.toDateString()))){
                        element.selectable = false;
                    }
                }, this);
            }                
            else if($view=="minute"){
                $dates.forEach(function(element) {
                    if(new Date(element.localDateValue()).getMinutes() < vm.now.getMinutes() && (new Date(element.localDateValue()).getHours() <= vm.now.getHours() && (new Date(new Date(element.localDateValue()).toDateString()) <= new Date(vm.now.toDateString())))){
                        element.selectable = false;
                    }
                }, this);
            }
            // var index = Math.floor(Math.random() * $dates.length);
            // $dates[index].selectable = false;
        }
        vm.listOfAvailabiltyChange = function(featureControl){
            featureControl.from = new Date();    
            $timeout(function(){                    
                featureControl.from = null;
                featureControl.to = null;
            }, 100);
            vm.isFrom = true;
            vm.isTo = false;
        }
		vm.feature.featureControl.forEach(function(featureControl) {
            if(featureControl.control == 3){
                featureControl.featureDetails.forEach(function(element) {
                    element.link= $sce.trustAsResourceUrl(element.link.replace('watch?v=','embed/'))
                }, this);
            }
        }, this);
        vm.confirmRequest = function  (){
            vm.newRequest.comment = vm.comment != null?vm.comment.trimLeft():vm.comment;
            vm.newRequest.$create().then(
                function(data, status) {
                    $state.go('Features');
					//ToastService.show("right","bottom","fadeInUp",$translate.instant('RequestSuccess'),"success");
                },
                function(data, status) {
                    //ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
        }
        
		vm.request = function (){
            vm.newRequest = new RequestResource();
            vm.newRequest.featureId = $stateParams.featureId;

            vm.newRequest.requestDetails = []
            vm.feature.featureControl.forEach(function(featureControl) {
                if(featureControl.control == 0){
                    if(featureControl.controlType == 'Multiple'){
                        featureControl.selectedOption.forEach(function(element) {
                            // if(element.isSelected ){
                                vm.newRequest.requestDetails.push({featureDetailId:element.featureDetailId,number:1,price:element.price})
                            // }
                            
                        }, this);
                    }
                    else if(featureControl.controlType == 'Single' && featureControl.selectedOption != null){
                        vm.newRequest.requestDetails.push({featureDetailId:featureControl.selectedOption.featureDetailId,number:1,price:featureControl.selectedOption.price})
                    }                    
                }
                if(featureControl.control == 2){
                    if(featureControl.controlType == 'Multiple'){
                        featureControl.featureDetails.forEach(function(element) {
                            if(element.isSelected ){
                                vm.newRequest.requestDetails.push({featureDetailId:element.featureDetailId,number:1,price:element.price})
                            }
                            
                        }, this);
                    }
                    else if(featureControl.controlType == 'Single' && featureControl.selectedOption != null){
                        vm.newRequest.requestDetails.push({featureDetailId:featureControl.selectedOption.featureDetailId,number:1,price:featureControl.selectedOption.price})
                    }                    
                }
                if(featureControl.control == 5){
                    vm.newRequest.requestTime = featureControl.date;
                }
                if(featureControl.control == 4){
                    vm.newRequest.requestDetails.push({featureDetailId:featureControl.featureDetails[0].featureDetailId,number:1,price:featureControl.featureDetails[0].price*((featureControl.to.getTime() - featureControl.from.getTime())/(1000*60*60))
                        ,from:featureControl.from,to:featureControl.to})
                }
                
                if(featureControl.control == 6){
                    vm.newRequest.requestDetails.push({featureDetailId:featureControl.selectedOption.featureDetailId,number:1,price:featureControl.selectedOption.price*((featureControl.to.getTime() - featureControl.from.getTime())/(1000*60*60))
                        ,from:featureControl.from,to:featureControl.to})
                }
            }, this);
        }
        vm.removeFeatureDetail = function(index){
            vm.newRequest.requestDetails.splice(index,1);
        }
        vm.filterFeatureLeftSide = function(feature){
            return feature.control == 0 ||feature.control == 2||feature.control == 4||feature.control == 5||feature.control == 6;
        }
        vm.filterFeatureRightSide = function(feature){
            return feature.control == 1 ||feature.control == 3;
        }
	}
	
}());
