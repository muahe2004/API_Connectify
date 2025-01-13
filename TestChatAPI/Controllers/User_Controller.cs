using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestChatAPI.BLL;
using TestChatAPI.BLL.Interfaces;

namespace TestChatAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class User_Controller : ControllerBase
	{
		private readonly IUser_Services _user_Services;
		public User_Controller(IUser_Services user_Services)
		{
			_user_Services = user_Services;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllUserAsync()
		{
			var Users = await _user_Services.GetAllUserAsync();

			if (Users == null || !Users.Any())
			{
				return NotFound("No users found.");
			}

			return Ok(Users);
		}
	}
}
