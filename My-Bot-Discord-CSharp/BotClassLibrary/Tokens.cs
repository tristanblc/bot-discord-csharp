using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotClassLibrary
{

	public class Tokens
	{

		[Key]
		public string Email { get; init; }

		public string Token { get; init; }

		public DateTime ExpireDate { get; init; }
	}
}
