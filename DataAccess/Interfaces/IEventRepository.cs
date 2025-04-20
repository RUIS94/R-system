using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace DataAccess.Interfaces
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task<bool> AddAsync(Event ev);
        Task<bool> UpdateAsync(Event ev);
        Task<bool> DeleteAsync(int id);
    }
}
