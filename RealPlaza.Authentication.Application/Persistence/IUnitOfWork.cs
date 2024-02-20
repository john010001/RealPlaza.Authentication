using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        DbConnection? Connection { get; }
        DbConnection? ConnectionSecurity { get; }
        DbTransaction? Transaction { get; }
        DbTransaction? TransactionSecurity { get; }
        Task Begin();
        Task Commit();
        Task Rollback();
        Task BeginSecurity();
        Task CommitSecurity();
        Task RollbackSecurity();
    }
}
