using Microsoft.AspNetCore.Mvc;
using Sporty.ViewModels;
namespace Sporty.Services.Interfaces
{
    public interface IPaymentGatewayService
    {
        Task<PaymentResponse> SendPaymentAsync(HttpRequest request);
        Task<bool> HandleCallbackAsync(HttpRequest request);
    }
}
