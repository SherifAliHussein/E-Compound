(function() {
    angular
      .module('home')
      .factory('RoomResource', ['$resource', 'appCONSTANTS', RoomResource])
      .factory('ActivateRoomResource', ['$resource', 'appCONSTANTS', ActivateRoomResource])
      .factory('DeactivateRoomResource', ['$resource', 'appCONSTANTS', DeactivateRoomResource])
      .factory('UsedUnitsResource', ['$resource', 'appCONSTANTS', UsedUnitsResource])
      ;

      function RoomResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Rooms/:roomId', {}, {
          getAllRooms: { method: 'GET', useToken: true },
          getAllRoomsName: {url:appCONSTANTS.API_URL + 'Rooms/Name', method: 'GET', useToken: true,isArray:true },
          getRoom: { method: 'GET', useToken: true },
          create: { method: 'POST', useToken: true },
          deleteRoom: { method: 'DELETE', useToken: true },
          update: { method: 'PUT', useToken: true }
        })
      }

      function UsedUnitsResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Rooms/UsedUnits', {}, {
          getAllUsedUnits: { method: 'GET', useToken: true, isArray:true}
        })
    }
  
      function ActivateRoomResource($resource, appCONSTANTS) {
          return $resource(appCONSTANTS.API_URL + 'Rooms/:roomId/Activate', {}, {
            Activate: { method: 'GET', useToken: true}
          })
      }
      function DeactivateRoomResource($resource, appCONSTANTS) {
          return $resource(appCONSTANTS.API_URL + 'Rooms/:roomId/DeActivate', {}, {
            Deactivate: { method: 'GET', useToken: true }
          })
      }
}());
    