

using ApiApplication.Authentification.Interface;
using ApiApplication.Repository;
using ApiApplication.Services;
using BotClassLibrary;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiApplication.Authentification.Manager
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
		private readonly IConfiguration Iconfiguration;
		private readonly JwtRepository JWTRepository;
		private readonly IPasswordHasherService PasswordHasher;

		public JWTManagerRepository(IConfiguration iconfiguration, JwtRepository jwtrepository)
		{
			Iconfiguration = iconfiguration;
			JWTRepository = jwtrepository;
			PasswordHasher = new PasswordHasherService();

		}

		public Tokens Authenticate(Users users)
		{

			string email = users.Email;
			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(email);

			if (!match.Success)
				throw new Exception("Not an email");

			var user = JWTRepository.GetByEmail(users.Email);
			if (user == null)
				throw new Exception("Not user");



			var user_password = PasswordHasher.GetPasswordHasher(users.Password);

			if(PasswordHasher.GetPasswordHasher(users.Password) != user_password)
			{
				return null;
			}

			var tokenHandler = new JsonWebTokenHandler();
			// Creating asymmetric key
			var key = new RsaSecurityKey(RSA.Create(2048));
			// creating the jwt
			var jwt = new SecurityTokenDescriptor
			{
				Issuer = "localhost",
				Audience = " your - spa ",
				Expires = (DateTime.Now).AddYears(1),
				// Details hide
				SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSsaPssSha256) // <- Probabilistic algorithm
			};
			// subscribing to jwt
			string token = tokenHandler.CreateToken(jwt);
		

			var tokens =  new Tokens { Token = token , Email = user.Email, ExpireDate = (DateTime.Now).AddDays(30) };

            try
            {
				JWTRepository.Add(tokens);

			}
			catch (Exception ex)
            {
				JWTRepository.Update(tokens);
            }

			

			return tokens;



		}

        public void CreateUser(Users user)
        {

			var my_user = JWTRepository.GetByEmail(user.Email);
			if (my_user != null)
				throw new Exception("Already exist");


			string email = user.Email;
			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(email);



            if (!match.Success)
				throw new Exception("Not an email");


			Regex regex1 = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{6,20}$");
			Match match_password = regex1.Match(user.Password);
			if (!match_password.Success)
				throw new Exception("Get more strong password");


			try
            {

				Users persist_user = new Users(user.Email, PasswordHasher.GetPasswordHasher(user.Password));
			

				JWTRepository.Add(persist_user);

			}catch(Exception ex)
			{
				throw new Exception("Error insert");
			}		

        }
    }
}
