using CommonNameSpace;
using MassTransit;

namespace Services.Implementations.Consumers
{
    public class EventConsumer : IConsumer<MessageDto>
    {
        public async Task Consume(ConsumeContext<MessageDto> context)
        {
            //throw new ArgumentException("some error");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("Value: {0}", context.Message.Content);
        }
    }
}
