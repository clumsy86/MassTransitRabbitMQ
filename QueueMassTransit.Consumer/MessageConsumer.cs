using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SharedMessage;

namespace QueueMassTransit.Consumer
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly ILogger<MessageConsumer> _logger;
        private readonly IHubContext<Hubs> _hubContext;

        public MessageConsumer(ILogger<MessageConsumer> logger, IHubContext<Hubs> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation("Receive message");
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", context.Message);
            Console.WriteLine($"OrderCreated message: {jsonMessage}");
        }
    }
}