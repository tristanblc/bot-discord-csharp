using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotBlazonApplication.Services.Classes
{
    public class User
    {

        [Key]
        public string Email { get; init; }
        public string Password { get; init; }
        public string Token { get; init; }

        public User(string email, string password, string token)
        {
            Email = email;
            Password = password;
            Token = token;
        }

        public User()
        {
        }
    }
}
