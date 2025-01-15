using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestChatAPI.BLL;
using TestChatAPI.BLL.Interfaces;
using TestChatAPI.Model;
using static TestChatAPI.Model.Messages_Model;
using static TestChatAPI.Model.Posts_Model;

namespace TestChatAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Messages_Controller : ControllerBase
	{
		private readonly IMessages_Services _messages_Services;
		public Messages_Controller(IMessages_Services mess_Services)
		{
			_messages_Services = mess_Services;
		}
		[HttpPost("SendMessage")]
		public async Task<IActionResult> SendMessageAsync(Messages_Model.Message_Add message)
		{
			var sendMess = await _messages_Services.SendMessageAsync(message);

			if (sendMess == null)
			{
				return NotFound("Tin nhắn không tồn tại.");
			} 
			else
			{
				return Ok(sendMess);
			}
		}
		[HttpPut]
		public async Task<IActionResult> UpdateMessageAsync(Message_Update message)
		{
			var updatedMess = await _messages_Services.UpdateMessageAsync(message);
			if (updatedMess == null)
			{
				return NotFound(new { Message = "Tin nhắn không tồn tại" });
			}
			else
			{
				return Ok(updatedMess);
			}
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteMessagesAsync(int messID)
		{
			var deletedMess = await _messages_Services.DeleteMessagesAsync(messID);

			if (!deletedMess)
			{
				return NotFound(new { Message = "Tin nhắn không tồn tại" });
			}
			else
			{
				return Ok(new { Message = "Xóa tin nhắn thành công" });
			}
		}
		[HttpGet("GetMessageBoxChatAsync")]
		public async Task<IActionResult> GetMessageBoxChatAsync(int SenderID, int ReceiverID)
		{
			var messages = await _messages_Services.GetMessageBoxChatAsync(SenderID, ReceiverID);

			if (messages == null || !messages.Any())
			{
				return NotFound("Hãy trò chuyện đi nàoooo.");
			}
			else
			{
				return Ok(messages);
			}
		}
	}
}
