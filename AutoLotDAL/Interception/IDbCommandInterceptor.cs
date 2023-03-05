using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLotDAL.EF
{
    public interface IDbCommandInterceptor : IDbInterceptor
    {
        void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int>
       interceptionContext);
        void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int>
       interceptionContext);
        void ReaderExecuted(DbCommand command,
        DbCommandInterceptionContext<DbDataReader> interceptionContext);
        void ReaderExecuting(DbCommand command,
        DbCommandInterceptionContext<DbDataReader> interceptionContext);
        void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object>
       interceptionContext);
        void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object>
       interceptionContext);
    }
}
