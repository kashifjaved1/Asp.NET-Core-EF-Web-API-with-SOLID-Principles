using PaymentGatewayAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.PaymentProvider
{
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        public PaymentStateDTO PaymentGateway(PaymentRequestDTO paymentRequestDTO)
        {
            Random random = new Random();
            var number = random.Next(1, 12);
            if (number % 4 == 0 || number % 6 == 0)
            {
                throw new Exception("Payment Failed.");
            }

            if (number % 2 == 0)
            {
                return new PaymentStateDTO()
                {
                    PaymentState = PaymentStateEnumerator.Failed,
                    PaymentStateDate = DateTime.UtcNow
                };
            }
            else
            {
                return new PaymentStateDTO()
                {
                    PaymentState = PaymentStateEnumerator.Processed,
                    PaymentStateDate = DateTime.UtcNow
                };
            }
        }
    }
}
