(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('categoryController', ['$scope','$stateParams','$translate', 'appCONSTANTS','$uibModal','GetCategoriesResource', 'CategoryResource','ActivateCategoryResource','DeactivateCategoryResource','categoriesPrepService','ToastService',  categoryController])

    function categoryController($scope,$stateParams ,$translate , appCONSTANTS,$uibModal,GetCategoriesResource, CategoryResource,ActivateCategoryResource,DeactivateCategoryResource,categoriesPrepService,ToastService){

        var vm = this;
		vm.categories = categoriesPrepService;
		vm.Now = $scope.getCurrentTime();
		$('.pmd-sidebar-nav>li>a').removeClass("active") 
		
		function refreshCategories(){
			var k = GetCategoriesResource.getAllCategories({ MenuId: $stateParams.menuId,page:vm.currentPage }).$promise.then(function(results) {
				vm.Now = $scope.getCurrentTime();	
				vm.categories = results;
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshCategories();
		}
		vm.openCategoryDialog = function(){		
			if($scope.selectedLanguage != appCONSTANTS.defaultLanguage)
			{
				var englishCategories;
				var k = GetCategoriesResource.getAllCategories({ MenuId: $stateParams.menuId,pagesize:0, lang: appCONSTANTS.defaultLanguage}).$promise.then(function(results) {
					englishCategories = results;
					var modalContent = $uibModal.open({
						templateUrl: './app/RestaurantAdmin/templates/editCategory.html',
						controller: 'editCategoryDialogController',
						controllerAs: 'editCategoryDlCtrl',
						resolve:{
							mode:function(){return "map"},
							englishCategories: function(){return englishCategories.results;},
							category:function(){ return null},
							callBackFunction:function(){return refreshCategories;}
						}
						
					});
				});
			}
			else{
				var modalContent = $uibModal.open({
					templateUrl: './app/RestaurantAdmin/templates/newCategory.html',
					controller: 'categoryDialogController',
					controllerAs: 'categoryDlCtrl',
					resolve:{
                        menuId: function(){ return $stateParams.menuId;},
						callBackFunction:function(){return refreshCategories;}
					}
					
				});
			}
		}
		function confirmationDelete(itemId){
			CategoryResource.deleteCategory({categoryId:itemId}).$promise.then(function(results) {
				ToastService.show("right","bottom","fadeInUp",$translate.instant('CategoryDeleteSuccess'),"success");
				refreshCategories();
			},
            function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.message,"error");
            });
		}
		vm.openDeleteCategoryDialog = function(name,id){			
			var modalContent = $uibModal.open({
				templateUrl: './app/core/Delete/templates/ConfirmDeleteDialog.html',
				controller: 'confirmDeleteDialogController',
				controllerAs: 'deleteDlCtrl',
				resolve: {
					itemName: function () { return name },
					itemId: function() { return id },
					message:function() { return null},
					callBackFunction:function() { return confirmationDelete }
				}
				
			});
		}
		
		vm.openEditCategoryDialog = function(index){
			var modalContent = $uibModal.open({
				templateUrl: './app/RestaurantAdmin/templates/editCategory.html',
				controller: 'editCategoryDialogController',
				controllerAs: 'editCategoryDlCtrl',
				resolve:{
					mode:function(){return "edit"},
					englishCategories: function(){return null;},
					category:function(){ return vm.categories.results[index]},
					callBackFunction:function(){return refreshCategories;}
				}
				
			});
			
		}
		vm.Activate = function(category){
			ActivateCategoryResource.Activate({categoryId:category.categoryId})
			.$promise.then(function(result){
				category.isActive = true;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}

		vm.Deactivate = function(category){
			DeactivateCategoryResource.Deactivate({categoryId:category.categoryId})
			.$promise.then(function(result){
				category.isActive = false;
			},
			function(data, status) {
				ToastService.show("right","bottom","fadeInUp",data.data.message,"error");
			})
		}		
		
		
		
	}
	
}
    ());
