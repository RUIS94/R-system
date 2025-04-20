using Business.Interfaces;
using DataAccess.Interfaces;
using Model.DomainModels;

namespace Business.Events
{
    public class EventBusiness : IEventBusiness
    {
        private readonly IEventRepository _repo;

        public EventBusiness(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<bool> AddAsync(Event ev)
        {
            return await _repo.AddAsync(ev);
        }

        public async Task<bool> UpdateAsync(Event ev)
        {
            return await _repo.UpdateAsync(ev);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id);
        }
    }
}