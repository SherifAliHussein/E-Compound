(function () {
    'use strict';

    angular
        .module('home')
        .controller('cartController', ['$rootScope', '$translate', '$scope', 'CartResource', 'appCONSTANTS', '$stateParams', '$state', '_', 'authenticationService', 'authorizationService', '$localStorage', 'userRolesEnum', 'ToastService', 'MenuOfflineResource', 'OfflineDataResource', 'totalCartService', 'CartIconService', 'ResturantPrepService','RequestResource', cartController])

    function cartController($rootScope, $translate, $scope, CartResource, appCONSTANTS, $stateParams, $state, _, authenticationService, authorizationService, $localStorage, userRolesEnum, ToastService, MenuOfflineResource, OfflineDataResource, totalCartService, CartIconService,ResturantPrepService,RequestResource) {

        $scope.$parent.globalInfo= ResturantPrepService;
        
        $scope.homeTotalNo = 0;
        $scope.cartIcon = false;
        $scope.$watch("cartIcon", function (newValue) {
            CartIconService.cartIcon = newValue;
        });
        var vm = this;
        vm.item = {
            itemobj: "",
            size: "", 
        };
        vm.currentItem=0; 
        vm.index = 0;
        vm.isItemLoaded = false;
        var total = 0;
        $scope.selectedCount = 0;
        
        $scope.total = 0;
        $scope.selectedCount = 1;


        vm.repeatCart = JSON.parse(localStorage.getItem("checkOut"));
        
        for (var i = 0; i < vm.repeatCart.length; i++) {
            var product = vm.repeatCart[i];
            total += (product.size.price * product.itemobj.count);
        }
        $scope.totalItem = total;

        
        $scope.addCounter = function (index) {
            vm.repeatCart[index].itemobj.count++;
            localStorage.setItem('checkOut', JSON.stringify(vm.repeatCart));
            var total = 0;            
            for (var i = 0; i < vm.repeatCart.length; i++) {
                var product = vm.repeatCart[i];
                total += (product.size.price * product.itemobj.count);
            }
            $scope.totalItem = total;
            $scope.homeTotalNo = $scope.totalItem;
            totalCartService.homeTotalNo = $scope.totalItem;
        }
        
        $scope.removeCounter = function (index) {
            vm.repeatCart[index].itemobj.count--;
           if(vm.repeatCart[index].itemobj.count <= 0) {
            vm.repeatCart.splice(index, 1);            
           }
           localStorage.setItem('checkOut', JSON.stringify(vm.repeatCart));
           if (vm.repeatCart == null || vm.repeatCart.length == 0) {
            $state.go('menu',{featureId: $stateParams.featureId,restaurantId:$stateParams.restaurantId});
           }
           var total = 0;           
           for (var i = 0; i < vm.repeatCart.length; i++) {
                var product = vm.repeatCart[i];
                total += (product.size.price * product.itemobj.count);
            }
            $scope.totalItem = total;
            $scope.homeTotalNo = $scope.totalItem;
            totalCartService.homeTotalNo = $scope.totalItem;

        };

        

        vm.isChanged = false;
        $scope.checkOut = function () {
            vm.isChanged = true;
            var items = JSON.parse(localStorage.getItem("checkOut"));
            var requestDetails = []
            items.forEach(function(element) {
                element.itemobj.itemSizes.forEach(function(itemSize) {
                    if(itemSize.sizeId == element.size.sizeId){
                        requestDetails.push({itemSizeId:itemSize.itemSizeId,number:element.itemobj.count,price:element.size.price})                        
                    }
                }, this);
                
                // requestDetails.push({itemSizeId:element.itemobj.itemID,number:element.itemobj.count,price:element.size.price})
            }, this);
            var newRequest = new RequestResource();
            newRequest.featureId = $stateParams.featureId;
            newRequest.requestDetails = requestDetails;
            newRequest.restaurantId = $stateParams.restaurantId;
            newRequest.$create().then(
                function(data, status) {
                    vm.isChanged = false;
                    $scope.homeTotalNo = 0;
                    $scope.$watch("homeTotalNo", function (newValue) {
                        totalCartService.homeTotalNo = newValue;
                    });
                    localStorage.removeItem('checkOut');
                    $state.go('menu',{featureId: $stateParams.featureId,restaurantId:$stateParams.restaurantId});
                },
                function(data, status) {
                    /*ToastService.show("right","bottom","fadeInUp",data.data.message,"error");*/
                }
            );
            console.log(items)
            
        };





    }


}());
