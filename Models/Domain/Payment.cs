using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.Domain
{
    public class Payment
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentId { get; set; }

        [Required]
        public string CreditCardNumber { get; set; }
        
        [Required]
        public string CardHolder { get; set; }
        
        [Required]
        public DateTime Expiry { get; set; }
        
        [Column(nameof(CVV), TypeName = "nvarchar(3)")]
        public string CVV { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        [Column(nameof(Amount), TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        
        [InverseProperty(nameof(PaymentStateManagement.Payment))]
        public virtual ICollection<PaymentStateManagement> PaymentStates { get; set; }
    }
}
