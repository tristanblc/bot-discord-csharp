using ApiApplication.Authentification.Interface;
using ApiApplication.Controllers.Interfaces;
using ApiApplication.Repository;
using ApiApplication.Repository.Interface;
using BotClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceClassLibrary.Interfaces;

namespace ApiApplication.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IJWTManagerRepository JwtManager;
		private ILoggerProject LoggerProject { get; init; }
		public UserController(IJWTManagerRepository jwtManager)
		{
			JwtManager = jwtManager;
			
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("createaccount")]
		public IActionResult CreateAccount(Users usersdata)
		{
			try
			{
				JwtManager.CreateUser(usersdata);
				return Ok();

			}
			catch (Exception ex)
			{
				LoggerProject.WriteLogErrorLog(ex.Message);
				return BadRequest();
			}



		}



		[AllowAnonymous]
		[HttpPost]
		[Route("authenticate")]
		public IActionResult Authenticate(Users usersdata)
		{

			var token = JwtManager.Authenticate(usersdata);

			if (token == null)
            {
				LoggerProject.WriteLogErrorLog("0 account");
				return BadRequest(new { message = "Username or password is incorrect" });
			}
	

			return Ok(token);
		


		}		
		
    }
}
