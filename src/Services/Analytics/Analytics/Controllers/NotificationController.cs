using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analytics.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Analytics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private IHubContext<MessageHub> _messageHubContext;
        public NotificationController(IHubContext<MessageHub> messageHubContext)
        {
            _messageHubContext = messageHubContext;
        }

        public IActionResult Post()
        {
            _messageHubContext.Clients.All.SendAsync("send", "Hello from the server!");
            return Ok();
        }
    }
}
