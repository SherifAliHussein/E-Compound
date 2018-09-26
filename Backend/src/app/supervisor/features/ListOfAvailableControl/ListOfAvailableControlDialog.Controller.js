angular.module('home').directive('listofavailableControlForm', function(){
    return {
        restrict: 'E',
        replace: true,
        scope: { featureControl: '=',selectedLanguage:'=',islistAvailable:"=" },
        templateUrl: "./app/supervisor/features/ListOfAvailableControl/templates/ListOfAvailableControlPopup.html",
        controllerAs:"listOfAvailableControlDlCtrl",
        controller:function($scope,$rootScope,controlEnum ,daysEnum,ManageFeatureResource,appCONSTANTS,$http,ToastService,$translate,AvailableControlService,$timeout,$filter){
            var vm = this;
            vm.featureControl = $scope.featureControl;
            vm.controls = controlEnum;
            vm.days = angular.copy(daysEnum);
            vm.language = appCONSTANTS.supportedLanguage;
            vm.featureName = AvailableControlService.getFeatureName();
            vm.close = function(){
                $scope.islistAvailable = false;                
            }
            $timeout(function(){
                vm.days.forEach(function(element) {
                    $('#datepicker-start'+element.id).datetimepicker({
                        format: 'LT'
                    });
                
                    // End date date and time picker 
                    $('#datepicker-end'+element.id).datetimepicker({
                        format: 'LT'
                    });
                    
                }, this);
             }, 100);
             vm.changeDay = function(day){
                 if(!day.isSelected){
                     day.startTime= "";
                     day.endTime= "";
                     day.max= "";
                 }
             }
            function add(){
                vm.isChanged = true;
                var newFeatureDetail = new ManageFeatureResource();
                newFeatureDetail.descriptionDictionary = vm.featureDetail.descriptionDictionary;
                newFeatureDetail.isFree= vm.featureDetail.isFree;
                newFeatureDetail.price = vm.featureDetail.isFree?0:vm.featureDetail.price;
                newFeatureDetail.featureControlId = AvailableControlService.getFeatureControlId();
                newFeatureDetail.availables = []
                var today= new Date()
                
                vm.days.forEach(function(element) {
                    if(element.isSelected){
                        newFeatureDetail.availables.push({                            
                            from: (new Date(today.getMonth()+"/"+today.getDate()+"/"+today.getFullYear()+" "+element.startTime).toISOString()).replace('Z',''),
                            to: (new Date(today.getMonth()+"/"+today.getDate()+"/"+today.getFullYear()+" "+element.endTime).toISOString()).replace('Z',''),
                            max:element.max,day:element.id});
                    }
                }, this);
                var model = new FormData();
                model.append('data', JSON.stringify(newFeatureDetail));
                // model.append('file', featureImage);
                $http({
                    method: 'POST',
                    url: appCONSTANTS.API_URL + 'Features/Detail',
                    useToken: true,
                    headers: { 'Content-Type': undefined },
                    data: model
                }).then(
                    function(data, status) {
                        ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureUpdateSuccess'),"success");
                         vm.isChanged = false;                     
                         $scope.islistAvailable = false;
                         $rootScope.$broadcast('listofavailableChange');
                         //  $uibModalInstance.dismiss('cancel');
                        //  callBackFunction();                    
                    },
                    function(data, status) {
                        vm.isChanged = false;                     
                        ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                    }
                );
                
            }
            function update(){
                vm.isChanged = true;
                var newFeatureDetail = new ManageFeatureResource();
                newFeatureDetail.descriptionDictionary = vm.featureDetail.descriptionDictionary;
                newFeatureDetail.featureDetailId = vm.featureDetail.featureDetailId;
                newFeatureDetail.isFree= vm.featureDetail.isFree;
                newFeatureDetail.price = vm.featureDetail.isFree?0:vm.featureDetail.price;
                newFeatureDetail.featureControlId = AvailableControlService.getFeatureControlId();
                newFeatureDetail.availables = [];
                var today= new Date()

                vm.days.forEach(function(element) {
                    if(element.isSelected){
                        newFeatureDetail.availables.push({
                            from:(new Date(today.getMonth()+"/"+today.getDate()+"/"+today.getFullYear()+" "+element.startTime).toISOString()).replace('Z',''),
                            to:(new Date(today.getMonth()+"/"+today.getDate()+"/"+today.getFullYear()+" "+element.endTime).toISOString()).replace('Z',''),
                            max:element.max,day:element.id,availableId:element.availableId});
                    }
                }, this);
                // newFeatureDetail.$updateFeatureDetail().then(
                //     function(data, status) {
                // 		ToastService.show("right","bottom","fadeInUp",$translate.instant('RoomAddSuccess'),"success");
                // 		$uibModalInstance.dismiss('cancel');
                // 		callBackFunction();
                //     },
                //     function(data, status) {
                // 		ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                //     }
                // );
                var model = new FormData();
                model.append('data', JSON.stringify(newFeatureDetail));
                // model.append('file', featureImage);
                $http({
                    method: 'PUT',
                    url: appCONSTANTS.API_URL + 'Features/Detail',
                    useToken: true,
                    headers: { 'Content-Type': undefined },
                    data: model
                }).then(
                    function(data, status) {
                        ToastService.show("right","bottom","fadeInUp",$translate.instant('FeatureUpdateSuccess'),"success");
                         vm.isChanged = false;
                         $scope.islistAvailable = false;
                         $rootScope.$broadcast('listofavailableChange');                         
                         //  $uibModalInstance.dismiss('cancel');
                        //  callBackFunction();  
                    },
                    function(data, status) {
                        vm.isChanged = false;                     
                        ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                    }
                );
            }
            featureDetail = AvailableControlService.getFeatureDetail();
            if(featureDetail == null){
                vm.save = add;
                vm.featureDetail = {};
                vm.featureDetail.isFree = true;
                vm.featureDetail.price  = 0;
               
            }
            else{
                vm.save = update;
                vm.featureDetail = featureDetail;
                vm.days.forEach(function(element) {
                    var day = $filter('filter')(featureDetail.availables, {day:element.id});
                    if(day.length >0){
                        $timeout(function(){
                            // vm.days.forEach(function(it) {
                                $('#datepicker-start'+element.id).datetimepicker({
                                    format: 'LT'
                                });
                            
                                // End date date and time picker 
                                $('#datepicker-end'+element.id).datetimepicker({
                                    format: 'LT'
                                });
                                
                        element.isSelected = true;
                        // day[0].from= day[0].from+"Z";
                        element.startTime = day[0].from;
                        // day[0].to= day[0].to+"Z";
                        element.endTime = day[0].to;
                        element.max = day[0].max;
                        element.availableId = day[0].availableId
                            // }, this);
                         }, 100);
                        // element.endTime = day.endTime
                        // newFeatureDetail.availables.push({from:element.startTime,to:element.endTime,max:element.max,day:element.id});
                    }
                }, this);
                // $timeout(function(){
                //     vm.days.forEach(function(element) {
                //         vm.days;
                //         $('#datepicker-start'+element.id).datetimepicker({
                //             format: 'LT'
                //         });
                    
                //         // End date date and time picker 
                //         $('#datepicker-end'+element.id).datetimepicker({
                //             format: 'LT'
                //         });
                        
                //     }, this);
                //  }, 100);
            }
            
                 
        }
        
    };
});
