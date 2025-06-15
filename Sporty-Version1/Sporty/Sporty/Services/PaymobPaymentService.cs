using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Sporty.Services;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Sporty.Services.Interfaces;
using Sporty.ViewModels;

namespace Sporty.Services
{
    public class PaymobPaymentService : BasePaymentService, IPaymentGatewayService
    {
        private readonly string _apiKey;
        private readonly int[] _integrationIds;
        private string _authToken;

        public PaymobPaymentService(HttpClient client, IConfiguration config)
            : base(client, config["Paymob:BaseUrl"])
        {
            _apiKey = config["Paymob:ApiKey"];
            _integrationIds = new[] {5090494};
        }

        private async Task<string> GenerateTokenAsync()
        {
            var result = await BuildRequestAsync(HttpMethod.Post, "/api/auth/tokens", new { api_key = _apiKey });

            if (result.Success && result.Data is JsonElement json&& json.TryGetProperty("token", out var token))
            {
                    return token.GetString();
               
            }

            throw new Exception("Unable to generate Paymob token.");
        }

        public async Task<PaymentResponse> SendPaymentAsync(HttpRequest request)
        {
            _authToken = await GenerateTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);

            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();
            var requestData = JsonSerializer.Deserialize<Dictionary<string, object>>(body);

            requestData["auth_token"] = _authToken;
            requestData["api_source"] = "INVOICE";
            requestData["integrations"] = _integrationIds;

            var response = await BuildRequestAsync(HttpMethod.Post, "/api/ecommerce/orders", requestData);

            if (response.Success && response.Data is JsonElement json && json.TryGetProperty("url", out var url))
            {
                return new PaymentResponse { Success = true, Url = url.GetString() };
            }

            return new PaymentResponse { Success = false, Url = "/payment/failed" };
        }

        public async Task<bool> HandleCallbackAsync(HttpRequest request)
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();

            //var path = Path.Combine(Directory.GetCurrentDirectory(), "paymob_response.json");
            //await File.WriteAllTextAsync(path, body);

            var document = JsonDocument.Parse(body);
            //return response != null &&
            //       response.TryGetValue("success", out var successVal) &&
            //       successVal?.ToString()?.ToLower() == "true";
           if (document.RootElement.TryGetProperty("obj", out var objElement) &&
    objElement.TryGetProperty("success", out var successProp))
            {
                return successProp.GetBoolean(); // returns true or false
            }

           return false;
        }
    }
}
