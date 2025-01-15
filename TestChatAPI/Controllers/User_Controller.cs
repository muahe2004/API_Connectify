using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TestChatAPI.BLL;
using TestChatAPI.BLL.Interfaces;
using static TestChatAPI.Model.User_Model;

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
		[HttpGet("Get_All_User")]
		public async Task<IActionResult> GetAllUserAsync()
		{
			var Users = await _user_Services.GetAllUserAsync();

			if (Users == null || !Users.Any())
			{
				return NotFound("No users found.");
			}

			return Ok(Users);
		}
		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] Login loginRequest)
		{
			var user = await _user_Services.Login(loginRequest);

			if (user == null)
			{
				return NotFound("Tài khoản không tồn tại");
			}
			else
			{
				return Ok(user);
			}
		}

		[HttpGet("Get_By_UserName")]
		public async Task<IActionResult> GetUserByUserName(string userName)
		{
			// Gọi service để lấy thông tin người dùng từ cơ sở dữ liệu
			var user = await _user_Services.GetUserByUserName(userName);  // passWord có thể null ở đây, nếu không cần

			if (user == null)
			{
				// Nếu không tìm thấy người dùng, trả về mã lỗi 404 (Not Found)
				return NotFound("User not found");
			}

			// Nếu tìm thấy người dùng, trả về thông tin người dùng với mã trạng thái 200 OK
			return Ok(user);
		}
        [Route("user/follower")]
        [HttpGet]
        public async Task<IActionResult> Follower(int userID)
        {
            var Users = await _user_Services.Follower(userID);

            if (Users == null || !Users.Any())
            {
                return NotFound("No users found.");
            }
            return Ok(Users);
        }

        [Route("user/following")]
        [HttpGet]
        public async Task<IActionResult> Following(int userID)
        {
            var Users = await _user_Services.Following(userID);

            if (Users == null || !Users.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(Users);
        }

        [Route("user/friendship")]
        [HttpGet]
        public async Task<IActionResult> Friendship(int userID)
        {
            var Users = await _user_Services.Friendship(userID);

            if (Users == null || !Users.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(Users);
        }

        [Route("user/create")]
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var create = await _user_Services.Create(user);

            if (create == null)
            {
                return NotFound("Thêm không thành công");
            }
            else
            {
                return Ok(create);
            }
        }

        [Route("user/update")]
        [HttpPut]
        public async Task<IActionResult> Update(User user)
        {
            var updated = await _user_Services.Update(user);
            if (updated == null)
            {
                return NotFound(new { Message = "Sửa không thành công" });
            }
            else
            {
                return Ok(updated);
            }
        }

        [Route("user/delete")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int userID)
        {
            var deleted = await _user_Services.Delete(userID);

            if (!deleted)
            {
                return NotFound(new { Message = "Xóa không thành công" });
            }
            else
            {
                return Ok(new { Message = "Xóa người dùng thành công" });
            }
        }

    }
}
