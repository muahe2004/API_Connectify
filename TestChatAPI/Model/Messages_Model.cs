namespace TestChatAPI.Model
{
	public class Messages_Model
	{
		public class Message
		{
			public int MessageID { get; set; }
			public int SenderId { get; set; }
			public int ReceiverID { get; set; }
			public string Content { get; set; }
			public DateTime SentAt { get; set; }
			public bool IsRead { get; set; }
		}

		public class Message_Add
		{
			public int SenderID { get; set; }
			public int ReceiverID { get; set; }
			public string Content { get; set; }
		}
		
		public class Message_Update
		{
			public int MessageID { get; set; }
			public string Content { get; set; }
		}
	}
}
