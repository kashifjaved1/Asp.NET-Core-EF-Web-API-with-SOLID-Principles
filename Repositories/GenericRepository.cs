using Microsoft.EntityFrameworkCore;
using PaymentGatewayAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly PaymentContext _context;

        public GenericRepository(PaymentContext context)
        {
            _context = context;
        }
        public async Task<TEntity> Create(TEntity entity)
        {
            var val = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return val.Entity;
        }

        public async Task Delete(long id)
        {
            var entityToDelete = await GetById(id);
            _context.Set<TEntity>().Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public abstract Task<TEntity> GetById(long id);

        public async Task Update(long id, TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
