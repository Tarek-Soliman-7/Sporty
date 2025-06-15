using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sporty.Services
{
    public abstract class BasePaymentService
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseUrl;

        protected BasePaymentService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl.TrimEnd('/');

            // Optional default header setup, can be extended by subclasses
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async Task<(bool Success, int Status, object? Data, string? ErrorMessage)> BuildRequestAsync(
            HttpMethod method, string url, object? data = null, string type = "json")
        {
            try
            {
                var request = new HttpRequestMessage(method, $"{_baseUrl}/{url.TrimStart('/')}");

                if (data != null)
                {
                    if (type == "json")
                    {
                        var jsonData = JsonSerializer.Serialize(data);
                        request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    }
                    else if (type == "form_params")
                    {
                        // convert data to form-urlencoded (if needed)
                        if (data is Dictionary<string, string> formDict)
                        {
                            request.Content = new FormUrlEncodedContent(formDict);
                        }
                        else
                        {
                            throw new ArgumentException("Form data must be Dictionary<string, string>");
                        }
                    }
                }

                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                object? responseData;
                try
                {
                    responseData = JsonSerializer.Deserialize<object>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch
                {
                    responseData = content;
                }

                return (response.IsSuccessStatusCode, (int)response.StatusCode, responseData, null);
            }
            catch (Exception ex)
            {
                return (false, 500, null, ex.Message);
            }
        }
    }
}
