using Hotel.Booking.Contract.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel.Booking.Contract.Impl
{
    public class GenericRepository<TEFEntity> : IRepository<TEFEntity>, IDisposable
       where TEFEntity : class, IEntity, new()
    {
       
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        protected DbSet<TEFEntity> DbSet
        {
            get
            {
                return _context.Set<TEFEntity>();
            }
        }

        public int Add(TEFEntity entity)
        {
            DbSet.Add(entity);
            Flush();
            return entity.Id;
        }

        public void Delete(TEFEntity entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            Flush();
        }

        public void Update(TEFEntity entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Flush();
        }

        public List<TEFEntity> Where(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToList();
        }

        public IQueryable<TEFEntity> WhereQueryable(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate);
        }

        public TEFEntity SingleOrDefault(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().SingleOrDefault(predicate);
        }

        public Task<TEFEntity> SingleOrDefaultAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        public Task<TEFEntity> SingleOrDefaultWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.SingleOrDefaultAsync(predicate);
        }

        public Task<TEFEntity> FirstOrDefaultAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEFEntity> FirstOrDefaultWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<TEFEntity> SingleOrDefaultEagerWithChildrenWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate, params Expression<Func<TEFEntity, object>>[] includeExpressions)
        {
            IQueryable<TEFEntity> set = DbSet;
            foreach (var includeExpression in includeExpressions)
            {
                set = set.Include(includeExpression);
            }
            return set.SingleOrDefaultAsync(predicate);
        }

        public Task<List<TEFEntity>> WhereAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public Task<List<TEFEntity>> WhereWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToListAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().AnyAsync(predicate);
        }

        public Task<int> CountWhereAsync(Expression<Func<TEFEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().CountAsync(predicate);
        }

        public async Task<int> AddAsync(TEFEntity entity)
        {
            var newEntity = DbSet.Add(entity);
            await FlushAsync();
            return newEntity.Entity.Id;
        }

        public async Task<int> DeleteAsync(TEFEntity entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            return await FlushAsync();
        }

        public async Task<int> UpdateAsync(TEFEntity entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return await FlushAsync();
        }

        public Task<int> AddMultipleAsync(List<TEFEntity> entities)
        {
            DbSet.AddRange(entities);
            return FlushAsync();
        }

        public Task<int> UpdateMultipleAsync(List<TEFEntity> entities)
        {
            entities.ForEach(x =>
            {
                DbSet.Attach(x);
                _context.Entry(x).State = EntityState.Modified;
            });
            return FlushAsync();
        }

        public Task<int> DeleteMultipleAsync(List<TEFEntity> entities)
        {
            entities.ForEach(x =>
            {
                DbSet.Attach(x);
                _context.Entry(x).State = EntityState.Deleted;
            });
            return FlushAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #region Private Methods

        private void Flush()
        {
            _context.SaveChanges();
        }

        private async Task<int> FlushAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _context != null)
            {
                _context.Dispose();
                _context = null;
            }
        }
               

        #endregion

    }
}
