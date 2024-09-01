using CommonNamespace;
using GeoMap.HttpClients;
using GeoMap.Model.User;
using MassTransit;
using Newtonsoft.Json;

namespace GeoMap.Consumer
{
    public class EventUserConsumer : IConsumer<MessageCreateUserDto>
    {
        public Task Consume(ConsumeContext<MessageCreateUserDto> context)
        {
            CreatingUserModel? userCreate = JsonConvert.DeserializeObject<CreatingUserModel>(context.Message.Content);
            var _client = new SimpleHttpClient("http://geomap-service:8080");
            if (userCreate != null)
            {
                var user = _client.GetAsync<UserModel>($"/api/User/{userCreate.Id}").Result;
                if(user.Name == null)
                {
                    Task.WaitAll(_client.PostAsyncNotResult<CreatingUserModel>("/api/User", userCreate));
                }               
            }
            return Task.CompletedTask;
        }
    }
}
