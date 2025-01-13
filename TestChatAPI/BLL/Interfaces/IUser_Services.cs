using static TestChatAPI.Model.User_Model;

namespace TestChatAPI.BLL.Interfaces
{
	public interface IUser_Services
	{
		Task<IEnumerable<User>> GetAllUserAsync();
	}
}
