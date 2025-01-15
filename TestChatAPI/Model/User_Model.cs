namespace TestChatAPI.Model
{
	public class User_Model
	{
		public class User {
			public int UserID { get; set; }
			public string Username { get; set; }
			public string PasswordHash { get; set; }
			public string Email { get; set; }
			public string FullName { get; set; }
			public string Bio { get; set; }
			public string ProfilePictureURL { get; set; }
			public DateTime CreatedAt { get; set; }
		}

		public class Login
		{
			public string Username { get; set; }
			public string PasswordHash { get; set; }
		}

        public class Follow
        {
            public string ProfilePictureURL { get; set; }
            public string Username { get; set; }
            public string FullName { get; set; }

        }
    }
}


