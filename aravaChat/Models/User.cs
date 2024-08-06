using System.ComponentModel.DataAnnotations;

namespace aravaChat.Models
{
    public class User
    {
        public User()
        {
            messages = new List<Message>();
        }

        [Key]
        public int id { get; set; }

        [Display(Name = "User Name")]
        public string user_name { get; set; } = "";

        [Display(Name = "Password")]
        public string password { get; set; } = "";

        [Display(Name = "Nick Name")]
        public string nick_name { get; set; } = "";
        public List<Message> messages { get; set; }

    }
}
