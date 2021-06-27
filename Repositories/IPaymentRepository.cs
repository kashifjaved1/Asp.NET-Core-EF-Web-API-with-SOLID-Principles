using PaymentGatewayAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Repositories
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
    }
}
