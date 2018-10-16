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
           
                var model = new FormData();
                model.append('data', JSON.stringify(newFeatureDetail));
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
                                $('#datepicker-start'+element.id).datetimepicker({
                                    format: 'LT'
                                });
                            
                                $('#datepicker-end'+element.id).datetimepicker({
                                    format: 'LT'
                                });
                                
                        element.isSelected = true;
                        element.startTime = day[0].from;
                        element.endTime = day[0].to;
                        element.max = day[0].max;
                        element.availableId = day[0].availableId
                         }, 100);
                    }
                }, this);
               
            }
            
                 
        }
        
    };
});
