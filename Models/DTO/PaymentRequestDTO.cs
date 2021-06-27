using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.DTO
{
    public class PaymentRequestDTO
    {
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        [CardInfoValidation.CardDateValidator]
        public string Expiry { get; set; }

        [Required]
        [StringLength(maximumLength: 3, MinimumLength = 3)]
        public string CVV { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
