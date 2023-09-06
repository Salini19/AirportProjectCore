using System.ComponentModel.DataAnnotations;

namespace AirportProjectCore.Models
{
    public class StateImg
    {
        [Key]
        public string State { get; set; }

        public string Photo { get; set; }
    }
}
