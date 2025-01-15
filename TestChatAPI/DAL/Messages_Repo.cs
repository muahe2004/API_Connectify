using Microsoft.Data.SqlClient;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.Model;
using static TestChatAPI.Model.Messages_Model;
using static TestChatAPI.Model.Posts_Model;

namespace TestChatAPI.DAL
{
	public class Messages_Repo : IMessages_Repo
	{
		// Kết nối cơ sở dữ liệu
		private readonly Connect_SQL connect_SQL;
		public Messages_Repo(IConfiguration configuration)
		{
			var str_Con = configuration.GetConnectionString("Connectify");
			connect_SQL = new Connect_SQL(str_Con);
		}

		public async Task<bool> DeleteMessagesAsync(int messageID)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageID", messageID)
			};
			var result = await connect_SQL.ExecuteNonQueryAsync("sp_Delete_Message", parameters);
			if (result == 0)
			{
				return false;
			}
			return true;
		}

		public async Task<IEnumerable<Messages_Model.Message>> GetMessageBoxChatAsync(int SenderID, int ReceiverID)
		{
			var messages = new List<Message>();

			SqlParameter[] parameters = new SqlParameter[] 
			{
				new SqlParameter("@SenderID", SenderID),
				new SqlParameter("@ReceiverID", ReceiverID),
			};

			using (var reader = await connect_SQL.ExecuteReaderAsync("sp_Get_Mess_BoxChat", parameters))
			{
				while (await reader.ReadAsync())
				{
					var message = new Message
					{
						MessageID = (int)reader["MessageID"],
						SenderId = (int)reader["SenderId"],
						ReceiverID = (int)reader["ReceiverID"],
						Content = reader["Content"].ToString()!,
						SentAt = (DateTime)reader["SentAt"], 
						IsRead = (bool)reader["IsRead"],
					};
					messages.Add(message);
				}
			}
			return messages;
		}

		public async Task<Messages_Model.Message_Add> SendMessageAsync(Messages_Model.Message_Add message)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@SenderID", message.SenderID),
				new SqlParameter("@ReceiverID", message.ReceiverID),
				new SqlParameter("@Content", message.Content)
			};
			await connect_SQL.ExecuteNonQueryAsync("sp_Send_Message", parameters);
			return message;
		}

		public async Task<Messages_Model.Message_Update> UpdateMessageAsync(Messages_Model.Message_Update message)
		{
			SqlParameter[] parameters = new SqlParameter[]
			{
				new SqlParameter("@MessageID", message.MessageID),
				new SqlParameter("@Content", message.Content)
			};
			var result = await connect_SQL.ExecuteNonQueryAsync("sp_Update_Message", parameters);

			if (result == 0)
			{
				return null;
			}

			return message;
		}
	}
}
