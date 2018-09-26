(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('CategoryTemplateController', ['$scope','$translate', '$stateParams', 'appCONSTANTS','$uibModal','allMenuPrepService','templatesPrepService','ToastService' ,'GetCategoriesNameResource','CategoryTemplateResource' ,  CategoryTemplateController])

    function CategoryTemplateController($scope,$translate,$stateParams, appCONSTANTS,$uibModal, allMenuPrepService, templatesPrepService, ToastService, GetCategoriesNameResource, CategoryTemplateResource){
        var vm = this;
        vm.menus = allMenuPrepService;
        vm.templates = templatesPrepService;
        vm.selectedTemplateId= 0;
        vm.selectedMenu = vm.menus[0];
        vm.selectedTemplates = [];
        vm.page=1;
        
        var totalItemsCount = 0;
        vm.isCategoryTemplateReady = false;
		$('.pmd-sidebar-nav>li>a').removeClass("active")	
		$($('.pmd-sidebar-nav').children()[4].children[0]).addClass("active")
        function loadCategory(){
            if(vm.selectedMenu != null){
                
            GetCategoriesNameResource.getAllCategoriesName({ MenuId: vm.selectedMenu.menuId })
            .$promise.then(function(results) {
                vm.categories = results;                
                vm.selectedTemplates = [];
                vm.page=1;
                totalItemsCount = 0;
                vm.selectedCategory = vm.categories[0];
                vm.selectedTemplateId= 0;
                vm.remainingItems = vm.selectedCategory.itemCount;
                // vm.isCategoryTemplateReady = false;
                if(vm.selectedCategory.itemCount <= totalItemsCount){
                    vm.isCategoryTemplateReady = true;
                }
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
            vm.selectedTemplates = [];
            vm.page=1;
            totalItemsCount = 0;
            vm.selectedTemplateId= 0;        
            // vm.selectedCategory = vm.categories[0];
            vm.remainingItems = vm.selectedCategory.itemCount;
            vm.isCategoryTemplateReady = false;
            
        }


        vm.selectTemplate = function(){
            vm.templates.forEach(function(element) {
                if(element.id == vm.selectedTemplateId){
                    var temp = angular.copy(element);
                    temp.page = vm.page;
                    vm.selectedTemplates.push(temp);
                    vm.selectedTemplateId = 0;
                    vm.page++;
                    totalItemsCount += temp.itemCount;
                    if(vm.selectedCategory.itemCount <= totalItemsCount){
                        vm.isCategoryTemplateReady = true;
                    }
                    vm.remainingItems = vm.selectedCategory.itemCount - totalItemsCount;
                    vm.remainingItems  = vm.remainingItems < 0 ? 0 : vm.remainingItems ;
                }
            }, this);
            console.log(vm.selectedTemplates)
        }

        vm.save = function(){
            var newCategroyTemplate = new CategoryTemplateResource();
            var categoryTemplates = []
            vm.selectedTemplates.forEach(function(element) {
                categoryTemplates.push({categoryId:vm.selectedCategory.categoryId,templateId:element.id,pageNumber:element.page})
            }, this);
            newCategroyTemplate.PageModels = categoryTemplates;
            newCategroyTemplate.$create({ categoryId: vm.selectedCategory.categoryId }).then(
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",$translate.instant('TemplateUpdateSuccessfuly'),"success");
                },
                function(data, status) {
					ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
                }
            );
        }


    }
	
}
());