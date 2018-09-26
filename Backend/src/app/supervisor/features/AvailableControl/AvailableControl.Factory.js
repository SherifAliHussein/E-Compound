(function() {
    angular
        .module('home')
        .factory('AvailableControlService',[ AvailableControlService]);

    function AvailableControlService() {
        var featureDetail;
        var featureName;
        var FeatureControlId;
        return {
            // featureName,
            // featureDetail,
            setFeatureDetail: function(param) {
                featureDetail = param;
            },
            getFeatureDetail:function(){
                return featureDetail;
            },
            setFeatureName:function(param){
                featureName = param;
            },            
            getFeatureName:function(){
                return featureName;
            },
            setFeatureControlId:function(param){
                FeatureControlId = param;
            },            
            getFeatureControlId:function(){
                return FeatureControlId;
            }
        }

    }
}());

angular.module('home').directive('datetimepicker', function(){
	return {
		require: '?ngModel',
		restrict: 'A',
		link: function(scope, element, attrs, ngModel){

			if(!ngModel) return; // do nothing if no ng-model

			ngModel.$render = function(){
				element.val( ngModel.$viewValue || '' );
			}

			// element.datetimepicker({
            //     format: 'LT'
            // });

			element.on('dp.change', function(e){
                if(element[0].id.includes('start')){
                    var k = angular.copy(element[0].id);
                    k = k.replace('start','end')
                    $('#'+k).data("DateTimePicker").minDate(e.date)
                }
				scope.$apply(read);
			});

			read();

			function read() {
				var value = element.val();
				ngModel.$setViewValue(value);
			}
		}
	}
});
