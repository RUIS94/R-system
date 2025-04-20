using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace Service.Shared
{
    public class TransactionExecutor
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionExecutor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> operation)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await operation();
                await _unitOfWork.CommitAsync();
                return result;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<T> ExecuteAsync<T>(Func<Task<T>> operation, string? errorMessage=null)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var result = await operation();
                await _unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    throw new InvalidOperationException($"{errorMessage}: {ex.Message}");
                }
                throw;
            }
        }
    }
}