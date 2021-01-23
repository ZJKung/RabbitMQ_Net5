using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Shared.Models;

namespace rabbitmqdotnet.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly ILogger<TicketController> _logger;

        public TicketController(IBus bus, ILogger<TicketController> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket != null)
            {
                ticket.BookedOn = DateTime.Now;
                Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(ticket);
                _logger.LogInformation("Send" + JsonSerializer.Serialize(ticket));
                return Ok();
            }
            return BadRequest();
        }

    }
}
