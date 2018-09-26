(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('itemOrderController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal' ,'allMenuPrepService','GetCategoriesNameResource','GetItemsResource','ToastService','ItemOrderResource','UpdateItemOrderResource',  itemOrderController])

    function itemOrderController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal ,allMenuPrepService,GetCategoriesNameResource,GetItemsResource,ToastService,ItemOrderResource,UpdateItemOrderResource){
		var vm = this;

	
		
        vm.menus = allMenuPrepService;
		vm.selectedMenu = vm.menus[0];
		vm.categoryItems = [];
		vm.sortingLog = [];
		vm.sortingLogId = [];
		vm.isChanged = true;
		$('.pmd-sidebar-nav>li>a').removeClass("active")	
		$($('.pmd-sidebar-nav').children()[6].children[0]).addClass("active")
        function loadCategory(){
            if(vm.selectedMenu != null){
                
            GetCategoriesNameResource.getAllCategoriesName({ MenuId: vm.selectedMenu.menuId })
            .$promise.then(function(results) {
                vm.categories = results;                
                vm.selectedTemplates = [];
                vm.page=1; 
                vm.selectedCategory = vm.categories[0];
				vm.selectedTemplateId= 0;
				
				vm.changeCategory();
              
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
            }
        }
        loadCategory();
        vm.changeMenu = function(){
            loadCategory();
        }

        vm.changeCategory = function(){ 
			vm.page=1;     
			vm.isChanged = true;
			ItemOrderResource.getAllItemOrder({ categoryId: vm.selectedCategory.categoryId})
            .$promise.then(function(results) {
				vm.categoryItems = results.templates; 
				console.log(vm.categoryItems);               
                vm.selectedTemplates = [];
                vm.page=1; 
                vm.selectedTemplateId= 0;
				vm.isChanged = false;  
				asd()
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
            
        }		
		
		vm.sortableOptions = {
			placeholder: "app",
			connectWith: ".apps-container"
		  };
		  

		  vm.Save = function(){
			vm.isChanged = true;			
			  console.log(vm.categoryItems);
			  var itemOrder = [];
			  var count = 1;
			  vm.categoryItems.forEach(function(element) {
				  element.itemModels.forEach(function(item) {
					itemOrder.push({itemId: item.itemID,orderNumber:count});
					count++;
				  }, this);
			  }, this);
			  var itemOrderResource = new UpdateItemOrderResource();
			  itemOrderResource.itemNames = itemOrder;
			  itemOrderResource.$updateOrder().then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('OrderItemUpdateSuccess'),"success");
					 vm.isChanged = false;                     
					 
                },
                function(data, status) {
                    vm.isChanged = false;                     
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
		  }

		  vm.error = false;
		  function asd(){
			vm.categoryItems.forEach(function(element) {
				$scope.$watch(function () { return element.itemModels.length  },function(newVal,oldVal){
					vm.error = false;
					vm.categoryItems.forEach(function(element) {
						if(element.itemModels.length > element.itemCount){
							vm.error =true;
							return false;
						}
						else
						{
							if(!vm.error)
							vm.error = false;		
						}
					}, this);
				 })
			
			}, this);
		  }
		  vm.isValid = function(){
			vm.categoryItems.forEach(function(element) {
				if(element.itemModels.length > element.itemCount){
					vm.error =true;
					return false;
				}
				else
				{
					vm.error = false;		
				}
			}, this);
			vm.error = false;
			return false;
		  }
	}
	
}
    ());
