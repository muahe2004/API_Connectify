using Microsoft.AspNetCore.Identity;
using TestChatAPI.BLL.Interfaces;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.Model;
using static TestChatAPI.Model.User_Model;

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

		public async Task<User_Model.User> GetUserByUserName(string userName)
		{
			return await _userRepo.GetUserByUserName(userName);
		}


		public async Task<User_Model.Login> Login(User_Model.Login login)
		{
			return await _userRepo.Login(login);
		}
        public async Task<IEnumerable<Follow>> Follower(int userID)
        {
            return await _userRepo.Follower(userID);
        }
        public async Task<IEnumerable<Follow>> Following(int userID)
        {
            return await _userRepo.Following(userID);
        }
        public async Task<IEnumerable<Follow>> Friendship(int userID)
        {
            return await _userRepo.Friendship(userID);
        }
        public async Task<User> Create(User user)
        {
            return await _userRepo.Create(user);
        }

        public async Task<User> Update(User user)
        {
            return await _userRepo.Update(user);
        }

        public async Task<bool> Delete(int userID)
        {
            return await _userRepo.Delete(userID);
        }
    }
}
