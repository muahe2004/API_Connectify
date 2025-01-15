using static TestChatAPI.Model.Messages_Model;

namespace TestChatAPI.BLL.Interfaces
{
	public interface IMessages_Services
	{
		Task<Message_Add> SendMessageAsync(Message_Add message);
		Task<Message_Update> UpdateMessageAsync(Message_Update message);
		Task<bool> DeleteMessagesAsync(int messageID);
		Task<IEnumerable<Message>> GetMessageBoxChatAsync(int SenderID, int ReceiverID);
	}
}
