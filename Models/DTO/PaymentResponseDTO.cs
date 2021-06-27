using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.DTO
{
    public class PaymentResponseDTO
    {
        public bool IsPaymentProcessed { get; set; }

        public PaymentStateDTO PaymentState { get; set; }
    }
}
