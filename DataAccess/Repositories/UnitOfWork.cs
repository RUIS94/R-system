using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private MySqlConnection _connection;
        private MySqlTransaction _transaction;

        public UnitOfWork()
        {
            var db = new DbConnection();
            _connectionString = db.ConnectionString;
        }

        public async Task BeginTransactionAsync()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(_connectionString);
                await _connection.OpenAsync();
            }

            if (_transaction == null)
            {
                _transaction = await _connection.BeginTransactionAsync();
            }
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction = null;
            }
        }

        public async Task SaveChangesAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
