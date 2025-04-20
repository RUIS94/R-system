using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;
using Service.Interfaces;
using Service.Shared;

namespace Service.Implementations
{
    public class HelpDocService : IHelpDocService
    {
        private readonly IHelpDocBusiness _business;
        private readonly TransactionExecutor _transactionExecutor;

        public HelpDocService(IHelpDocBusiness business, IUnitOfWork unitOfWork)
        {
            _business = business;
            _transactionExecutor = new TransactionExecutor(unitOfWork);
        }

        public async Task<List<HelpDoc>> GetAllHelpDocsAsync()
        {
            return await _business.GetAllHelpDocsAsync();
        }

        public async Task<bool> AddHelpDocAsync(HelpDoc helpDoc)
        {
            return await _transactionExecutor.ExecuteAsync(() =>
                _business.AddHelpDocAsync(helpDoc));
        }
    }
}