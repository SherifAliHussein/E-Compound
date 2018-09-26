angular.module('home').directive('pageTemplate6', function(){
    return {
        restrict: 'E',
        replace: true,
        scope: { pageitems: '=' ,itemdetails: '=',selectedLanguage:'=' },
        templateUrl: "./app/items/Templates/itemTemplate6.html",
        controller:function($scope,$localStorage){
            console.log($scope.itemdetails)
            $scope.lang = $localStorage.language;            
            $scope.viewItemDetail=function(item){
                $scope.$parent.$parent.$parent.itemdetails = item;                
            }    
        }
        
    };
});