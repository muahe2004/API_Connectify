using static TestChatAPI.Model.Posts_Model;

namespace TestChatAPI.DAL.Interfaces
{
	public interface IPosts_Repo
	{
		Task<IEnumerable<Post>> GetAllPostAsync();
		Task<PostWithImageResponse> AddPostWithImageAsync(Post_Add post, PostImage postImage);

        Task<Post_Update> UpdatePostAsync(Post_Update post);
		Task<bool> DeletePostAsync(int postID);
	}
}
