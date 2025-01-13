using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestChatAPI.BLL.Interfaces;
using static TestChatAPI.Model.Posts_Model;

namespace TestChatAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Posts_Controller : ControllerBase
	{
		private readonly IPosts_Services _posts_Services;
		public Posts_Controller(IPosts_Services posts_Services)
		{
			_posts_Services = posts_Services;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllPostsAsync()
		{
			var Posts = await _posts_Services.GetAllPostAsync();

			if (Posts == null || !Posts.Any())
			{
				return NotFound("No post found.");
			}
			else
			{
				return Ok(Posts);
			}
		}
		[HttpPost]
		public async Task<IActionResult> AddPostAsync(Post_Add post)
		{
			var addedPost = await _posts_Services.AddPostAsync(post);

			if (addedPost == null)
			{
				return NotFound("No post found.");
			} 
			else
			{
				return Ok(addedPost);
			}
		}

		[HttpPut] 
		public async Task<IActionResult> UpdatePostAsync(Post_Update post)
		{
			var updatedPost = await _posts_Services.UpdatePostAsync(post);
			if (updatedPost == null)
			{
				return NotFound(new { Message = "Bài viết không tồn tại" });
			}
			else
			{
				return Ok(updatedPost);
			}
		}
		[HttpDelete]
		public async Task<IActionResult> DeletePostAsync(int postID)
		{
			var deletedPost = await _posts_Services.DeletePostAsync(postID);

			if (!deletedPost)
			{
				return NotFound(new { Message = "Bài viết không tồn tại" });
			}
			else
			{
				return Ok(new { Message = "Xóa bài viết thành công" });
			}
		}

	}
}
