using Microsoft.AspNetCore.Mvc;

namespace TelegramBot.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMessage()
        {
            return Ok("This is a message from the MessageController.");
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] string message)
        {
            // Logic to send the message
            return Ok($"Message '{message}' sent successfully.");
        }
    }
}
