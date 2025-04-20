using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.HelpDocs
{
    public class HelpDocBusiness : IHelpDocBusiness
    {
        private readonly IHelpDocRepository _repository;

        public HelpDocBusiness(IHelpDocRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<HelpDoc>> GetAllHelpDocsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<bool> AddHelpDocAsync(HelpDoc helpDoc)
        {
            if (string.IsNullOrWhiteSpace(helpDoc.Name))
                throw new ArgumentException("Name is required");

            return await _repository.AddAsync(helpDoc);
        }
    }
}
