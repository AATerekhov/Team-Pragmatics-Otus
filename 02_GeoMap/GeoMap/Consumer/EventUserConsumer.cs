using AutoMapper;
using CommonNamespace;
using Domain.Entities;
using GeoMap.Model.User;
using MassTransit;
using Newtonsoft.Json;
using Services.Abstractions;
using Services.Contracts.User;

namespace GeoMap.Consumer
{
    public class EventUserConsumer(IUserService service, IMapper mapper) : IConsumer<MessageCreateUserDto>
    {
        public async Task Consume(ConsumeContext<MessageCreateUserDto> context)
        {
            
            CreatingUserModel? userCreate = JsonConvert.DeserializeObject<CreatingUserModel>(context.Message.Content!);

            var createUserTask = await service.GetByIdAsync(userCreate!.Id) switch
            {
                null => service.CreateAsync(mapper.Map<CreatingUserDto>(userCreate)),
                _ => Task.CompletedTask
            };

        }
    }
}
