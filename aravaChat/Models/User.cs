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
        public int id;
        public string user_name = "";
        public string password = "";
        public string nick_name = "";
        public List<Message> messages;

    }
}
