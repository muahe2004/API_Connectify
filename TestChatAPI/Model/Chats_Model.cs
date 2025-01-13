namespace TestChatAPI.Model
{
	public class Chats_Model
	{
		public class Chat
		{
			public int ChatID { get; set; }
			public int User1ID { get; set; }
			public int User2ID { get; set; }
			public DateTime CreatedAt { get; set; }
		}
	}
}
