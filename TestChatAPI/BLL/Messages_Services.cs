using TestChatAPI.BLL.Interfaces;
using TestChatAPI.DAL.Interfaces;
using TestChatAPI.Model;

namespace TestChatAPI.BLL
{
	public class Messages_Services : IMessages_Services
	{
		private readonly IMessages_Repo _messRepo;
		public Messages_Services(IMessages_Repo messsRepo)
		{
			_messRepo = messsRepo;
		}

		public async Task<bool> DeleteMessagesAsync(int messageID)
		{
			return await _messRepo.DeleteMessagesAsync(messageID);
		}

		public async Task<IEnumerable<Messages_Model.Message>> GetMessageBoxChatAsync(int SenderID, int ReceiverID)
		{
			return await _messRepo.GetMessageBoxChatAsync(SenderID, ReceiverID);
		}

		public async Task<Messages_Model.Message_Add> SendMessageAsync(Messages_Model.Message_Add message)
		{
			return await _messRepo.SendMessageAsync(message);	
		}

		public Task<Messages_Model.Message_Update> UpdateMessageAsync(Messages_Model.Message_Update message)
		{
			return _messRepo.UpdateMessageAsync(message);
		}
	}
}
