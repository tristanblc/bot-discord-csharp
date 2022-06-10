using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{
    public class Users
    {

        [Key]
        public string Email { get; init; }
        public string Password { get; init; }

        public Users(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public Users()
        {
        }
    }
}
