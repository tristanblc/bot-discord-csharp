
using System.Security.Cryptography;
using System.Text;

namespace ApiApplication.Services
{

        internal class PasswordHasherService : IPasswordHasherService
        {
            public string GetPasswordHasher(string password_not_hashed)
            {
                using (var sha256 = SHA256.Create())
                {
                    // Send a sample text to hash.  
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password_not_hashed));
                    // Get the hashed string.  
                    var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    // Print the string.
                    // 
                    return hash;

                }

            }
       }
    

}
