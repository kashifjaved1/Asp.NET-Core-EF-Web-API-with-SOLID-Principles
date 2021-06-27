using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {

        public PaymentRepository(PaymentContext _context) : base(_context)
        {
        }

        public override async Task<Payment> GetById(long id)
        {
            return await _context.Set<Payment>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => entity.PaymentId == id);
        }
    }
}
