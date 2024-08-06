using System.ComponentModel.DataAnnotations;

namespace aravaChat.Models
{
    public class Message
    {
        [Key]
        public int id { get; set; }
        public string text { get; set; } = "";
        public User? sender { get; set; }
        public DateTime? created_at { get; set; }
    }
}
