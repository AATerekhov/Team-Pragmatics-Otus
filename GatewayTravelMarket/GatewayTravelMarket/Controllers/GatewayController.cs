using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using GatewayTravelMarket.Settings;
using System.Text.Json;
using System.Security.AccessControl;

namespace GatewayTravelMarket.Controllers
{
    [ApiController]
    [Route("api")]
    public class GatewayController:ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApiGatewaySettings _gatewaySettings;
        // Dictionary to store the client API keys
        private readonly HashSet<string> _validApiKeys;
        public GatewayController(IHttpClientFactory httpClientFactory, 
            IOptions<ApiGatewaySettings> gatewaySettings)
        {
            _httpClientFactory = httpClientFactory;
            _gatewaySettings = gatewaySettings.Value;
            // Parse the comma-separated valid API keys from the settings and store them in a HashSet
            _validApiKeys = new HashSet<string>(_gatewaySettings.ValidApiKeys.Split(',').Select(apiKey => apiKey.Trim()));
        }

        [HttpGet("placetypes")]
        public async Task<IActionResult> GetPlaceTypes()
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.GetAsync("/api/PlaceType");
            return await CheckResponse(response);
        }

        [HttpGet("placetype/{id:int}")]
        public async Task<IActionResult> GetPlaceType(int id)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.GetAsync($"/api/PlaceType/{id}");
            return await CheckResponse(response);
        }

        [HttpPost("placetype")]
        public async Task<IActionResult> CreatePlaceType([FromBody] JsonElement json)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.PostAsync($"/api/PlaceType", JsonContent.Create(json));
            return await CheckResponse(response);
        }

        [HttpPut("placetype/{id:int}")]
        public async Task<IActionResult> UpdatePlaceType(int id,[FromBody] JsonElement json)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.PutAsync($"/api/PlaceType/{id}", JsonContent.Create(json));
            return await CheckResponse(response);
        }

        [HttpDelete("placetype/{id:int}")]
        public async Task<IActionResult> DeletePlaceType(int id)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.DeleteAsync($"/api/PlaceType/{id}");
            return await CheckResponse(response);
        }

        [HttpGet("places/{typeid:int}")]
        public async Task<IActionResult> GetPlaces(int typeid) 
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.GetAsync($"/api/Place/{typeid}");
            return await CheckResponse(response);
        }
        [HttpGet("place/{id:Guid}")]
        public async Task<IActionResult> GetPlace(Guid id)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.GetAsync($"/api/Place/{id}");
            return await CheckResponse(response);
        }

        [HttpPost("place/tracing/{placeTypeId:int}")]
        public async Task<IActionResult> TracingRoute(int typeid,[FromBody] JsonElement json)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.PostAsync($"/api/Place/Tracing/{typeid}", JsonContent.Create(json));
            return await CheckResponse(response);
        }

        [HttpPost("place")]
        public async Task<IActionResult> CreatePlace([FromBody] JsonElement json)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.PostAsync($"/api/Place", JsonContent.Create(json));
            return await CheckResponse(response);
        }

        [HttpPut("place/{id:Guid}")]
        public async Task<IActionResult> UpdatePlace(Guid id, [FromBody] JsonElement json)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.PutAsync($"/api/Place/{id}", JsonContent.Create(json));
            return await CheckResponse(response);
        }

        [HttpDelete("place/{id:Guid}")]
        public async Task<IActionResult> DeletePlace(Guid id)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("GeoMapServiceClient");
            var response = await productServiceClient.DeleteAsync($"/api/Place/{id}");
            return await CheckResponse(response);
        }

        [HttpGet("travels")]
        public async Task<IActionResult> GetTravels()
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("TravelServiceClient");
            var response = await productServiceClient.GetAsync("/Travel");
            return await CheckResponse(response);
        }

        [HttpGet("travel/{id:int}")]
        public async Task<IActionResult> GetTravel(int id)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("TravelServiceClient");
            var response = await productServiceClient.GetAsync($"/Travel/{id}");
            return await CheckResponse(response);
        }

        [HttpPost("travel")]
        public async Task<IActionResult> CreateTravel([FromBody] JsonElement json)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("TravelServiceClient");
            var response = await productServiceClient.PostAsync("/Travel", JsonContent.Create(json));
            return await CheckResponse(response);
        }

        [HttpPut("travel/{id:int}")]
        public async Task<IActionResult> UpdateTravel(int id, [FromBody] JsonElement json)
        {
            if (!CheckKey(out IActionResult actionResult)) return actionResult;
            var productServiceClient = _httpClientFactory.CreateClient("TravelServiceClient");
            var response = await productServiceClient.PutAsync($"/Travel/{id}", JsonContent.Create(json));
            return await CheckResponse(response);
        }

        private bool IsApiKeyValid(string apiKey)
        {
            // Check if the API key exists in the valid API keys HashSet
            return _validApiKeys.Contains(apiKey);
        }
        private bool CheckKey(out IActionResult actionResult)
        {
            // Check for X-API-Key header
            if (!Request.Headers.TryGetValue("TM-API-Key", out var apiKey)) { actionResult = BadRequest("TM-API-Key header is missing."); return false; }
            // Validate the API key
            if (!IsApiKeyValid(apiKey)) { actionResult = actionResult = StatusCode(401, "Invalid API key."); return false; }
            actionResult = StatusCode(200, "API key is valid");
            return true;
        }
        private async Task<IActionResult> CheckResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
