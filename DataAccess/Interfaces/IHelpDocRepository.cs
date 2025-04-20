using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IHelpDocRepository
    {
        Task<List<HelpDoc>> GetAllAsync();
        Task<bool> AddAsync(HelpDoc helpDoc);
    }
}
