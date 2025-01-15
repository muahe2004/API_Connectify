using Microsoft.Data.SqlClient;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.Model;
using static TestChatAPI.Model.Posts_Model;

namespace TestChatAPI.DAL
{
	public class Posts_Repo : IPosts_Repo
	{
		// Kết nối cơ sở dữ liệu
		private readonly Connect_SQL connect_SQL;
		public Posts_Repo(IConfiguration configuration)
		{
			var str_Con = configuration.GetConnectionString("Connectify");
			connect_SQL = new Connect_SQL(str_Con);
		}

        public async Task<PostWithImageResponse> AddPostWithImageAsync(Post_Add post, PostImage postImage)
        {
            // Chuẩn bị tham số cho stored procedure
            SqlParameter[] parameters = new SqlParameter[]
            {
				new SqlParameter("@UserID", post.UserID),
				new SqlParameter("@Content", post.Content),
				new SqlParameter("@ImageURL", postImage.ImageURL ?? (object)DBNull.Value) // Đảm bảo truyền giá trị null nếu không có URL
            };

            // Thực thi stored procedure
            await connect_SQL.ExecuteNonQueryAsync("sp_AddPostWithImage", parameters);

            // Trả về đối tượng bài viết với ảnh đã được thêm
            return new PostWithImageResponse
            {
                Post = post,
                PostImage = postImage
            };
        }


        public async Task<bool> DeletePostAsync(int postID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PostID", postID)
			};
			var result = await connect_SQL.ExecuteNonQueryAsync("sp_Delete_Post", parameters);
			if (result == 0)
			{
				return false;
			}
			return true;
		}

		public async Task<IEnumerable<Posts_Model.Post>> GetAllPostAsync()
		{
			var posts = new List<Post>();

			SqlParameter[] parameters = new SqlParameter[] { };

			using (var reader = await connect_SQL.ExecuteReaderAsync("sp_Get_All_Post", parameters))
			{
				while (await reader.ReadAsync())
				{
					var post = new Post
					{
						PostID = (int)reader["PostID"],
						UserID = (int)reader["UserID"],
						Content = reader["Content"].ToString()!,
						CreatedAt = (DateTime)reader["CreatedAt"]
					};
					posts.Add(post);
				}
			}
			return posts;
		}

		public async Task<Post_Update> UpdatePostAsync(Post_Update post)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@PostID", post.PostID),
				new SqlParameter("@Content", post.Content)
			};
			var result = await connect_SQL.ExecuteNonQueryAsync("sp_Update_Post", parameters);

			if (result == 0)
			{
				return null; 
			}

			return post;
		}
	}
}
