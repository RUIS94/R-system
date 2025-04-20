using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Service.Interfaces
{
    public interface IHelpDocService
    {
        Task<List<HelpDoc>> GetAllHelpDocsAsync();
        Task<bool> AddHelpDocAsync(HelpDoc helpDoc);
    }
}
