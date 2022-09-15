using System.Linq.Expressions;

namespace Hotel.Booking.Common.Contract.DataAccess
{
    public interface IRepository<TEFEntity> : IDisposable
    {
        int Add(TEFEntity model);
        void Delete(TEFEntity model);
        void Update(TEFEntity model);
        List<TEFEntity> Where(Expression<Func<TEFEntity, bool>> predicate);
        IQueryable<TEFEntity> WhereQueryable(Expression<Func<TEFEntity, bool>> predicate);
        TEFEntity? SingleOrDefault(Expression<Func<TEFEntity, bool>> predicate);
        Task<TEFEntity?> SingleOrDefaultAsync(Expression<Func<TEFEntity, bool>> predicate);
        Task<TEFEntity?> SingleOrDefaultWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate);
        Task<TEFEntity?> FirstOrDefaultAsync(Expression<Func<TEFEntity, bool>> predicate);
        Task<TEFEntity?> FirstOrDefaultWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate);     
        Task<TEFEntity?> SingleOrDefaultEagerWithChildrenWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate, params Expression<Func<TEFEntity, object>>[] includeExpressions);
        Task<List<TEFEntity>> WhereAsync(Expression<Func<TEFEntity, bool>> predicate);       
        Task<List<TEFEntity>> WhereWithTrackingAsync(Expression<Func<TEFEntity, bool>> predicate);       
        Task<bool> AnyAsync(Expression<Func<TEFEntity, bool>> predicate);
        Task<int> CountWhereAsync(Expression<Func<TEFEntity, bool>> predicate);
        Task<int> AddAsync(TEFEntity model);
        Task<int> DeleteAsync(TEFEntity model);
        Task<int> UpdateAsync(TEFEntity model);
        Task<int> AddMultipleAsync(List<TEFEntity> entities);
        Task<int> UpdateMultipleAsync(List<TEFEntity> entities);
        Task<int> DeleteMultipleAsync(List<TEFEntity> entities);
    }
}
