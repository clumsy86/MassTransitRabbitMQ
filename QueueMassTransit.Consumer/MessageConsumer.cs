using MassTransit;
using Newtonsoft.Json;
using SharedMessage;

namespace QueueMassTransit.Consumer
{
    public class MessageConsumer : IConsumer<Message>
    {
        public Task Consume(ConsumeContext<Message> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"OrderCreated message: {jsonMessage}");
            return Task.CompletedTask;
        }
    }
}