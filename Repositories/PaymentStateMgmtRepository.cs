using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Repositories
{
    public class PaymentStateMgmtRepository : GenericRepository<PaymentStateManagement>, IPaymentStateMgmtRepository
    {

        public PaymentStateMgmtRepository(PaymentContext _context) : base(_context)
        {

        }

        public override async Task<PaymentStateManagement> GetById(long id)
        {
            return await _context.Set<PaymentStateManagement>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.PaymentId == id);
        }
    }
}
