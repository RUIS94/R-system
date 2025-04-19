using System.Data;
using DataAccess.Interfaces;
using Model.DomainModels;
using Optional.Caching;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly RedisHelper redis;

        public UserRepository(RedisHelper redisHelper) : base()
        {
            redis = redisHelper;
        }


        public async Task<List<User>> GetAllUsersAsync()
        {
            string userCacheKey = "AllUsers";
            var allUsers = await redis.GetAsync<List<User>>(userCacheKey);

            if (allUsers != null)
            {
                return allUsers;
            }

            string query = @"
                SELECT u.id, u.username, u.password, u.phone, u.email, u.role_id, r.role_name
                FROM users u
                JOIN roles r ON u.role_id = r.id";
            DataTable table = await ExecuteQueryAsync(query);
            List<User> users = new List<User>();

            foreach (DataRow row in table.Rows)
            {
                User user = MapToUser(row);
                users.Add(user);
            }

            await redis.SetAsync(userCacheKey, users, TimeSpan.FromMinutes(1)); 
            return users;
        }
        public async Task<List<User>> GetUserByTermAsync(string searchTerm)
        {
            string query = @"
                SELECT u.id, u.username, u.password, u.phone, u.email, u.role_id, r.role_name 
                FROM users u
                LEFT JOIN roles r ON u.role_id = r.id
                WHERE u.id LIKE @term OR u.username LIKE @term OR u.email LIKE @term";
            var parameters = new Dictionary<string, object?>
            {
                { "term", $"%{searchTerm}%" }
            };

            DataTable table = await ExecuteQueryAsync(query, parameters);
            List<User> users = new List<User>();

            foreach (DataRow row in table.Rows)
            {
                User user = MapToUser(row);
                users.Add(user);
            }

            return users;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "username", user.UserName },
                { "password", user.Password },
                { "phone", user.Phone },
                { "email", user.Email },
                { "role_id", user.RoleID }
            };

            string query = "INSERT INTO users (username, password, phone, email, role_id) VALUES (@username, @password, @phone, @email, @role_id)";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var parameters = new Dictionary<string, object?>
            {
                { "username", user.UserName },
                { "password", user.Password },
                { "phone", user.Phone },
                { "email", user.Email },
                { "role_id", user.RoleID }
            };

            string query = "UPDATE users SET password = @password, phone = @phone, email = @email, role_id = @role_id WHERE username = @username";
            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<bool> DeleteUserAsync(string username)
        {
            string query = "DELETE FROM users WHERE username = @username";
            var parameters = new Dictionary<string, object?>
            {
                { "username", username }
            };

            await ExecuteNonQueryAsync(query, parameters);
            return true;
        }

        public async Task<string?> GetPasswordByUsernameAsync(string username)
        {
            string query = "SELECT password FROM users WHERE username = @username";
            var parameters = new Dictionary<string, object?>
            {
                { "username", username }
            };
            DataTable table = await ExecuteQueryAsync(query, parameters);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["password"].ToString();
            }
            return null;
        }
        public async Task<int?> GetRoleByUsernameAsync(string username)
        {
            string query = "SELECT role_id FROM users WHERE username = @username";
            var parameters = new Dictionary<string, object?>
            {
                { "username", username }
            };
            DataTable table = await ExecuteQueryAsync(query, parameters);
            if (table.Rows.Count > 0)
            {
                var roleId = table.Rows[0]["role_id"];
                if (roleId != DBNull.Value)
                {
                    return Convert.ToInt32(roleId);
                }
            }
            return null;
        }
        private User MapToUser(DataRow row)
        {
            var user = new User
            {
                ID = Convert.ToInt32(row["id"]),
                UserName = row["username"].ToString(),
                Password = row["password"].ToString(),
                Phone = row["phone"].ToString(),
                Email = row["email"].ToString(),
                RoleID = Convert.ToInt32(row["role_id"]),
                RoleName = row["role_name"].ToString()
            };
            return user;
        }
    }
}