angular.module('home').directive('flipbook', function($timeout){
    return{
      restrict: 'E',
      replace: true,
      scope: { itempagectrl: '=' ,itemdetails:'=',selectedLanguage:'='},
      compile: function(){
        return{
          pre: function(scope, iElement, iAttrs, controller){
            var element = $('.text_editor').children();
            
            element.jqte({ 
              focus: function () {
                 element.parents(".jqte").find(".jqte_toolbar").show();
                 element.parents(".jqte").click(function () { element.parents(".jqte").find(".jqte_toolbar").show(); });
                  scope.$apply(function () {
                     
                  });
              }, 
              blur: function () {
                element.parents(".jqte").find(".jqte_toolbar").hide();
                  scope.$apply(function () {
                      
                  });
              }, 
              change: function () {
                ngModel.$setViewValue(element.parents(".jqte").find(".jqte_editor")[0].innerHTML);
                  scope.$apply(function () {
                      
                  });
              }
            });
            element.parents(".jqte").find(".jqte_toolbar").hide();
          },
          post: function(scope, iElement, iAttrs, controller) {
         
            $timeout(function(){
              iElement.turn({
              
               pages: 10,
               page: 2,
               when: {
                turned: function(event, page, pageObj) {
                  console.log("aa0")
                },
                turning: function(event, page, pageObj) {
                  console.log("aa1") 
                  if(page == 1) {
                    event.preventDefault();
                }
                }
              }
             }).bind("start", function(event, pageObject, corner) {
              
              if(pageObject.next == 1) {
                event.preventDefault();
            }
            })


           }, 0);
          }
              
        }
      },
      controller: function($scope, $rootScope){
        $scope.hide_book = function(){
          console.log("hide_book");
          $rootScope.show_book = false;
        }
        
      },
      templateUrl: "./app/items/Templates/ItemList.html"
    }
  });