(function() {
    angular
        .module('home')
        .factory('OfflineDataResource',['$localStorage', OfflineDataResource]);

    function OfflineDataResource($localStorage) {
        var categories;
        return {
            setAllData: function(lang,allMenus) {
                if($localStorage.offlineData == null)
                {
                    $localStorage.offlineData = {};
                }
                $localStorage.offlineData[lang] = allMenus; 
            },

            getMenus: function() {
                return $localStorage.offlineData[$localStorage.language]
            },
            getAllCategories:function(menuId){
                
                $localStorage.offlineData[$localStorage.language].forEach(function(menu) {
                    if(menu.menuId == menuId){
                        categories =  menu.categoryModels;    
                    }
                }, this);
                return categories;
                // return $localStorage.offlineData[menuId];
            },
            getAllItems:function(menuId,categoryId){
                var items;
                $localStorage.offlineData[$localStorage.language].forEach(function(menu) {
                    if(menu.menuId == menuId){
                        //categories =  menu.categoryModels;    
                        menu.categoryModels.forEach(function(category) {
                            if(category.categoryId == categoryId){
                                items =  category.categoryPageTemplateModel;    
                            }
                        }, this);
                    }
                }, this);
                
                // categories.forEach(function(category) {
                //     if(category.categoryId == categoryId){
                //         items =  category.categoryPageTemplateModel;    
                //     }
                // }, this);
                return items;
                // return $localStorage.offlineData[menuId];
            }
            
        }

    }


}());
