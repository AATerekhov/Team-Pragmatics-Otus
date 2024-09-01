using AutoMapper;
using CommonNameSpace;
//using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using MassTransit;
using Newtonsoft.Json;
using Services.Abstractions;
using Services.Repositories.Abstractions;
using System.Net.Http.Json;
using System;
using System.Security.AccessControl;
using Services.Contracts.User;
using System.Net;

namespace Services.Implementations.Consumers
{
    public class EventConsumer : IConsumer<MessageCreateUserDto>
    {
        private readonly HttpClient _travelClient;

        public async Task Consume(ConsumeContext<MessageCreateUserDto> context)
        {
            //throw new ArgumentException("some error");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine("Consume value: {0}", context.Message.Content);
            var newUser = GetUserFromJSON(context.Message.Content);
            var checkUserIsNotCreated = await CheckUserIsNotCreated(newUser.Id.ToString());
            if (context.Message.Content != null && checkUserIsNotCreated)
            {
                newUser.Deleted = false;
                var response = await CreateUserFromBus(newUser);
            }
        }

        public EventConsumer()
        {
            _travelClient = new HttpClient()
            {
                BaseAddress = new Uri("http://travel-card-service:8080")
            };
        }

        public static UserDto GetUserFromJSON(string ObjValue)
        {
            return JsonConvert.DeserializeObject<UserDto>(ObjValue);
        }

        public async Task<UserDto> CreateUserFromBus(UserDto user)
        {
            JsonContent contentUser = JsonContent.Create(user);
            var response = await _travelClient.PostAsync("/User", contentUser);
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<bool> CheckUserIsNotCreated(string guid)
        {
            var response = await _travelClient.GetAsync($"/User/{guid}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }
}
