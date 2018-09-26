
angular.module('core')
  .directive('equalto', function() {
    return {
      restrict: 'A',
      require: 'ngModel',
      scope: {
        otherModelValue: '=equalto'
      },
      link: function(scope, element, attributes, ngModel) {

        ngModel.$validators.equalto = function(modelValue) {
          return modelValue == scope.otherModelValue.$modelValue;
        };
        scope.$watch('otherModelValue.$modelValue', function() {
          ngModel.$validate();
        },true);

      }
    };
  });
