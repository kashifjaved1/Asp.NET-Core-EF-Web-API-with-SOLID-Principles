using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.DTO
{
    public class PaymentStateDTO
    {
        public PaymentStateEnumerator PaymentState { get; set; }

        public DateTime PaymentStateDate { get; set; }
    }
}
