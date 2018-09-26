(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('supervisorReportController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal', 'RequestResource'
        ,'requestsPrepService','$filter','ToastService','authorizationService','FeatureResource','roomsNamePrepService','featuresNamePrepService','$timeout',  supervisorReportController])

    function supervisorReportController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal, RequestResource
        ,requestsPrepService,$filter,ToastService,authorizationService,FeatureResource,roomsNamePrepService,featuresNamePrepService,$timeout){

        var vm = this;
        vm.requests = requestsPrepService;
        vm.rooms = [{roomId:0,roomName:"All rooms"}];
        vm.selectedRoom = vm.rooms[0];
        vm.rooms = vm.rooms.concat(roomsNamePrepService);

        
        vm.features = [{featureId:0,featureNameDictionary:{'en-us':"All features",'ar-eg':"كل الميزات"}}];
        vm.selectedFeature = vm.features[0];
        vm.features = vm.features.concat(featuresNamePrepService);

        _.forEach(vm.requests.results, function (request) {
            request.createTime= request.createTime+"Z";
           request.createTime = $filter('date')(new Date(request.createTime), "dd/MM/yyyy hh:mm a");
           request.modifyTime= request.modifyTime+"Z";
          request.modifyTime = $filter('date')(new Date(request.modifyTime), "dd/MM/yyyy hh:mm a");
          if(request.requestTime !=null){                      
            request.requestTime= request.requestTime+"Z";
            request.requestTime = $filter('date')(new Date(request.requestTime), "dd/MM/yyyy hh:mm a");
          }
          request.requestDetails.forEach(function(element) {
            if(element.from !=null){                      
                element.from= element.from+"Z";
                element.from = $filter('date')(new Date(element.from), "dd/MM/yyyy hh:mm a");
              }
              if(element.to !=null){                      
                element.to= element.to+"Z";
                element.to = $filter('date')(new Date(element.to), "dd/MM/yyyy hh:mm a");
              }
          }, this);
        });
        $('.pmd-sidebar-nav>li>a').removeClass("active")
        var user = authorizationService.getUser();
        
        $($('.pmd-sidebar-nav').children()[2].children[0]).addClass("active")
		
		function refreshRequests(){
            vm.isLoading = true;
            var from ="";
            if($('#datepicker-start').val() != "") 
                from  = (new Date($('#datepicker-start').val())).toISOString().replace('Z','');
                
            var to ="";
            if($('#datepicker-end').val() != "") 
                to  = (new Date($('#datepicker-end').val())).toISOString().replace('Z','');
            var k = RequestResource.getAllRequest({ page:vm.currentPage,roomId: vm.selectedRoom.roomId
                ,featureId:vm.selectedFeature.featureId,from:from , to:to }).$promise.then(function(results) {
				
                vm.requests = results;
                _.forEach(vm.requests.results, function (request) {
                    request.createTime= request.createTime+"Z";
                   request.createTime = $filter('date')(new Date(request.createTime), "dd/MM/yyyy hh:mm a");
                   request.modifyTime= request.modifyTime+"Z";
                  request.modifyTime = $filter('date')(new Date(request.modifyTime), "dd/MM/yyyy hh:mm a");
                  if(request.requestTime !=null){                      
                    request.requestTime= request.requestTime+"Z";
                    request.requestTime = $filter('date')(new Date(request.requestTime), "dd/MM/yyyy hh:mm a");
                  }
                  request.requestDetails.forEach(function(element) {
                    if(element.from !=null){                      
                        element.from= element.from+"Z";
                        element.from = $filter('date')(new Date(element.from), "dd/MM/yyyy hh:mm a");
                      }
                      if(element.to !=null){                      
                        element.to= element.to+"Z";
                        element.to = $filter('date')(new Date(element.to), "dd/MM/yyyy hh:mm a");
                      }
                  }, this);
                  
                });
                vm.isLoading = false;
			},
            function(data, status) {
                ToastService.show("right","bottom","fadeInUp",data.message,"error");
                vm.isLoading = false;
            });
        }
        
		vm.currentPage  = 1;
        vm.changePage  = function (page) {
            vm.currentPage = page;
            refreshRequests();
           
		}	
        vm.showMore = function(element)
        {
            $(element.currentTarget).toggleClass( "child-table-collapse" );
        }
        
        vm.fileName= $filter('date')(new Date(), "dd/MM/yyyy hh:mm a")+".csv";
        vm.reportData =[];
        vm.charEncode;
        vm.canDownload = false;
        vm.downloadFile = function(){
            vm.fileColumnHeaders=[
                "",
                "",
                "",
                "",
                "",
                vm.selectedRoom.roomName,
                vm.selectedFeature.featureNameDictionary[$scope.selectedLanguage],
                $translate.instant('from')+" "+$('#datepicker-start').val(),
                $translate.instant('to')+" "+$('#datepicker-end').val()
                
            ];
            
            var from ="";
            if($('#datepicker-start').val() != "") 
                from  = (new Date($('#datepicker-start').val())).toISOString().replace('Z','');
                
            var to ="";
            if($('#datepicker-end').val() != "") 
                to  = (new Date($('#datepicker-end').val())).toISOString().replace('Z','');
            var requests = [
            ];
            requests.push({
                roomName:$translate.instant('RoomLbl'),
                featureName:$translate.instant('Featurelbl'),
                createTime:$translate.instant('CreateTime'),
                status:$translate.instant('StatusLbl'),
                action:$translate.instant('Actionby'),
                actionTime:$translate.instant('ActionTime'),
                comment:$translate.instant('Comment'),
                time:$translate.instant('Time'),
                from:$translate.instant('from'),
                to:$translate.instant('to'),
                description:$translate.instant('DescriptionLbl'),
                count:$translate.instant('NumberLbl'),
                price:$translate.instant('priceLbl'),
                totalPrice:$translate.instant('totalpriceLbl')
            })
           // var deferme = $q.defer();
            
             var k = RequestResource.getAllRequest({ pageSize:0,roomId: vm.selectedRoom.roomId
                ,featureId:vm.selectedFeature.featureId,from:from , to:to }).$promise.then(function(results) {
                    // requests = results;
                _.forEach(results.results, function (request) {
                    request.createTime= request.createTime+"Z";
                   request.createTime = $filter('date')(new Date(request.createTime), "dd/MM/yyyy hh:mm a");
                   request.modifyTime= request.modifyTime+"Z";
                  request.modifyTime = $filter('date')(new Date(request.modifyTime), "dd/MM/yyyy hh:mm a");
                  if(request.requestTime !=null){                      
                    request.requestTime= request.requestTime+"Z";
                    request.requestTime = $filter('date')(new Date(request.requestTime), "dd/MM/yyyy hh:mm a");
                  }
                  request.requestDetails.forEach(function(element) {
                    if(element.from !=null){                      
                        element.from= element.from+"Z";
                        element.from = $filter('date')(new Date(element.from), "dd/MM/yyyy hh:mm a");
                      }
                      if(element.to !=null){                      
                        element.to= element.to+"Z";
                        element.to = $filter('date')(new Date(element.to), "dd/MM/yyyy hh:mm a");
                      }
                      requests.push({
                        roomName:request.roomName,
                        featureName:request.featureNameDictionary[$scope.selectedLanguage],
                        createTime:request.createTime,
                        status:$translate.instant(request.status),
                        action:request.modifier,
                        actionTime:request.status !="Pending"?request.modifyTime:"",
                        comment:request.comment,
                        time:request.requestTime,
                        from:element.from,
                        to:element.to,
                        description:element.descriptionDictionary != undefined? element.descriptionDictionary[$scope.selectedLanguage]:"",
                        count:element.number,
                        price:element.price,
                        totalPrice:element.price*element.number
                    })
                  }, this);
                  if(request.requestDetails.length<=0){
                    requests.push({
                        roomName:request.roomName,
                        featureName:request.featureNameDictionary[$scope.selectedLanguage],
                        createTime:request.createTime,
                        status:$translate.instant(request.status),
                        action:request.modifier,
                        actionTime:request.status !="Pending"?request.modifyTime:"",
                        comment:request.comment,
                        time:request.requestTime,
                        from:null,
                        to:null,
                        description:null,
                        count:null,
                        price:null,
                        totalPrice:null
                        
                    })
                  }
                 
                });
                vm.reportData = requests
                vm.charEncode = $scope.selectedLanguage == "ar-eg"?"utf8":"utf-8"
                vm.canDownload = true;
               // deferme.resolve(requests); 
                // return requests.results;
			},
            function(data, status) {
                ToastService.show("right","bottom","fadeInUp",data.message,"error");
                vm.isLoading = false;
            });
          //  return deferme.promise;
            // return k.promise;
        }
        vm.download = function(){
            vm.canDownload = false;
        }
	}
	
}());
