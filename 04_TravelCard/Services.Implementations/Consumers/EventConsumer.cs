using CommonNameSpace;
using MassTransit;

namespace Services.Implementations.Consumers
{
    public class EventConsumer : IConsumer<MessageCreateUserDto>
    {
        public async Task Consume(ConsumeContext<MessageCreateUserDto> context)
        {
            //throw new ArgumentException("some error");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("Consume value: {0}", context.Message.Content);
        }
    }
}
