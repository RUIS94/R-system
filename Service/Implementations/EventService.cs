using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;
using Service.Interfaces;
using Service.Shared;

namespace Service.Implementations
{
    public class EventService : IEventService
    {
        private readonly IEventBusiness _business;
        private readonly TransactionExecutor _transactionExecutor;

        public EventService(IEventBusiness business, IUnitOfWork unitOfWork)
        {
            _business = business;
            _transactionExecutor = new TransactionExecutor(unitOfWork);
        }

        public async Task<List<Event>> GetAllAsync() => await _business.GetAllAsync();

        public async Task<bool> AddAsync(Event ev) =>
            await _transactionExecutor.ExecuteAsync(() => _business.AddAsync(ev));

        public async Task<bool> UpdateAsync(Event ev) =>
            await _transactionExecutor.ExecuteAsync(() => _business.UpdateAsync(ev));

        public async Task<bool> DeleteAsync(int id) =>
            await _transactionExecutor.ExecuteAsync(() => _business.DeleteAsync(id));
    }
}
