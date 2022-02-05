using SqlSugar;
using System.Linq.Expressions;

namespace L4d2PanelBackend.Services
{
    public class BaseSqlLoggerService<T> : DbScope, ILoggerService<T> where T : class
    {
        public SqlSugarScope Db_ => Db;

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
