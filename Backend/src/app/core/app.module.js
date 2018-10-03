(function() {
    'use strict';

    angular
    .module('core', [
    		'ngResource',
        'ui.router',
        'ngStorage',
      'permission',
      'bw.paging',
      'ui.event',
      'ngProgressLite',
    'ui.bootstrap',
    'pascalprecht.translate',
    'ui.sortable',
    'ngSanitize', 'ngCsv'
    ]);
}());
