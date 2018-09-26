(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('feedBackController', ['$scope','$filter', 'appCONSTANTS','feedBacksPrepService','ToastService' , 'FeedBackResource',  feedBackController])

    function feedBackController($scope,$filter, appCONSTANTS,feedBacksPrepService,ToastService ,FeedBackResource){
		
        var vm = this;
        vm.feedBacks = feedBacksPrepService;
        vm.feedBacks.results.forEach(function(element) {
            element.createTime = element.createTime+"z"
            element.createTime = $filter('date')(new Date(element.createTime), "dd/MM/yyyy hh:mm a");
            
        }, this);

		$('.pmd-sidebar-nav>li>a').removeClass("active")
		$($('.pmd-sidebar-nav').children()[5].children[0]).addClass("active")
        vm.currentPage = 1;
        vm.changePage = function (page) {
            vm.currentPage = page;
            refreshFeedBack();
		}        
		function refreshFeedBack(){
            FeedBackResource.getAllFeedBack({page:vm.currentPage}).$promise.then(function (results) {
                
                results.results.forEach(function(element) {
                    element.createTime = element.createTime+"z"
                    element.createTime = $filter('date')(new Date(element.createTime), "dd/MM/yyyy hh:mm a");
                    
                }, this);
                vm.feedBacks = results;
                
            },
            function (data, status) {

             });
        }
	}
	
}
());
