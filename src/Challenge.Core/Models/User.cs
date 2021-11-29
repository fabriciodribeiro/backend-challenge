using System;

namespace Challenge.Core.Models
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        public User(string name,
            string email,
            string userName,
            string password,
            string salt)
        {
            Name = name;
            Email = email;
            UserName = userName;
            Password = password;
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
        }
    }
}
