using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentGatewayAPI.Models.DTO;
using PaymentGatewayAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRequestService _paymentRequestService;

        public PaymentController(IPaymentRequestService paymentRequestService)
        {
            _paymentRequestService = paymentRequestService;
        }

        [HttpGet]
        public string Get()
        {
            return "Payment Gateway is Up & running";
        }

        [HttpPost]
        public async Task<IActionResult> Post(PaymentRequestDTO paymentRequestDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var paymentState = await _paymentRequestService.Paymnt(paymentRequestDTO);
                    var paymentResponse = new PaymentResponseDTO()
                    {
                        IsPaymentProcessed = paymentState.PaymentState == PaymentStateEnumerator.Processed,
                        PaymentState = paymentState
                    };

                    if (!paymentResponse.IsPaymentProcessed)
                    {
                        return StatusCode(500, new { error = "Payment can't processed" });
                    }
                    return Ok(paymentResponse);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

    }
}
