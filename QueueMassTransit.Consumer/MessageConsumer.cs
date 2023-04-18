using MassTransit;
using Newtonsoft.Json;
using SharedMessage;

namespace QueueMassTransit.Consumer
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly ILogger<MessageConsumer> _logger;

        public MessageConsumer(ILogger<MessageConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation("Got message");
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"OrderCreated message: {jsonMessage}");
            return Task.FromResult(jsonMessage);               
        }
    }
}