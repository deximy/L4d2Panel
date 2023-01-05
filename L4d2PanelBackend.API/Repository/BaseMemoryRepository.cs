using System.Linq.Expressions;

namespace L4d2PanelBackend.API.Repository
{
    public class BaseMemoryRepository<T> : IRepository<T> where T : Models.BaseModel, new()
    {
        internal List<T> model_list_ { get; set; }

        public BaseMemoryRepository()
        {
            model_list_ = new List<T>();
        }

        public Task<Guid> Add(T entity)
        {
            model_list_.Add(entity);
            return Task.FromResult(entity.id);
        }

        public Task<T> Query(Guid id, bool use_cache)
        {
            return Task.FromResult(
                model_list_.Where(x => x.id == id).First()
            );
        }

        public Task<List<T>> Query(List<Guid> id_list)
        {
            return Task.FromResult(
                model_list_.Where(x => id_list.Contains(x.id)).ToList()
            );
        }

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression)
        {
            return Task.FromResult(
                model_list_.Where(where_expression.Compile()).ToList()
            );
        }

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, int top)
        {
            return Task.FromResult(
                model_list_.Where(where_expression.Compile()).Take(top).ToList()
            );
        }

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, bool is_asc = true)
        {
            return Task.FromResult(
                is_asc
                ? model_list_.Where(where_expression.Compile()).OrderBy(order_by_expression.Compile()).ToList()
                : model_list_.Where(where_expression.Compile()).OrderByDescending(order_by_expression.Compile()).ToList()
            );
        }

        public Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, int top, bool is_asc = true)
        {
            return Task.FromResult(
                is_asc
                ? model_list_.Where(where_expression.Compile()).OrderBy(order_by_expression.Compile()).Take(top).ToList()
                : model_list_.Where(where_expression.Compile()).OrderByDescending(order_by_expression.Compile()).Take(top).ToList()
            );
        }

        public Task<bool> Update(T entity)
        {
            var entity_list = model_list_.Select((x, i) => new { i, x }).Where(t => t.x.id == entity.id).Select(t => t.i).ToList();
            if (entity_list.Count != 1)
            {
                return Task.FromResult(false);
            }
            model_list_[entity_list.First()] = entity;
            return Task.FromResult(true);
        }
    }
}
