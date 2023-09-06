using System.ComponentModel.DataAnnotations;

namespace AirportProjectCore.Models
{
    public class AirportInfo
    {
        [Key]
        public string IataCode { get; set; }

        public string AirportName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }
    }
    
}
