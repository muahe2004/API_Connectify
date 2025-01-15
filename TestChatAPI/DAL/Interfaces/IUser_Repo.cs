using static TestChatAPI.Model.User_Model;

namespace TestChatAPI.DAL.Interfaces
{
	public interface IUser_Repo
	{
		Task<IEnumerable<User>> GetAllUserAsync();
		Task<User> GetUserByUserName(string userName);
		Task<Login> Login(Login login);
        Task<IEnumerable<Follow>> Follower(int userID);
        Task<IEnumerable<Follow>> Following(int userID);
        Task<IEnumerable<Follow>> Friendship(int userID);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<bool> Delete(int userID);
    }
}
