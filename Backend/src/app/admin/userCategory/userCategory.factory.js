(function() {
    angular
      .module('home')
      .factory('UserCategoryPagingResource', ['$resource', 'appCONSTANTS', UserCategoryPagingResource])
      .factory('CategoryResource', ['$resource', 'appCONSTANTS', CategoryResource])
      .factory('AddUserCategoryResource', ['$resource', 'appCONSTANTS', AddUserCategoryResource])
      .factory('DeleteUserCategoryResource', ['$resource', 'appCONSTANTS', DeleteUserCategoryResource])
      .factory('UpdateUserCategoryResource', ['$resource', 'appCONSTANTS', UpdateUserCategoryResource])
   ;
  

    function UserCategoryPagingResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'UserCategoriesPaging', {}, {
        getAllPagingUserCategories: { method: 'GET', useToken: true} 
      })
    }
    
    function CategoryResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'UserCategories', {}, {
        getAllActivatedCategories: { method: 'GET', useToken: true, isArray:true} 
      })
    }


    function DeleteUserCategoryResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'UserCategories/:userCategoryId', {}, {
          deleteUserCategory: { method: 'DELETE', useToken: true} 
        })
    }

    function AddUserCategoryResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'UserCategories/', {}, { 
            addUserCategory: { method: 'POST',useToken: true}
        })
    }

    function UpdateUserCategoryResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'UserCategories', {}, {
          updateUserCategory: { method: 'PUT', useToken: true} 
        })
    }

  }());
  
