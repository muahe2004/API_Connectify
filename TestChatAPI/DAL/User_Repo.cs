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
			var str_Con = configuration.GetConnectionString("Connectify");
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
						UserID = (int)reader["UserID"],
						Username = reader["Username"].ToString()!,
						PasswordHash = reader["PasswordHash"].ToString()!,
						Email = reader["Email"].ToString()!,
						FullName = reader["FullName"].ToString()!,
						Bio = reader["Bio"].ToString()!,
						ProfilePictureURL = reader["ProfilePictureURL"].ToString()!,
						CreatedAt = (DateTime)reader["CreatedAt"]

					};
					users.Add(user);
				}
			}
			return users;
		}

		public async Task<User> GetUserByUserName(string userName)
		{
			User user = null;

			// Định nghĩa tham số truy vấn
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Username", userName)
			};

			// Kết nối với cơ sở dữ liệu và thực hiện truy vấn
			using (var reader = await connect_SQL.ExecuteReaderAsync("sp_Get_User_By_UserName", parameters))
			{
				if (await reader.ReadAsync())  // Chỉ cần lấy một người dùng nếu tìm thấy
				{
					user = new User
					{
						UserID = (int)reader["UserID"],
						Username = reader["Username"].ToString()!,
						PasswordHash = reader["PasswordHash"].ToString()!,
						Email = reader["Email"].ToString()!,
						FullName = reader["FullName"].ToString()!,
						Bio = reader["Bio"].ToString()!,
						ProfilePictureURL = reader["ProfilePictureURL"].ToString()!,
						CreatedAt = (DateTime)reader["CreatedAt"]
					};
				}
			}

			return user;  // Trả về đối tượng user nếu tìm thấy hoặc null nếu không
		}


		public async Task<Login> Login(Login loginInput)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@Username", loginInput.Username),
				new SqlParameter("@PasswordHash", loginInput.PasswordHash)
			};

			Login loginResult = null;

			using (var reader = await connect_SQL.ExecuteReaderAsync("sp_Login", parameters))
			{
				if (await reader.ReadAsync())
				{
					loginResult = new Login
					{
						Username = reader["Username"]?.ToString(),
						PasswordHash = reader["PasswordHash"]?.ToString()
					};
				}
			}

			return loginResult;
		}


        public async Task<IEnumerable<Follow>> Follower(int userID)
        {
            var users = new List<Follow>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userID", userID)
            };

            using (var reader = await connect_SQL.ExecuteReaderAsync("sp_user_follower", parameters))
            {
                while (await reader.ReadAsync())
                {
                    var user = new Follow
                    {
                        ProfilePictureURL = reader["ProfilePictureURL"].ToString()!,
                        Username = reader["Username"].ToString()!,
                        FullName = reader["FullName"].ToString()!,
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public async Task<IEnumerable<Follow>> Following(int userID)
        {
            var users = new List<Follow>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userID", userID)
            };

            using (var reader = await connect_SQL.ExecuteReaderAsync("sp_user_following", parameters))
            {
                while (await reader.ReadAsync())
                {
                    var user = new Follow
                    {
                        ProfilePictureURL = reader["ProfilePictureURL"].ToString()!,
                        Username = reader["Username"].ToString()!,
                        FullName = reader["FullName"].ToString()!,
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public async Task<IEnumerable<Follow>> Friendship(int userID)
        {
            var users = new List<Follow>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userID", userID)
            };

            using (var reader = await connect_SQL.ExecuteReaderAsync("sp_friendship", parameters))
            {
                while (await reader.ReadAsync())
                {
                    var user = new Follow
                    {
                        ProfilePictureURL = reader["ProfilePictureURL"].ToString()!,
                        Username = reader["Username"].ToString()!,
                        FullName = reader["FullName"].ToString()!,
                    };
                    users.Add(user);
                }
            }
            return users;
        }

        public async Task<User> Create(User user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@Bio", user.Bio),
                new SqlParameter("@ProfilePictureURL", user.ProfilePictureURL),
            };
            await connect_SQL.ExecuteNonQueryAsync("sp_user_create", parameters);
            return user;
        }

        public async Task<User> Update(User user)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@PasswordHash", user.PasswordHash),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@FullName", user.FullName),
                new SqlParameter("@Bio", user.Bio),
                new SqlParameter("@ProfilePictureURL", user.ProfilePictureURL),
            };
            var result = await connect_SQL.ExecuteNonQueryAsync("sp_user_update", parameters);

            if (result == 0)
            {
                return null;
            }

            return user;
        }

        public async Task<bool> Delete(int userID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userID", userID)
            };
            var result = await connect_SQL.ExecuteNonQueryAsync("sp_user_delete", parameters);
            if (result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
