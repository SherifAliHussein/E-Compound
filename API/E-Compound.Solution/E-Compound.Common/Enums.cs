using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.Common
{
    public class Enums
    {
        public enum RoleType
        {
            Admin,
            Supervisor,
            Receptionist,
            Room,
            RestaurantAdmin,
            Waiter
        }
        public enum RequestStatus
        {
            Pending,
            Approved,
            Rejected
        }

        public enum FeatureType
        {
            Normal,
            Restaurant
        }

        public enum Control
        {
            ListOfText,
            ListOfImage,
            ListOfTextAndImage,
            Videos,
            Available,
            Time,
            ListOfAvailable,
        }
        
        public enum ControlType
        {
            None,
            Single,
            Multiple
        }

        public enum Day
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }
    }
}
