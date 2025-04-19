using System.Data;
using MySql.Data.MySqlClient;

namespace DataAccess.Repositories
{
    public class BaseRepository
    {
        protected readonly string _connectionString;
        protected MySqlTransaction? Transaction { get; set; }
        public BaseRepository()
        {
            var db = new DbConnection();
            _connectionString = db.ConnectionString;
        }
        public async Task<DataTable> ExecuteQueryAsync(string query, Dictionary<string, object?>? parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    await conn.OpenAsync();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        await Task.Run(() => adapter.Fill(dataTable));
                    }
                }
            }
            return dataTable;
        }

        public async Task ExecuteNonQueryAsync(string query, Dictionary<string, object?> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                using (MySqlTransaction trans = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, conn, trans))
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                            }

                            await cmd.ExecuteNonQueryAsync();
                        }
                        await trans.CommitAsync(); 
                    }
                    catch
                    {
                        await trans.RollbackAsync(); 
                        throw;
                    }
                }
            }
        }

        public async Task<string?> ExecuteScalarAsync(string query, Dictionary<string, object?>? parameters = null)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue("@" + param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    await conn.OpenAsync();
                    var result = await cmd.ExecuteScalarAsync();
                    return result?.ToString();
                }
            }
        }
    }
}
