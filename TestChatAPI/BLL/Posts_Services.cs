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

		public async Task<Posts_Model.PostWithImageResponse> AddPostWithImageAsync(Posts_Model.Post_Add post, Posts_Model.PostImage postImage)
		{
			return await _postRepo.AddPostWithImageAsync(post, postImage);
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
