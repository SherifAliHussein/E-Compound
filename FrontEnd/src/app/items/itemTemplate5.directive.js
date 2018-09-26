angular.module('home').directive('pageTemplate5', function(){
    return {
        restrict: 'E',
        replace: true,
        scope: { pageitems: '=' ,itemdetails: '=',selectedLanguage:'=' },
        templateUrl: "./app/items/Templates/itemTemplate5.html",
        controller:function($scope,$localStorage){
            console.log($scope.itemdetails)
            $scope.lang = $localStorage.language;            
            $scope.viewItemDetail=function(item){
                $scope.$parent.$parent.$parent.itemdetails = item;                
            }    
        }
        
    };
});