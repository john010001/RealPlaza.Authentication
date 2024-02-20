using RealPlaza.Authentication.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection? _connection;
        private DbTransaction? _transaction;
        private readonly DbConnection? _connectionSecurity;
        private DbTransaction? _transactionSecurity;

        public UnitOfWork(DbConnection connection, DbConnection connectionSecurity)
        {
            _connectionSecurity = connectionSecurity;
            _connection = connection;
        }

        public DbConnection? Connection => _connection;

        public DbTransaction? Transaction => _transaction;

        public DbConnection? ConnectionSecurity => _connectionSecurity;

        public DbTransaction? TransactionSecurity => _transactionSecurity;

        public async Task Begin()
        {
            if (_connection is not null)
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                {
                    await _connection.OpenAsync();
                }
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        public async Task Commit()
        {
            if (_transaction is not null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public async Task Rollback()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                await DisposeAsync();
            }
        }

        public async Task BeginSecurity()
        {
            if (_connectionSecurity is not null)
            {
                _transactionSecurity = await _connectionSecurity.BeginTransactionAsync();
            }
        }

        public async Task CommitSecurity()
        {
            if (_transactionSecurity is not null)
            {
                await _transactionSecurity.CommitAsync();
                await DisposeAsync();
            }
        }

        public async Task RollbackSecurity()
        {
            if (_transactionSecurity != null)
            {
                await _transactionSecurity.RollbackAsync();
                await DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
            if (_transactionSecurity is not null)
            {
                await _transactionSecurity.DisposeAsync();
                _transactionSecurity = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
