angular.module('home').directive('pageTemplate2', function(){
    return {
        restrict: 'E',
        replace: true,
        scope: { pageitems: '=' ,itemdetails: '=',selectedLanguage:'=' },
        templateUrl: "./app/items/Templates/itemTemplate2.html",
        controller:function($scope,$localStorage){
            console.log($scope.itemdetails)
            console.log($localStorage.language)
            $scope.lang = $localStorage.language;
            $scope.viewItemDetail=function(item){
                $scope.$parent.$parent.$parent.itemdetails = item;                
            }    
        }
        
    };
});