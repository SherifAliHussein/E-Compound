(function() {
	angular
		.module('core')
		.constant('appCONSTANTS', {
			 'API_URL': 'http://localhost:36402/api/',
		//	'API_URL': 'http://ecompoundbackend-testing.azurewebsites.net/api/',			

			'defaultLanguage':'en'
		})
		.constant('messageTypeEnum', {
      success: 0,
      warning: 1,
      error: 2
		}).constant('userRolesEnum', {
			Room:"Room"
    });
}());