using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PaymentGatewayAPI.Models
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-R50DEPO; Database=PaymentsDB; Trusted_Connection=True;");
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStateManagement> PaymentState { get; set; }
    }
}
