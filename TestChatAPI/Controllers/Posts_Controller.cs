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
        // Phương thức để thêm bài viết và hình ảnh
        [HttpPost("add-post-with-image")]
        public async Task<IActionResult> AddPostWithImage([FromForm] Post_Add postRequest, IFormFile imageFile)
        {
            if (postRequest == null || string.IsNullOrEmpty(postRequest.Content))
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Cần phải chọn một hình ảnh.");
            }

            // Lưu hình ảnh vào thư mục trên server và lấy URL của hình ảnh
            var imageURL = await SaveImageAsync(imageFile);

            // Tạo đối tượng Post_Add và PostImage từ dữ liệu nhận được
            var post = new Post_Add
            {
                UserID = postRequest.UserID,
                Content = postRequest.Content
            };

            var postImage = new PostImage
            {
                ImageURL = imageURL
            };

            // Gọi phương thức thêm bài viết và hình ảnh
            var addedPost = await _posts_Services.AddPostWithImageAsync(post, postImage);

            // Trả về kết quả thêm bài viết
            //return Ok(addedPost);
            // Trả về đối tượng PostWithImageResponse
            return Ok(new { Post = addedPost, PostImage = postImage });
        }

        // Lưu hình ảnh lên server và trả về đường dẫn URL của hình ảnh
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Trả về URL của hình ảnh
            return $"http://localhost:44328/images/{imageFile.FileName}";
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
