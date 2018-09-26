angular.module('home').directive('pageTemplate1', function(){
    return {
        restrict: 'E',
        replace: true,
        scope: { pageitems: '=' ,itemdetails: '=',selectedLanguage:'=' },
        templateUrl: "./app/items/Templates/itemTemplate1.html",
        controller:function($scope,$localStorage){
            $scope.lang = $localStorage.language;
            $scope.viewItemDetail=function(item){
                $scope.$parent.$parent.$parent.itemdetails = item;

                
            }      
        }
        
    };
});