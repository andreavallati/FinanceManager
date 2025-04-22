using FinanceManager.API.Application.Interfaces.Repositories.Base;
using FinanceManager.API.Domain.Entities.Base;
using FinanceManager.API.Infrastructure.Context;
using FinanceManager.Shared.Constants;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.API.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        protected RepositoryBase(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            if (!_context.Database.CanConnect())
            {
                throw new InvalidOperationException("Unable to connect to the database");
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var table = GetTable();
            return await table.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id)
        {
            var table = GetTable();
            var entity = await table.FindAsync(id);
            if (entity is null)
            {
                throw new InvalidOperationException("Entity not found");
            }

            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            entity.CreationDate = DateTime.UtcNow.ToLocalTime();
            entity.CreationUser = Common.CreationUsername;

            var table = GetTable();
            var insertResult = await table.AddAsync(entity);
            return insertResult.Entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            var existingEntity = await _context.Set<TEntity>().FindAsync(entity.Id);
            if (existingEntity is null)
            {
                throw new InvalidOperationException("Entity not found");
            }

            entity.ModificationDate = DateTime.UtcNow.ToLocalTime();
            entity.ModificationUser = Common.ModificationUsername;

            // Set only updated values to the existing entity
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var table = GetTable();
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                table.Remove(entity);
            }
        }

        public virtual async Task CommitChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private DbSet<TEntity> GetTable()
        {
            var table = _context.Set<TEntity>();
            return table;
        }
    }
}