(function() {
    angular
      .module('home')
      .factory('ReceptionistResource', ['$resource', 'appCONSTANTS', ReceptionistResource])
      .factory('ActivateReceptionistResource', ['$resource', 'appCONSTANTS', ActivateReceptionistResource])
      .factory('DeactivateReceptionistResource', ['$resource', 'appCONSTANTS', DeactivateReceptionistResource])
      .factory('SupervisorResource', ['$resource', 'appCONSTANTS', SupervisorResource])
      .factory('ActivateSupervisorResource', ['$resource', 'appCONSTANTS', ActivateSupervisorResource])
      .factory('DeactivateSupervisorResource', ['$resource', 'appCONSTANTS', DeactivateSupervisorResource])
      .factory('TechnicianResource', ['$resource', 'appCONSTANTS', TechnicianResource])
      .factory('ActivateTechnicianResource', ['$resource', 'appCONSTANTS', ActivateTechnicianResource])
      .factory('DeactivateTechnicianResource', ['$resource', 'appCONSTANTS', DeactivateTechnicianResource])
  
      ;
  
    function ReceptionistResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Users/Receptionist/:receptionistId', {}, {
        getAllReceptionists: { method: 'GET', useToken: true },
        getReceptionist: { method: 'GET', useToken: true },
        create: { method: 'POST', useToken: true },
        deleteReceptionist: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }

    function ActivateReceptionistResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Users/Receptionist/:receptionistId/Activate', {}, {
          Activate: { method: 'GET', useToken: true}
        })
    }
    function DeactivateReceptionistResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Users/Receptionist/:receptionistId/DeActivate', {}, {
          Deactivate: { method: 'GET', useToken: true }
        })
    }


    function SupervisorResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Users/Supervisor/:supervisorId', {}, {
          getAllSupervisors: { method: 'GET', useToken: true },
          getSupervisor: { method: 'GET', useToken: true },
          create: { method: 'POST', useToken: true },
          deleteSupervisor: { method: 'DELETE', useToken: true },
          update: { method: 'PUT', useToken: true }
        })
      }
  
      function ActivateSupervisorResource($resource, appCONSTANTS) {
          return $resource(appCONSTANTS.API_URL + 'Users/Supervisor/:supervisorId/Activate', {}, {
            Activate: { method: 'GET', useToken: true}
          })
      }
      function DeactivateSupervisorResource($resource, appCONSTANTS) {
          return $resource(appCONSTANTS.API_URL + 'Users/Supervisor/:supervisorId/DeActivate', {}, {
            Deactivate: { method: 'GET', useToken: true }
          })
      }

      function TechnicianResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Users/Technician/:technicianId', {}, {
          getAllTechnicians: { method: 'GET', useToken: true },
          getTechnician: { method: 'GET', useToken: true },
          create: { method: 'POST', useToken: true },
          deleteTechnician: { method: 'DELETE', useToken: true },
          update: { method: 'PUT', useToken: true }
        })
      }
  
      function ActivateTechnicianResource($resource, appCONSTANTS) {
          return $resource(appCONSTANTS.API_URL + 'Users/Technician/:technicianId/Activate', {}, {
            Activate: { method: 'GET', useToken: true}
          })
      }
      function DeactivateTechnicianResource($resource, appCONSTANTS) {
          return $resource(appCONSTANTS.API_URL + 'Users/Technician/:technicianId/DeActivate', {}, {
            Deactivate: { method: 'GET', useToken: true }
          })
      }

}());
  