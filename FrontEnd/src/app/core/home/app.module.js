(function() {
  'use strict';
  angular.module('home', ['core']) 
  .service('CurrentItem', function() {
    this.CurrentItemId = 0;
  })  
  .service('CartIconService', function() {
    this.cartIcon = true;
  })  
.service('totalCartService', function() {
    this.homeTotalNo;
  });
  ;
  
}());
