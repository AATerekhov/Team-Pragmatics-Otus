using AutoMapper;
using CommonNameSpace;
using MassTransit;
using Newtonsoft.Json;
using Services.Abstractions;
using Services.Contracts.User;

namespace WebApi.Consumers
{
    public class EventUserConsumer(IUserService service, IMapper mapper) : IConsumer<MessageCreateUserDto>
    {
        public async Task Consume(ConsumeContext<MessageCreateUserDto> context)
        {
            var newUser = JsonConvert.DeserializeObject<CreatingUserModel>(context.Message.Content!);
            var checkUserIsCreated = await service.GetByIdAsync(newUser!.Id);
            if (checkUserIsCreated == null)
            {
                newUser.Deleted = false;
                var createNewUser = await service.CreateAsync(mapper.Map<CreatingUserDto>(newUser));
            }
        }
    }
}