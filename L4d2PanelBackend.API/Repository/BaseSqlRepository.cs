using L4d2PanelBackend.API.Models;
using SqlSugar;
using System.Linq.Expressions;

namespace L4d2PanelBackend.API.Repository
{
    public abstract class BaseSqlRepository<T> : DbScope, IRepository<T> where T : BaseModel, new()
    {
        public async Task<Guid> Add(T entity)
        {
            return (await Db.Insertable(entity).ExecuteReturnEntityAsync()).id;
        }

        public async Task<T> Query(Guid id, bool use_cache)
        {
            return await Db.Queryable<T>().WithCacheIF(use_cache).SingleAsync(x => x.id == id);
        }

        public async Task<List<T>> Query(List<Guid> id_list)
        {
            return await Db.Queryable<T>().In(id_list).ToListAsync();
        }

        public async Task<List<T>> Query(Expression<Func<T, bool>> where_expression)
        {
            return await
                Db
                .Queryable<T>()
                .WhereIF(where_expression != null, where_expression)
                .ToListAsync();
        }

        public async Task<List<T>> Query(Expression<Func<T, bool>> where_expression, int top)
        {
            return await
                Db
                .Queryable<T>()
                .WhereIF(where_expression != null, where_expression)
                .Take(top)
                .ToListAsync();
        }

        public async Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, bool is_asc = true)
        {
            return await
                Db
                .Queryable<T>()
                .OrderByIF(order_by_expression != null, order_by_expression, is_asc ? OrderByType.Asc : OrderByType.Desc)
                .WhereIF(where_expression != null, where_expression)
                .ToListAsync();
        }


        public async Task<List<T>> Query(Expression<Func<T, bool>> where_expression, Expression<Func<T, object>> order_by_expression, int top, bool is_asc = true)
        {
            return await
                Db
                .Queryable<T>()
                .OrderByIF(order_by_expression != null, order_by_expression, is_asc ? OrderByType.Asc : OrderByType.Desc)
                .WhereIF(where_expression != null, where_expression)
                .Take(top)
                .ToListAsync();
        }

        public async Task<bool> Update(T entity)
        {
            return await Db.Updateable(entity).ExecuteCommandHasChangeAsync();
        }
    }

    public class DbScope
    {
        protected static SqlSugarScope Db = new SqlSugarScope(
            new ConnectionConfig() {
                DbType = DbType.Sqlite,
                ConnectionString = "ServerManager.db",
            },
            db => {
                db.Aop.OnLogExecuting = (s, p) => {
                    Console.WriteLine(s);
                };
            }
        );
    }
}
