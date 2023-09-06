using System.ComponentModel.DataAnnotations;

namespace AirportProjectCore.Models
{
    public class FeedBack
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }
    }
}
