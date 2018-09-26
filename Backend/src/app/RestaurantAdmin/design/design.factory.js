(function() {
    angular
      .module('home')
      .factory('TemplateResource', ['$resource', 'appCONSTANTS', TemplateResource])
      .factory('CategoryTemplateResource', ['$resource', 'appCONSTANTS', CategoryTemplateResource]);
  
    function TemplateResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Templates/', {}, {
        getTemplates: { method: 'GET', useToken: true,isArray: true }
      })
    }

    function CategoryTemplateResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Categories/:categoryId/Template', {}, {
        create: { method: 'POST', useToken: true }
      })
    }

}());
  