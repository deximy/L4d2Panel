using L4d2PanelBackend.API.Models;
using System.Linq.Expressions;

namespace L4d2PanelBackend.API.Repository
{
    public interface IRepository<T> where T : BaseModel, new()
    {
        public Task<Guid> Add(T entity);

        public Task<T> Query(Guid id, bool use_cache = true);

        public Task<List<T>> Query(List<Guid> id_list);

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression);

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, int top);

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, bool is_asc = true);

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, int top, bool is_asc = true);

        public Task<bool> Update(T entity);
    }
}
