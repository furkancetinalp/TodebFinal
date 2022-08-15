using API.Configuration.Filters.Auth;
using Business.Abstract;
using DTO.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _service;

        public MessageController(IMessageService service)
        {
            _service = service;
        }
        //Sending message to admin
        [HttpPost]
        public IActionResult SendMeesageToAdmin(CreateMessageRequest request)
        {
            var response = _service.Create(request);
            return Ok(response);

        }
        //Getting all messages -- can only be reached by admin
        [HttpGet]
        [Permission(Permission.GetAllMessages)]
        public IActionResult AllMessages()
        {
            var response = _service.GetAll();
            return Ok(response);
        }
        //Setting messages as Read -- can be done only by admin
        [HttpGet("MarkMessagesAsRead")]
        [Permission(Permission.MarkMessagesAsRead)]
        public IActionResult MarkMessagesAsRead()
        {
            var response = _service.MarkMessagesAsRead();
            return Ok(response);
        }
        //Getting unread messages
        [HttpGet("UnreadMessages")]
        [Permission(Permission.GetUnreadMessages)]
        public IActionResult GetUnreadMessages()
        {
            var response = _service.UnreadMessages();
            return Ok(response);

        }
    }
}
