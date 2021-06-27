using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.Domain
{
    public class PaymentStateManagement
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentStateId { get; set; }
        
        [Required]
        public string State { get; set; }
        
        [Required]
        [Column(nameof(CreationDate), TypeName = "datetime")]
        public DateTime CreationDate { get; set; }

        public long PaymentId { get; set; }

        [ForeignKey(nameof(PaymentId))]
        public Payment Payment { get; set; }
    }
}
