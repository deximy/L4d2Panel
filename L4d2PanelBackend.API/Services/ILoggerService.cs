using System.Linq.Expressions;

namespace L4d2PanelBackend.Services
{
    public interface ILoggerService<T> where T : class
    {
        public Task<Guid> Add(T message);

        public Task<T> Query(Guid id);

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression);

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, bool is_asc = true);
    }
}
