(function () {
    'use strict';

    angular
        .module('home')
        .controller('ItemController', ['$compile','$scope', '$translate', '$stateParams', 'appCONSTANTS', 'categoryItemsTemplatePrepService', 'ResturantPrepService', 'totalCartService','CartIconService','ItemsResource', ItemController])

    function ItemController($compile,$scope, $translate, $stateParams, appCONSTANTS, categoryItemsTemplatePrepService, ResturantPrepService, totalCartService,CartIconService,ItemsResource) {
 
        var vm = this;
        $scope.cartIcon = true;
        $scope.$watch("cartIcon", function (newValue) {
            CartIconService.cartIcon = newValue;
          });
        vm.catgoryTemplates = categoryItemsTemplatePrepService;
        $scope.$parent.globalInfo= ResturantPrepService;
        vm.selectedLanguage = $scope.selectedLanguage;
        
        vm.restaurantId = $stateParams.restaurantId;
        console.log($scope.selectedLanguage)
       /* 
     //   console.log(vm.catgoryTemplates);
       //  vm.itemDetails;
        // vm.viewItemDetails=function(item){
        //     console.log(item)
        // vm.itemDetails = categoryItemsTemplatePrepService.templates[0].itemModels[0];
        // } */
       vm.currentItem=0; 
       vm.selectedSize = 10;
        vm.selectedSide = 10; 
        $scope.checkradioasd = -1;
        $scope.selectedCount=1;
        $scope.cart = [];
        $scope.total = 0;
        $scope.item = {
            itemobj: "",
            size: "",
            sides: [],
        }; 
        $scope.displayAdd = false;

        $scope.addItemToCart = function (product) {
       if($scope.selectedCount < 1)
       {
        alert("Must postive number"); 
        return;
       }     
if(vm.currentItem != product.itemID){
    $scope.item = {
        itemobj: "",
        size: "",
        sides: [],
    }; 
     alert("Please Choose the correct item"); 
    return;
}
            for (var i = 0; i < $scope.selectedCount; i++) {

                if ($scope.cart.length === 0) {
                    product.count = 1;
                    $scope.item.itemobj = product;
                    $scope.cart.push($scope.item);

                } else {
                    var repeat = false;
                    for (var k = 0; k < $scope.cart.length; k++) {
                        if ($scope.cart[k].itemobj.itemID === product.itemID && $scope.cart[k].size.sizeId ===   $scope.item.size.sizeId) {
                            repeat = true;
                            $scope.cart[k].itemobj.count += 1;
                        }
                    }
                    if (!repeat) {
                        product.count = 1;
                        $scope.item.itemobj = product;
                        $scope.cart.push($scope.item);
                    }
                } 

                $scope.total += parseFloat($scope.item.size.price);

            }
            var CheckOutLocalstorage = JSON.parse(localStorage.getItem("checkOut"));
            if (CheckOutLocalstorage != null) {
                for (var s = 0; s < CheckOutLocalstorage.length; s++) {
                    var repeat = false;
                    for (var z = 0; z < $scope.cart.length; z++) {
                      
                        var id=$scope.cart[z].itemobj.itemID;
                        var objsize=$scope.cart[z].size.sizeId;

                        var stordId=CheckOutLocalstorage[s].itemobj.itemID ;
                        var stordSize=CheckOutLocalstorage[s].size.sizeId;

                          if (id === stordId && objsize ===stordSize) {
                            repeat = true;
                            $scope.cart[z].itemobj.count +=CheckOutLocalstorage[s].itemobj.count;
                           
                        
                        }
                    }
                    if (!repeat) {
                        $scope.item.itemobj = product;
                        $scope.cart.push(CheckOutLocalstorage[s]); 
                    }
                    
                }
            $scope.total=0;
            for (var t = 0; t < $scope.cart.length; t++) {
                var product = $scope.cart[t];
                $scope.total += (product.size.price * product.itemobj.count);
            }
        }

            $scope.homeTotalNo = $scope.total; 

            $scope.$watch("homeTotalNo", function (newValue) {
                totalCartService.homeTotalNo = newValue;
              });
              
            localStorage.setItem('checkOut', JSON.stringify($scope.cart));
            $scope.cart=[];
            $scope.item = {
                itemobj: "",
                size: "",
                sides: [],
            };
            $scope.displayAdd = false;
            $scope.checkradioasd = -1;
            $scope.selectedCount=1;
        };

        
        $scope.radioSizeClick = function (size,item) {
            vm.currentItem=item;
            $scope.checkradioasd = size.sizeId;
            $scope.item.size = size;
            if ($scope.item.size != "") {
                $scope.displayAdd = true;
            }

        };

        $scope.checkSideClick = function (side) {
            if ($scope.item.sides.indexOf(side) !== -1) {
                var index = $scope.item.sides.indexOf(side);
                $scope.item.sides.splice(index, 1);
                if ($scope.item.sides.length == 0) {
                        /* $scope.displayAdd = false;*/
                }
            }
            else {
                $scope.item.sides.push(side);
                if ($scope.item.sides.length > 0 && $scope.item.size != "") {
                    /*  $scope.displayAdd = true;*/
                }
            }
        };
        $scope.addCounter = function () { 
            $scope.selectedCount = $scope.selectedCount+1;  
            
        };
        $scope.removeCounter = function () { 
            if($scope.selectedCount <= 1){
                return;
            }
            $scope.selectedCount = $scope.selectedCount-1;  
        };

        vm.likeItem = function(item){
            item.like++;
            ItemsResource.likeItem({itemId:item.itemID});
        }
        vm.dislikeItem = function(item){
            item.dislike++;
            ItemsResource.dislikeItem({itemId:item.itemID});
           
        }
    }

}
());
