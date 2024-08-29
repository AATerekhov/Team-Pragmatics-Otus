using CommonNamespace;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Comsumers
{
    public class EventConsumer : IConsumer<MessageCreateUserDto>
    {
        public async Task Consume(ConsumeContext<MessageCreateUserDto> context)
        {
            var test = context.Message.Content;
        }
    }
}
