(function() {
	angular
		.module('core')
		.constant('appCONSTANTS', {
			//  'API_URL': 'http://localhost:36402/api/',
			'API_URL': 'http://eguestbackend-v2-testing.azurewebsites.net/api/',			
			'defaultLanguage':'en-us',
			'supportedLanguage':{
				'en-us':{'key':'en-us','value':'english'},
				'ar-eg':{'key':'ar-eg','value':'arabic'}
			}
		})
		.constant('messageTypeEnum', {
      success: 0,
      warning: 1,
      error: 2
		}).constant('userRolesEnum', {
			Admin:"Admin",
			Room:"Room"
    }).constant('controlEnum', [
		{id:0,text:"ListOfText"},
		{id:1,text:"ListOfImage"},
		{id:2,text:"ListOfTextAndImage"},
		{id:3,text:"Videos"},
		{id:4,text:"Available"},
		{id:5,text:"Time"},
		{id:6,text:"ListOfAvailable"},
		])
	.constant('daysEnum', [
		{id:0,text:"Sunday"},
		{id:1,text:"Monday"},
		{id:2,text:"Tuesday"},
		{id:3,text:"Wednesday"},
		{id:4,text:"Thursday"},
		{id:5,text:"Friday"},
		{id:6,text:"Saturday"},
		]);;
}());