using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DomainModels;

namespace Business.Interfaces
{
    public interface IEventBusiness
    {
        Task<List<Event>> GetAllAsync();
        Task<bool> AddAsync(Event ev);
        Task<bool> UpdateAsync(Event ev);
        Task<bool> DeleteAsync(int id);
    }
}
