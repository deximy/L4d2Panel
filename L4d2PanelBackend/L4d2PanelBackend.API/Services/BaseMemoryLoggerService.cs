using SqlSugar;
using System.Linq.Expressions;

namespace L4d2PanelBackend.API.Services
{
    public class BaseMemoryLoggerService<T> : ILoggerService<T> where T : class
    {

        public Task<Guid> Add(T message)
        {
            throw new NotImplementedException();
        }

        public Task<T> Query(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, bool is_asc = true)
        {
            throw new NotImplementedException();
        }
    }
}
