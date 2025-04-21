using System.Data;
using DataAccess.Interfaces;
using Infrastructure.Caching;
using Model.DomainModels;

namespace DataAccess.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly RedisHelper redis;

        public AccountRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            string accountCacheKey = "AllAccounts";
            var allAccounts = await redis.GetAsync<List<Account>>(accountCacheKey);

            if (allAccounts != null)
            {
                return allAccounts;
            }

            string query = @"
                SELECT a.id, a.customer_id, a.balance, a.created_at, c.username 
                FROM customers c
                JOIN accounts a ON c.id = a.customer_id";
            DataTable table = await ExecuteQueryAsync(query);
            List<Account> accounts = new List<Account>();
            foreach (DataRow row in table.Rows)
            {
                Account account = MapToAccount(row);
                accounts.Add(account);
            }

            await redis.SetAsync(accountCacheKey, accounts, TimeSpan.FromMinutes(1));
            return accounts;
        }

        public async Task<Account> GetAccountByNameAsync(string username)
        {
            string accountCacheKey = $"{username}'s Accounts";
            var userAccount = await redis.GetAsync<Account>(accountCacheKey);

            if (userAccount != null)
            {
                return userAccount;
            }
            string query = @"
                SELECT a.id, a.balance, a.created_at, c.username 
                FROM customers c
                JOIN accounts a ON c.id = a.customer_id
                WHERE c.username = @username";
            var parameters = new Dictionary<string, object?>
            {
                { "username", username }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            if (table.Rows.Count > 0)
            {
                Account account = MapToAccount(table.Rows[0]);
                await redis.SetAsync(accountCacheKey, account, TimeSpan.FromMinutes(1));
                return account;
            }

            return null;
        }

        public async Task<bool> AddAccountAsync(Account account)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "customer_id", account.CustomerID },
                { "balance", account.Balance }
            };

            string query = "INSERT INTO accounts (customer_id, balance) VALUES (@customer_id, @balance)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateBalanceAsync(Account account)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "customer_id", account.CustomerID },
                { "balance", account.Balance }
            };

            string query = "UPDATE accounts SET balance = @balance WHERE customer_id = @customer_id";
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

        public async Task<decimal> GetBalanceByUsernameAsync(string username)
        {
            string query = @"
                SELECT a.balance 
                FROM customers c
                JOIN accounts a ON c.id = a.customer_id
                WHERE c.username = @username";
            var parameters = new Dictionary<string, object?>
            {
                { "username", username }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);

            if (table.Rows.Count > 0)
                return Convert.ToDecimal(table.Rows[0]["balance"]);

            return 0;
        }

        private Account MapToAccount(DataRow row)
        {
            return new Account
            {
                ID = Convert.ToInt32(row["id"]),
                CustomerID = Convert.ToInt32(row["customer_id"]),
                Balance = Convert.ToDecimal(row["balance"]),
                CreatedAt = Convert.ToDateTime(row["created_at"]),
                UserName = row["username"].ToString()
            };
        }
    }
}