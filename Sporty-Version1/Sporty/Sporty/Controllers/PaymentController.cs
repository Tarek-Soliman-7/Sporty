using Microsoft.AspNetCore.Mvc;
using Sporty.Services.Interfaces;
using Sporty.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Sporty.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentGatewayService _paymentService;

        public PaymentController(IPaymentGatewayService paymentService)
        {
            _paymentService = paymentService;
        }

        // Endpoint to test SendPaymentAsync
        [HttpPost("send-payment")]
        public async Task<IActionResult> SendPayment()
        {
            var response = await _paymentService.SendPaymentAsync(Request);
            return Ok(response);
        }

        // Endpoint to test HandleCallbackAsync
        [HttpPost("callback")]
        public async Task<IActionResult> Callback()
        {
            bool result = await _paymentService.HandleCallbackAsync(Request);
            if (result) { return View("PaymentSuccessful"); }
            else { return View("PaymentFailed"); }
        }
    }



}
