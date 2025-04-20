using System.Data;
using System.Security.Principal;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Model.DomainModels;
using Optional.Caching;
using StackExchange.Redis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Repositories
{
    public class EventRepository : BaseRepository, IEventRepository
    {
        private readonly RedisHelper redis;

        public EventRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<Event>> GetAllAsync()
        {
            string eventCacheKey = "AllEvents";
            var allEvents = await redis.GetAsync<List<Event>>(eventCacheKey);

            if (allEvents != null)
            {
                return allEvents;
            }

            string query = "SELECT * FROM events";
            DataTable table = await ExecuteQueryAsync(query);
            List<Event> events = new List<Event>();
            foreach (DataRow row in table.Rows)
            {
                Event eve = MapToEvent(row);
                events.Add(eve);
            }

            await redis.SetAsync(eventCacheKey, events, TimeSpan.FromMinutes(1));
            return events;
        }

        public async Task<bool> AddAsync(Event eve)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "summary", eve.Summary },
                { "event_date", eve.EventDate },
                { "notes", eve.Notes }
            };

            string query = @"INSERT INTO events (summary, event_date, notes) 
                VALUES (@(summary, @event_date, @notes)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateAsync(Event eve)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", eve.ID },
                { "summary", eve.Summary },
                { "event_date", eve.EventDate },
                { "notes", eve.Notes }
            };

            var query = @"UPDATE events 
                    SET summary = @summary, event_date = @event_date, notes = @notes 
                    WHERE id = @ID";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }
        public async Task<bool> DeleteAccountAsync(int customerId)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "customer_id", customerId }
            };

            string query = "DELETE FROM accounts WHERE customer_id = @customer_id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "id", id }
            };

            string query = "DELETE FROM events WHERE id = @id";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        private Event MapToEvent(DataRow row)
        {
            return new Event
            {
                ID = Convert.ToInt32(row["id"]),
                Summary = row["summary"].ToString(),
                EventDate = Convert.ToDateTime(row["event_date"]),
                Notes = row["notes"].ToString()
            };
        }
    }
}