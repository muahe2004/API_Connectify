using static TestChatAPI.Model.Posts_Model;

namespace TestChatAPI.BLL.Interfaces
{
	public interface IPosts_Services
	{
		Task<IEnumerable<Post>> GetAllPostAsync();
		Task<PostWithImageResponse> AddPostWithImageAsync(Post_Add post, PostImage postImage);

        Task<Post_Update> UpdatePostAsync(Post_Update post);
		Task<bool> DeletePostAsync(int postID);
	}
}
