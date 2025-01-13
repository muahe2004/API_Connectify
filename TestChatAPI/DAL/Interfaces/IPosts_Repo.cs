using static TestChatAPI.Model.Posts_Model;

namespace TestChatAPI.DAL.Interfaces
{
	public interface IPosts_Repo
	{
		Task<IEnumerable<Post>> GetAllPostAsync();
		Task<Post_Add> AddPostAsync(Post_Add post);
		Task<Post_Update> UpdatePostAsync(Post_Update post);
		Task<bool> DeletePostAsync(int postID);
	}
}
