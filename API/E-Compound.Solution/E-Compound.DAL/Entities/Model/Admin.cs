using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Compound.DAL.Entities.Model
{
    public class Admin:User
    {
        public Admin()
        {
            Supervisors = new List<Supervisor>();
            Receptionists = new List<Receptionist>();
            Features = new List<Feature>();
            Rooms = new List<Room>();
            Restaurants = new List<Restaurant>();
            RestaurantTypes = new List<RestaurantType>();
            Packages = new List<Package>();
            Buildings = new List<Building>();
            Floors = new List<Floor>();
        }
        public Guid UserAccountId { get; set; }
        public long ProductId { get; set; }
        public virtual List<Supervisor> Supervisors { get; set; }
        public virtual List<Receptionist> Receptionists { get; set; }
        public virtual List<Feature> Features { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<RestaurantType> RestaurantTypes { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual ICollection<Floor> Floors { get; set; }
        public virtual ICollection<UnitType> UnitTypes { get; set; }
        public long? FeaturesBackgroundId { get; set; }

    }
}
