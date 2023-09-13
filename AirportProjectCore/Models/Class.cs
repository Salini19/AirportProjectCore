namespace AirportProjectCore.Models
{
    public class Class
    {
    }
    public class Location
    {

        public double Latitude { get; set; }
        public double Longitude { get; set; }



        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    public class State
    {
        public string Sname { get; set; }
    }
    public class AirInfo
    {
        public string IataCode { get; set; }
        public string AirportName { get; set; }
        public string City { get; set; }
        public double Distance { get; set; }
    }

}
