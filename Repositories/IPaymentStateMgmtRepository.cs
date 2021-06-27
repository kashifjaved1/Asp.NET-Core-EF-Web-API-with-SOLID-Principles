using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGatewayAPI.Models.Domain;

namespace PaymentGatewayAPI.Repositories
{
    public interface IPaymentStateMgmtRepository : IGenericRepository<PaymentStateManagement>
    {
    }
}
