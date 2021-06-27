using PaymentGatewayAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.PaymentProvider
{
    public interface IGenericPaymentProvider
    {
        PaymentStateDTO PaymentGateway(PaymentRequestDTO paymentRequestDTO);
    }
}
