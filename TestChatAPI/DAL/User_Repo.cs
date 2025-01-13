using Microsoft.Data.SqlClient;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.Model;
using static TestChatAPI.Model.User_Model;

namespace TestChatAPI.DAL
{
	public class User_Repo : IUser_Repo
	{
		// Kết nối cơ sở dữ liệu
		private readonly Connect_SQL connect_SQL;
		public User_Repo(IConfiguration configuration)
		{
			var str_Con = configuration.GetConnectionString("TestChat");
			connect_SQL = new Connect_SQL(str_Con);
		}

		public async Task<IEnumerable<User>> GetAllUserAsync()
		{
			var users = new List<User>();

			SqlParameter[] parameters = new SqlParameter[] { };

			using (var reader = await connect_SQL.ExecuteReaderAsync("sp_Get_All_User", parameters))
			{
				while (await reader.ReadAsync())
				{
					var user = new User
					{
						UserId = (int)reader["UserId"],
						UserName = reader["UserName"].ToString()!,
						Avatar = reader["Avatar"].ToString()!,
					};
					users.Add(user);
				}
			}
			return users;
		}
	}
}
