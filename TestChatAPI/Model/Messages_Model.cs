namespace TestChatAPI.Model
{
	public class Messages_Model
	{
		public class Message
		{
			public int MessageId { get; set; }
			public int ChatID { get; set; }
			public int SenderId { get; set; }
			public string Content { get; set; }
			public DateTime Timestamp { get; set; }
			public bool IsRead { get; set; }
		}
	}
}
