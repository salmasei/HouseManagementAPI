namespace HouseManagementAPI.Models
{
    public class HouseModel
    {
        public string Address { get; set; }
        public int NumberOfFloors { get; set; }
        public string UnitType { get; set; } // house, apartment, serialhouse
        public List<string> Features { get; set; } // balcony, fireplace, parking, etc.
    }
}
