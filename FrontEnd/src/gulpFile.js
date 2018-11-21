// requires
var gulp = require('gulp');
// var sass = require('gulp-sass');
var concat = require('gulp-concat');
var cssnano = require('gulp-cssnano');
var imageop = require('gulp-image-optimization');
var gulpCopy = require('gulp-copy');
 var cleanCSS = require('gulp-clean-css');
 var minify = require('gulp-minify');
var minifyHtml = require('gulp-htmlmin');
var webserver = require('gulp-webserver');
var ngHtml2Js = require('gulp-ng-html2js');
var uglify = require('gulp-uglify');
var inject = require('gulp-inject-string');
 var watch = require('gulp-watch');
var gutil = require('gulp-util');
var replace = require('gulp-replace');
var strip = require('gulp-strip-comments');
var config = {
    production: !!gutil.env.production
};
uglify = config.production ? uglify : gutil.noop;


  //var minifyHtml = require('gulp-minify-html')
var runSequence = require('run-sequence').use(gulp);

  var rimraf = require('rimraf');
 var ngTemplate = require('gulp-ng-template');
 var concatCss = require('gulp-concat-css');
 var version = require('gulp-version-number');
 var paths = {
  app: [
  './app/**/*.js',
  '!./app/core/**/*.js'
  ],
  core: [
    './app/core/app.module.js',
    './app/core/home/app.module.js',
    './app/core/app.config.js',
    './app/core/core.constants.js',
    './app/core/app.routes.js',
	  './app/core/confirmPassword.directive.js',
	  './app/core/translateProvider.js',
    './app/core/ToastService.factory.js',
    './app/core/GlobalAdmin/*.js',
    './app/core/RestaurantAdmin/*.js',
    './app/core/Delete/*.js',
    './app/core/login/*.js',
    './app/core/home/*.js',
    './app/core/Authentication/*.js',
    './app/core/Authorization/*.js',
    './app/core/Toast/*.js',
    './app/core/OfflineData.Factory.js',
    
     // './app/**/*.*.js',
    
  ],
  libs: [
    './assets/js/jquery.min.js',
    './node_modules/lodash/lodash.min.js',
    './node_modules/angular/angular.js',
    './node_modules/angular-ui-router/release/angular-ui-router.js',
    './node_modules/angular-resource/angular-resource.js',
    './node_modules/angular-permission/dist/angular-permission.js',
    './node_modules/angular-animate/angular-animate.js',
    './node_modules/angular-messages/angular-messages.js',
    './node_modules/angular-aria/angular-aria.js',
    './node_modules/ngstorage/ngStorage.js',
    './node_modules/angular-sanitize/angular-sanitize.min.js',
    // './node_modules/propellerkit/dist/js/propeller.js',
    // './node_modules/bootstrap-ui-datetime-picker/dist/datetime-picker.min.js',
    './node_modules/moment/moment.js',
    './node_modules/angularjs-bootstrap-datetimepicker/src/js/datetimepicker.js',
    './node_modules/angularjs-bootstrap-datetimepicker/src/js/datetimepicker.templates.js',
	'./node_modules/propellerkit/dist/js/bootstrap.min.js',
	'./node_modules/angular-ui-bootstrap/dist/ui-bootstrap-tpls.js',
    './node_modules/angular-paging/dist/paging.js',
     './node_modules/ngprogress-lite/ngprogress-lite.min.js',
     './node_modules/angular-translate/dist/angular-translate.min.js',
     './node_modules/angular-ui-event/dist/event.min.js',
    //  './node_modules/select2/dist/js/select2.full.min.js', 
     './assets/js/*.js',
     './node_modules/angular-jk-rating-stars/dist/jk-rating-stars.min.js', 
     './node_modules/angular-ui-carousel/dist/ui-carousel.min.js', 
     './node_modules/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.concat.min.js', 
     './node_modules/ng-scrollbars/dist/scrollbars.min.js', 
     './node_modules/ui-select/dist/select.js'
     
  
  ],
//  sass: {
//    main: './src/assets/scss/app.scss',
//    mainRTL: './src/assets/scss/app-rtl.scss',
//    loadPaths: [
//      './src/assets/scss/*.scss',
//      './bower_components/lumx/dist/scss/',
//      './bower_components/bourbon/app/assets/stylesheets/',
//      './bower_components/mdi/scss'
//    ]
//  },
  css: [
    //'./node_modules/angular-material/angular-material.css',
	  './node_modules/propellerkit/dist/css/bootstrap.min.css',
    './node_modules/propellerkit/dist/css/propeller.min.css',
    // './node_modules/select2/dist/css/select2.min.css',
    // './node_modules/select2-bootstrap-theme/dist/select2-bootstrap.min.css',
    './node_modules/ngprogress-lite/ngprogress-lite.css',
    './node_modules/nvd3/build/nv.d3.min.css',
    './assets/css/jquery_te.css', 
    './assets/css/bootstrap.min.css', 
    './assets/css/lity.css', 
    './assets/css/style.css', 
    './node_modules/angular-jk-rating-stars/dist/jk-rating-stars.min.css', 
    './node_modules/angular-ui-carousel/dist/ui-carousel.min.css', 
    './node_modules/angularjs-bootstrap-datetimepicker/src/css/datetimepicker.css',
    './node_modules/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css',
    './node_modules/ui-select/dist/select.min.css'

  ],
  
  cssAR: [
    //'./node_modules/angular-material/angular-material.css',
	  './node_modules/propellerkit/dist/css/bootstrap.min.css',
    './node_modules/propellerkit/dist/css/propeller.min.css',
    // './node_modules/select2/dist/css/select2.min.css',
    // './node_modules/select2-bootstrap-theme/dist/select2-bootstrap.min.css',
    './node_modules/ngprogress-lite/ngprogress-lite.css',
    './node_modules/nvd3/build/nv.d3.min.css',
    './assets/css/jquery_te.css', 
    './assets/css/bootstrap.min.css', 
    './assets/css/lity-ar.css', 
    './assets/css/style-ar.css', 
    './node_modules/angular-jk-rating-stars/dist/jk-rating-stars.min.css', 
    './node_modules/angular-ui-carousel/dist/ui-carousel.min.css', 
    './node_modules/angularjs-bootstrap-datetimepicker/src/css/datetimepicker.css',
    './node_modules/malihu-custom-scrollbar-plugin/jquery.mCustomScrollbar.css',
    './node_modules/ui-select/dist/select.min.css'
    
  ],
  templates: [
    './app/**/*.html'
  ],
  index: './index.html',
  favicon: 'src/favicon1.ico',
  build: 'dist',
  temp: '.tmp',
  docs: '.documentation',
 
  turnJsEN:[
    './assets/turnJS/turn.min.js',    
  ],
  tunrJsAr:[
    './assets/turnJS/turn.min-ar.js',    
  ]
}
 
// sass task
//gulp
//	.task('sass', function () {
//	  return gulp.src('./assets/sass/**/*.scss')
//	    .pipe(sass().on('error', sass.logError))
//	    .pipe(gulp.dest('./assets/css'));
//});

//gulp
//	.task('sass:watch', function () {
//	  gulp
//		  .watch('./assets/sass/**/*.scss', ['sass'])
//				.on('change', function(event) {
//		    console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
//		    });
//});

//service worker js
// gulp.task('copy-serviceWorker', function() {
//   gulp.src(paths.serviceWorker)
//  .pipe(concat('serviceWorker.js', {newLine: ''}))
//  //.pipe(uglify())
//  .pipe(strip())
//  .pipe(gulp.dest(paths.build + '/'));
// });

gulp.task('copy-turnJsEN', function() {
  gulp.src(paths.turnJsEN)
 .pipe(concat('turn.min-en.js', {newLine: ''}))
 //.pipe(uglify())
 .pipe(strip())
 .pipe(gulp.dest(paths.build + '/'));
});

gulp.task('copy-tunrJsAr', function() {
  gulp.src(paths.tunrJsAr)
 .pipe(concat('turn.min-ar.js', {newLine: ''}))
 //.pipe(uglify())
 .pipe(strip())
 .pipe(gulp.dest(paths.build + '/'));
});

//concatination js
gulp.task('copy-libs', function() {
   gulp.src(paths.libs)
    .pipe(concat('libs.js'))
    .pipe(uglify())
    .pipe(strip())
    .pipe(gulp.dest(paths.build + '/'));
});
//concatination js
gulp.task('copy-core', function() {
  return gulp.src(paths.core)
    .pipe(concat('core.js', {newLine: ';'}))
    //.pipe(uglify())
    .pipe(gulp.dest(paths.build + '/'));
});

//concatination js
gulp.task('copy-app', function() {
     gulp.src(paths.app)
    .pipe(concat('app.js', {newLine: ''}))
    //.pipe(uglify())
    .pipe(strip())
    .pipe(gulp.dest(paths.build + '/'));
});

 //concat css
gulp.task('css', function () {
  return gulp.src(paths.css)
    .pipe(concat('bundle-en-us.css'))
    .pipe(cleanCSS({compatibility: 'ie8'}))
    .pipe(gulp.dest(paths.build + '/'));
});

 //concat css ar
 gulp.task('cssAr', function () {
  return gulp.src(paths.cssAR)
    .pipe(concat('bundle-ar-eg.css'))
    .pipe(cleanCSS({compatibility: 'ie8'}))
    .pipe(gulp.dest(paths.build + '/'));
});

//concat css
/* gulp.task('img', function () {
   return gulp.src(paths.assets)
     .pipe(concat('img.svg'))
     .pipe(gulp.dest(paths.build + '/'));
 });
*/ 
gulp.task('images', function(cb) {
    gulp.src(['./assets/img/*'])
            .pipe(gulp.dest(paths.build + '/assets/img'));
});
gulp.task('svgs', function(cb) {
    gulp.src(['./assets/svg/*'])
            .pipe(gulp.dest(paths.build + '/assets/svg'));
});
gulp.task('fonts', function(cb) {
    gulp.src(['./assets/fonts/**/*.{eot,svg,ttf,woff,woff2}'])
            .pipe(gulp.dest(paths.build + '/fonts'));
});
 //cash template
gulp.task('copy-templates', function() {
   gulp
    .src(paths.templates)
    .pipe(ngHtml2Js({
      moduleName: "home",
      declareModule: false,
        rename: function(templateUrl) {
         return  './app/'+templateUrl
        }
    }))
    .pipe(concat('templates.js'))
     .pipe(uglify())
    .pipe(strip())
    .pipe(gulp.dest(paths.build + '/'));
});

// gulp.task('copy-templates', function() {
//   gulp.src('./app/**/*.html')
//     .pipe(minifyHtml({empty: true, quotes: true}))
//     .pipe(ngTemplate({
//       moduleName: 'vlabs',
//       standalone: true,
//       filePath: 'templates.js'
//     }))
//     .pipe(gulp.dest(paths.build + '/'));  // output file: 'dist/js/templates.js' 
// })


gulp.task('copy-index', function() {
  gulp
    .src(paths.index)
    .pipe(version({
      'value' : '%MD5%',
      'replaces' : [/#{VERSION_REPlACE}#/g]
    }))

    .pipe(gulp.dest(paths.build + '/'));
});


//serving
gulp.task('serve', function() {
  gulp.src(paths.build)
    .pipe(webserver({
      port: 8090,
      host: 'localhost',
      livereload: true,
      open: true
    }));
});

// copy task
gulp.task('copy', ['css','cssAr','copy-libs','copy-core','copy-app','copy-templates','copy-index','images','svgs','fonts','copy-turnJsEN','copy-tunrJsAr']);

//clean build
gulp.task('clean-build', function(cb) {
  rimraf(paths.build, cb)
});

// clean temp
gulp.task('clean-temp', function(cb) {
  rimraf(paths.temp, cb)
});

gulp.task('clean', ['clean-temp', 'clean-build']);

gulp.task('build', function(cb) {
  runSequence('clean', 'copy', cb);
});



gulp.task('default', function(cb) {
  runSequence('serve',['build','watch'], cb);

  // gulp.watch(paths.libs, ['copy-libs']);
  // gulp.watch(paths.core, ['copy-core']);
  // gulp.watch(paths.app, ['copy-app']);
  // gulp.watch(paths.template, ['copy-templates']);
  // gulp.watch(paths.index, ['copy-index']);
});

gulp.task('watch', function(cb) {

  gulp.watch(paths.libs, ['copy-libs']);
  gulp.watch(paths.core, ['copy-core']);
  gulp.watch(paths.app, ['copy-app']);
  gulp.watch(paths.templates, ['copy-templates']);
  gulp.watch(paths.index, ['copy-index']);
});


// gulp.task('watch', function() {
//       gulp.watch(paths.libs, ['copy-libs']);
// });