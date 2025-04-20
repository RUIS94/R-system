using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Model.DomainModels;
using Optional.Caching;

namespace DataAccess.Repositories
{
    public class HelpDocRepository : BaseRepository, IHelpDocRepository
    {
        private readonly RedisHelper redis;

        public HelpDocRepository(RedisHelper redisHelper)
        {
            redis = redisHelper;
        }

        public async Task<List<HelpDoc>> GetAllAsync()
        {
            string docCacheKey = "AllDocs";
            var allDocs = await redis.GetAsync<List<HelpDoc>>(docCacheKey);

            if (allDocs != null)
            {
                return allDocs;
            }

            string query = "SELECT * FROM help_docs";
            DataTable table = await ExecuteQueryAsync(query);
            List<HelpDoc> docs = new List<HelpDoc>();
            foreach (DataRow row in table.Rows)
            {
                HelpDoc doc = MapToDoc(row);
                docs.Add(doc);
            }

            await redis.SetAsync(docCacheKey, docs, TimeSpan.FromMinutes(1));
            return docs;
        }
        public async Task<bool> AddAsync(HelpDoc doc)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "name", doc.Name },
                { "type", doc.Type },
                { "link", doc.Link }
            };

            string query = "INSERT INTO help_docs (name, type, link) VALUES (@Name, @Type, @Link)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        private HelpDoc MapToDoc(DataRow row)
        {
            return new HelpDoc
            {
                ID = Convert.ToInt32(row["id"]),
                Name = row["name"].ToString(),
                Type = row["type"].ToString(),
                Link = row["link"].ToString()
            };
        }
    }
}
