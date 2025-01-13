using TestChatAPI.BLL.Interfaces;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.Model;

namespace TestChatAPI.BLL
{
	public class User_Services : IUser_Services
	{
		private readonly IUser_Repo _userRepo;
		public User_Services(IUser_Repo userRepo)
		{
			_userRepo = userRepo;
		}
		public async Task<IEnumerable<User_Model.User>> GetAllUserAsync()
		{
			return await _userRepo.GetAllUserAsync();
		}
	}
}
