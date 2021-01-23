using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Models;
using System.Text.Json;

namespace TicketProcessor.Microservice.Consumers
{
    public class TicketConsumer : IConsumer<Ticket>
    {
        private readonly ILogger<TicketConsumer> _logger;

        public TicketConsumer(ILogger<TicketConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<Ticket> context)
        {
            var data = context.Message;
            _logger.LogInformation("Received" + JsonSerializer.Serialize(data));
            await Task.CompletedTask;
        }
    }
}