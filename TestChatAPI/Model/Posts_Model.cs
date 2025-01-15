using System.ComponentModel.DataAnnotations;

namespace TestChatAPI.Model
{
	public class Posts_Model
	{
		public class Post
		{
			public int PostID { get; set; }
			public int UserID { get; set; }
			public string Content { get; set; }
			public DateTime CreatedAt { get; set; }
		}

		public class Post_Add
		{
			public int UserID { get; set; }

			[StringLength(500, ErrorMessage = "Content không được vượt quá 500 ký tự.")]
			public string Content { get; set; }
		}
		public class Post_Update
		{
			public int PostID { get; set; }

			[StringLength(500, ErrorMessage = "Content không được vượt quá 500 ký tự.")]
			public string Content { get; set; }
		}
		public class PostImage
		{
			public int PostID { get; set; }
			public string ImageURL { get; set; }
		}

        public class PostWithImageResponse
        {
            public Post_Add Post { get; set; }
            public PostImage PostImage { get; set; }
        }

    }
}
