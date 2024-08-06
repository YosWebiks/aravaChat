using aravaChat.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace aravaChat.DAL
{
    public class DataLayer : DbContext
    {
        public DataLayer(string cs) : base(GetOptions(cs))
        {
            Database.EnsureCreated();

            Seed();
        }

        private void Seed()
        {
            if (!Users.Any())
            {
                User user = new User { user_name = "Johnny770", password = "770", nick_name = "Johnny 770" };
                user.messages = CreateDefaultMessagesList(user);
                Users.Add(user);
                SaveChanges();
            }
        }

        private List<Message> CreateDefaultMessagesList(User user)
        {
            List<Message> messageList = new List<Message>();
            Message message = new Message { text = "Hey y'all!", created_at = DateTime.Now, sender = user };
            Messages.Add(message);
            return messageList;
        }



        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }


        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions
                .UseSqlServer(new DbContextOptionsBuilder(), connectionString)
                .Options;
        }
    }
}