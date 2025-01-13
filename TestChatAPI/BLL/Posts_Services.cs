using TestChatAPI.BLL.Interfaces;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.Model;

namespace TestChatAPI.BLL
{
	public class Posts_Services : IPosts_Services
	{
		private readonly IPosts_Repo _postRepo;
		public Posts_Services(IPosts_Repo postsRepo)
		{
			_postRepo = postsRepo;
		}

		public async Task<Posts_Model.Post_Add> AddPostAsync(Posts_Model.Post_Add post)
		{
			return await _postRepo.AddPostAsync(post);
		}

		public async Task<bool> DeletePostAsync(int postID)
		{
			return await _postRepo.DeletePostAsync(postID);
		}
		public async Task<IEnumerable<Posts_Model.Post>> GetAllPostAsync()
		{
			return await _postRepo.GetAllPostAsync();
		}

		public async Task<Posts_Model.Post_Update> UpdatePostAsync(Posts_Model.Post_Update post)
		{
			return await _postRepo.UpdatePostAsync(post);
		}
	}
}
