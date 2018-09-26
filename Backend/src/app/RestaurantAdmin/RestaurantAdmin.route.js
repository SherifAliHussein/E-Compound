(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
              .state('Menu', {
					url: '/menu',
                    templateUrl: './app/RestaurantAdmin/templates/menu.html',
                    controller: 'menuController',
                    'controllerAs': 'menuCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                           redirectTo: 'root'
                        },
                        displayName: 'Menu'
                    },
                    resolve: {
                        menusPrepService: menusPrepService,
                        RestaurantIsReadyPrepService:RestaurantIsReadyPrepService
                    }
                 
                })
                .state('newMenu', {
                      url: '/NewMenu',
                      templateUrl: './app/RestaurantAdmin/templates/newMenu.html',
                      controller: 'menuDialogController',
                      'controllerAs': 'menuDlCtrl',
                      data: {
                          permissions: {
                              only: ['RestaurantAdmin'],
                             redirectTo: 'root'
                          },
                          displayName: 'Menu'
                      }
                   
                  })
                  .state('editMenu', {
                        url: '/Menu/:menuId',
                        templateUrl: './app/RestaurantAdmin/templates/editMenu.html',
                        controller: 'editMenuDialogController',
                        'controllerAs': 'editMenuDlCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                               redirectTo: 'root'
                            },
                            displayName: 'Menu'
                        },
                        resolve: {
                            menuPrepService: menuPrepService
                        }
                    })
                .state('Category', {
                      url: '/menu/:menuId/Category',
                      templateUrl: './app/RestaurantAdmin/templates/Category.html',
                      controller: 'categoryController',
                      'controllerAs': 'categoryCtrl',
                      data: {
                          permissions: {
                              only: ['RestaurantAdmin'],
                             redirectTo: 'root'
                          },
                          displayName: 'Category'
                      },
                      resolve: {
                        categoriesPrepService: categoriesPrepService
                      }                   
                  })
                  .state('newCategory', {
                        url: '/menu/:menuId/NewCategory',
                        templateUrl: './app/RestaurantAdmin/templates/newCategory.html',
                        controller: 'categoryDialogController',
                        'controllerAs': 'categoryDlCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                               redirectTo: 'root'
                            },
                            displayName: 'Category'
                        }             
                    })
                    .state('editCategory', {
                          url: '/menu/:menuId/Category/:categoryId',
                          templateUrl: './app/RestaurantAdmin/templates/editCategory.html',
                          controller: 'editCategoryDialogController',
                          'controllerAs': 'editCategoryDlCtrl',
                          data: {
                              permissions: {
                                  only: ['RestaurantAdmin'],
                                 redirectTo: 'root'
                              },
                              displayName: 'Category'
                          },
                          resolve: {
                            categoryPrepService: categoryPrepService
                          }                   
                      })
                  .state('size', {
                        url: '/size',
                        templateUrl: './app/RestaurantAdmin/templates/size.html',
                        controller: 'sizeController',
                        'controllerAs': 'sizeCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                               redirectTo: 'root'
                            },
                            displayName: 'Size'
                        },
                        resolve: {
                          sizesPrepService: sizesPrepService
                        }
                     
                    })
                    .state('newsize', {
                          url: '/Newsize',
                          templateUrl: './app/RestaurantAdmin/templates/newSize.html',
                          controller: 'sizeDialogController',
                          'controllerAs': 'sizeDlCtrl',
                          data: {
                              permissions: {
                                  only: ['RestaurantAdmin'],
                                 redirectTo: 'root'
                              },
                              displayName: 'Size'
                          }                       
                      })
                      .state('editsize', {
                            url: '/size/:sizeId',
                            templateUrl: './app/RestaurantAdmin/templates/editSize.html',
                            controller: 'editSizeDialogController',
                            'controllerAs': 'editSizeDlCtrl',
                            data: {
                                permissions: {
                                    only: ['RestaurantAdmin'],
                                   redirectTo: 'root'
                                },
                                displayName: 'Size'
                            },
                            resolve: {
                                sizePrepService: sizePrepService
                            }                     
                        })
                    /*.state('sideItem', {
                          url: '/SideItem',
                          templateUrl: './app/RestaurantAdmin/templates/sideItem.html',
                          controller: 'sideItemController',
                          'controllerAs': 'sideItemCtrl',
                          data: {
                              permissions: {
                                  only: ['RestaurantAdmin'],
                                 redirectTo: 'root'
                              },
                              displayName: 'SideItem'
                          },
                          resolve: {
                            sideItemPrepService: sideItemPrepService
                          }
                       
                      })*/
                    .state('Items', {
                        url: '/Category/:categoryId/Item',
                        templateUrl: './app/RestaurantAdmin/templates/Item.html',
                        controller: 'ItemController',
                        'controllerAs': 'itemCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                                redirectTo: 'root'
                            },
                            displayName: 'Category'
                        },
                        resolve: {
                            itemsPrepService: itemsPrepService
                        }
                    })
                    .state('newItem', {
                        url: '/Category/:categoryId/newItem',
                        templateUrl: './app/RestaurantAdmin/templates/newItem.html',
                        controller: 'newItemController',
                        'controllerAs': 'newItemCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                               redirectTo: 'root'
                            },
                            displayName: 'Items'
                        },
                        resolve: {
                            ItemSizePrepService: ItemSizePrepService,
                            ItemSideItemPrepService: ItemSideItemPrepService,
                            defaultItemsPrepService: defaultItemsPrepService,
                        }                 
                    })
                    .state('editItem', {
                        url: '/Category/:categoryId/Items/:itemId',
                        templateUrl: './app/RestaurantAdmin/templates/editItem.html',
                        controller: 'editItemController',
                        'controllerAs': 'editItemCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                               redirectTo: 'root'
                            },
                            displayName: 'Items'
                        },
                        resolve: {
                            itemPrepService:itemPrepService,
                            ItemSizePrepService: ItemSizePrepService,
                            ItemSideItemPrepService: ItemSideItemPrepService
                        }                 
                    })
                    .state('Waiters', {
                        url: '/Waiter',
                        templateUrl: './app/RestaurantAdmin/templates/waiter.html',
                        controller: 'WaiterController',
                        'controllerAs': 'waiterCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                                redirectTo: 'root'
                            },
                            displayName: 'Waiters'
                        },
                        resolve: {
                            waitersPrepService: waitersPrepService
                            /*WaitersLimitPrepService:WaitersLimitPrepService*/
                        }
                    })
                    .state('Backgrounds', {
                        url: '/Background',
                        templateUrl: './app/RestaurantAdmin/templates/background.html',
                        controller: 'BackgroundController',
                        'controllerAs': 'backgroundCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                                redirectTo: 'root'
                            },
                            displayName: 'Backgrounds'
                        },
                        resolve: {
                            backgroundsPrepService: backgroundsPrepService
                        }
                    })
                    
                    .state('Template', {
                        url: '/Template',
                        templateUrl: './app/RestaurantAdmin/templates/categoryTemplate.html',
                        controller: 'CategoryTemplateController',
                        'controllerAs': 'CategoryTemplateCtrl',
                        data: {
                            permissions: {
                                only: ['RestaurantAdmin'],
                                redirectTo: 'root'
                            },
                            displayName: 'Templates'
                        },
                        resolve: {
                            allMenuPrepService: allMenuPrepService,
                            templatesPrepService: templatesPrepService
                        }
                    })
                    .state('branch', {
                          url: '/Branch',
                          templateUrl: './app/RestaurantAdmin/templates/branch.html',
                          controller: 'branchController',
                          'controllerAs': 'branchCtrl',
                          data: {
                              permissions: {
                                  only: ['RestaurantAdmin'],
                                 redirectTo: 'root'
                              },
                              displayName: 'Branch'
                          },
                          resolve: {
                            branchsPrepService: branchsPrepService
                          }
                       
                      })
                      .state('newbranch', {
                            url: '/newBranch',
                            templateUrl: './app/RestaurantAdmin/templates/newBranch.html',
                            controller: 'branchDialogController',
                            'controllerAs': 'branchDlCtrl',
                            data: {
                                permissions: {
                                    only: ['RestaurantAdmin'],
                                   redirectTo: 'root'
                                },
                                displayName: 'Branch'
                            }
                         
                        })
                        .state('editbranch', {
                              url: '/Branch/:branchId',
                              templateUrl: './app/RestaurantAdmin/templates/editBranch.html',
                              controller: 'editBranchDialogController',
                              'controllerAs': 'editBranchDlCtrl',
                              data: {
                                  permissions: {
                                      only: ['RestaurantAdmin'],
                                     redirectTo: 'root'
                                  },
                                  displayName: 'Branch'
                              },
                              resolve: {
                                branchPrepService: branchPrepService
                              }
                          })
                          .state('itemOrder', {
                                url: '/order',
                                templateUrl: './app/RestaurantAdmin/templates/itemOrder.html',
                                controller: 'itemOrderController',
                                'controllerAs': 'itemOrderDlCtrl',
                                data: {
                                    permissions: {
                                        only: ['RestaurantAdmin'],
                                       redirectTo: 'root'
                                    },
                                    displayName: 'itemOrder'
                                },
                                resolve: {     
                                    allMenuPrepService: allMenuPrepService
                                }
                            }).state('feedBacks', {
                                url: '/feedback',
                                templateUrl: './app/RestaurantAdmin/templates/feedbacks.html',
                                controller: 'feedBackController',
                                'controllerAs': 'feedBackCtrl',
                                data: {
                                    permissions: {
                                        only: ['RestaurantAdmin'],
                                       redirectTo: 'root'
                                    },
                                    displayName: 'Menu'
                                },
                                resolve: {
                                    feedBacksPrepService: feedBacksPrepService
                                }
                             
                            })
        });
        
        menusPrepService.$inject = ['MenuResource']
        function menusPrepService(MenuResource) {
            return MenuResource.getAllMenus().$promise;
        }

        menuPrepService.$inject = ['MenuResource','$stateParams']
        function menuPrepService(MenuResource,$stateParams) {
            return MenuResource.getMenu({menuId : $stateParams.menuId}).$promise;
        }

        categoriesPrepService.$inject = ['GetCategoriesResource','$stateParams']
        function categoriesPrepService(GetCategoriesResource,$stateParams) {
            return GetCategoriesResource.getAllCategories({ MenuId: $stateParams.menuId }).$promise;
        }

        categoryPrepService.$inject = ['CategoryResource','$stateParams']
        function categoryPrepService(CategoryResource,$stateParams) {
            return CategoryResource.getCategory({ categoryId: $stateParams.categoryId }).$promise;
        }
        
        sizesPrepService.$inject = ['SizeResource']
        function sizesPrepService(SizeResource) {
            return SizeResource.getAllSizes().$promise;
        }

        sizePrepService.$inject = ['SizeResource','$stateParams']
        function sizePrepService(SizeResource,$stateParams) {
            return SizeResource.getSize({ sizeId: $stateParams.sizeId }).$promise;
        }
        
        sideItemPrepService.$inject = ['SideItemResource']
        function sideItemPrepService(SideItemResource) {
            return SideItemResource.getAllSideItems().$promise;
        }
        
        itemsPrepService.$inject = ['GetItemsResource','$stateParams']
        function itemsPrepService(GetItemsResource,$stateParams) {
            return GetItemsResource.getAllItems({ CategoryId: $stateParams.categoryId }).$promise;
        }

        ItemSizePrepService.$inject = ['SizeResource']
        function ItemSizePrepService(SizeResource) {
            return SizeResource.getAllSizes({ pagesize:0 }).$promise;
        }

        ItemSideItemPrepService.$inject = ['SideItemResource']
        function ItemSideItemPrepService(SideItemResource) {
            return SideItemResource.getAllSideItems({ pagesize:0 }).$promise;
        }

        itemPrepService.$inject = ['ItemResource','$stateParams']
        function itemPrepService(ItemResource,$stateParams) {
            return ItemResource.getItem({ itemId:$stateParams.itemId }).$promise;
        }

        defaultItemsPrepService.$inject = ['GetItemNamesResource','$stateParams','$localStorage','appCONSTANTS']
        function defaultItemsPrepService(GetItemNamesResource,$stateParams,$localStorage,appCONSTANTS) {
            if($localStorage.language != appCONSTANTS.defaultLanguage){
                return GetItemNamesResource.getAllItemNames({ CategoryId:$stateParams.categoryId, lang:appCONSTANTS.defaultLanguage }).$promise;
            }
            else
                return null;
        }

        RestaurantIsReadyPrepService.$inject = ['CheckRestaurantReadyResource']
        function RestaurantIsReadyPrepService(CheckRestaurantReadyResource) {
            return CheckRestaurantReadyResource.IsReady().$promise;
        }

        waitersPrepService.$inject = ['WaiterResource']
        function waitersPrepService(WaiterResource) {
            return WaiterResource.getAllWaiters().$promise;
        }

        backgroundsPrepService.$inject = ['BackgroundResource']
        function backgroundsPrepService(BackgroundResource) {
            return BackgroundResource.getAllBackgrounds().$promise; 
        }

        
        templatesPrepService.$inject = ['TemplateResource']
        function templatesPrepService(TemplateResource) {
            return TemplateResource.getTemplates().$promise;
        }
        
        allMenuPrepService.$inject = ['ActivatedMenuResource']
        function allMenuPrepService(ActivatedMenuResource) {
            return ActivatedMenuResource.getAllMenusName().$promise;
        }
        
        branchsPrepService.$inject = ['BranchResource']
        function branchsPrepService(BranchResource) {
            return BranchResource.getAllBranches().$promise;
        }

        branchPrepService.$inject = ['BranchResource','$stateParams']
        function branchPrepService(BranchResource,$stateParams) {
            return BranchResource.getBranch({branchId: $stateParams.branchId}).$promise;
        }
        
        WaitersLimitPrepService.$inject = ['WaitersLimitResource']
        function WaitersLimitPrepService(WaitersLimitResource) {
            return WaitersLimitResource.getWaitersLimit().$promise;
        }
        feedBacksPrepService.$inject = ['FeedBackResource']
        function feedBacksPrepService(FeedBackResource) {
            return FeedBackResource.getAllFeedBack().$promise;
        }
}());
