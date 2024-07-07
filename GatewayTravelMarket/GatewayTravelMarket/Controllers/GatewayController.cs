using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using GatewayTravelMarket.Settings;

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
        private bool IsApiKeyValid(string apiKey)
        {
            // Check if the API key exists in the valid API keys HashSet
            return _validApiKeys.Contains(apiKey);
        }
        [HttpGet("plasetypes")]
        public async Task<IActionResult> GetPlaseTypes()
        {
            // Check for X-API-Key header
            if (!Request.Headers.TryGetValue("TM-API-Key", out var apiKey))
            {
                return BadRequest("TM-API-Key header is missing.");
            }

            // Validate the API key
            if (!IsApiKeyValid(apiKey))
            {
                return StatusCode(401, "Invalid API key.");
            }

            var productServiceClient = _httpClientFactory.CreateClient("PlaseTypesServiceClient");
            var response = await productServiceClient.GetAsync($"{productServiceClient.BaseAddress}api/PlaceType");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
