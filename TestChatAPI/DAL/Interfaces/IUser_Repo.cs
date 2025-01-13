using static TestChatAPI.Model.User_Model;

namespace TestChatAPI.DAL.Interfaces
{
	public interface IUser_Repo
	{
		Task<IEnumerable<User>> GetAllUserAsync();
	}
}
